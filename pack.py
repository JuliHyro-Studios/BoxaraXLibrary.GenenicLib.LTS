import json
import shutil
import subprocess
import sys
import urllib.error
import urllib.request
import xml.etree.ElementTree as ET
from pathlib import Path

# ============================================================
# GLOBAL CONFIGURATION - EDIT ONLY THESE VARIABLES
# ============================================================
PACKAGE_NAME = "BoxaraXLibrary.GenenicLib.LTS"
CSPROJ_FILE = f"{PACKAGE_NAME}.csproj"
NUGET_API_URL = "https://api.nuget.org/v3/index.json"
NUGET_PACKAGE_URL = f"https://www.nuget.org/packages/{PACKAGE_NAME}"
OUTPUT_DIR = Path("nupkgs")
# ============================================================


def get_project_properties():
    project_path = Path(CSPROJ_FILE)

    if not project_path.exists():
        print(f"[ERROR] Project file not found: {project_path}")
        sys.exit(1)

    try:
        root = ET.parse(project_path).getroot()
    except ET.ParseError as e:
        print(f"[ERROR] Failed to parse {project_path}: {e}")
        sys.exit(1)

    properties = {}

    for element in root.iter():
        if not isinstance(element.tag, str):
            continue

        tag = element.tag.rsplit("}", 1)[-1]
        if tag in {"PackageId", "Version", "TargetFrameworks", "TargetFramework"}:
            value = (element.text or "").strip()
            if value and tag not in properties:
                properties[tag] = value

    package_name = properties.get("PackageId") or PACKAGE_NAME
    version = properties.get("Version") or "1.0.0"
    target_frameworks = (
        properties.get("TargetFrameworks")
        or properties.get("TargetFramework")
        or "net8.0"
    )

    if "TargetFrameworks" not in properties and "TargetFramework" not in properties:
        print("[WARN] Cannot find TargetFrameworks in .csproj. Using fallback: net8.0")

    return package_name, version, target_frameworks


def fetch_latest_version(package_name):
    flat_container_url = (
        "https://api.nuget.org/v3-flatcontainer/"
        f"{package_name.lower()}/index.json"
    )

    request = urllib.request.Request(
        flat_container_url,
        headers={"Accept": "application/json"},
    )

    try:
        with urllib.request.urlopen(request, timeout=15) as response:
            data = json.loads(response.read().decode("utf-8"))

        versions = data.get("versions", [])
        if not versions:
            print("[WARN] NuGet returned no published versions.")
            return "unknown"

        return str(versions[-1])

    except (urllib.error.URLError, TimeoutError, json.JSONDecodeError) as e:
        print(f"[WARN] Failed to fetch latest version from NuGet.org: {e}")
        print("[WARN] Make sure you are connected to the internet.")
        print("[WARN] Skipping version check.")
        return "unknown"
    except Exception as e:
        print(f"[WARN] Unexpected NuGet error: {e}")
        print("[WARN] Skipping version check.")
        return "unknown"


def ask_confirmation():
    while True:
        try:
            answer = input("Do you want to continue anyway? (Y/N): ").strip().lower()
        except KeyboardInterrupt:
            print()
            return False

        if answer in {"y", "yes"}:
            return True
        if answer in {"n", "no", ""}:
            return False

        print("[WARN] Please enter Y or N.")


def run_command(command, step_name):
    try:
        result = subprocess.run(command)
    except FileNotFoundError:
        print("[ERROR] 'dotnet' was not found.")
        print("[ERROR] Make sure the .NET SDK is installed and available in PATH.")
        sys.exit(1)
    except KeyboardInterrupt:
        print()
        print(f"[WARN] {step_name} cancelled by user.")
        sys.exit(130)
    except OSError as e:
        print(f"[ERROR] Failed to start {step_name}: {e}")
        sys.exit(1)

    if result.returncode != 0:
        print(f"[ERROR] {step_name} failed with error code {result.returncode}.")
        sys.exit(result.returncode)


def clean_output_directory():
    print("\n[STEP 5] Cleaning old nupkgs folder...")

    try:
        OUTPUT_DIR.mkdir(parents=True, exist_ok=True)

        files_removed = 0

        for item in OUTPUT_DIR.iterdir():
            if item.is_file() and item.suffix.lower() in {".nupkg", ".snupkg"}:
                try:
                    item.unlink()
                    files_removed += 1
                    print(f"[INFO] Removed: {item.name}")
                except PermissionError:
                    print(f"[ERROR] Access denied: {item}")
                    print(
                        "[ERROR] The package file may be locked by another "
                        "application."
                    )
                    sys.exit(1)
                except OSError as exc:
                    print(f"[ERROR] Could not remove {item}: {exc}")
                    sys.exit(1)

        if files_removed == 0:
            print("[INFO] No old NuGet packages found.")
        else:
            print(f"[INFO] Removed {files_removed} old package(s).")

    except PermissionError:
        print(f"[ERROR] Access denied: {OUTPUT_DIR}")
        print(
            "[ERROR] Close any Explorer, IDE, terminal, or application "
            "currently using the nupkgs folder."
        )
        sys.exit(1)

    except OSError as exc:
        print(f"[ERROR] Could not access {OUTPUT_DIR}: {exc}")
        sys.exit(1)


def main():
    print("=" * 59)
    print(f"  Packing {PACKAGE_NAME}")
    print("=" * 59)
    print()

    # ===== STEP 1: FETCH LATEST VERSION FROM NUGET.ORG =====
    print("[STEP 1] Fetching latest version from NuGet.org...")
    nuget_latest = fetch_latest_version(PACKAGE_NAME)

    if nuget_latest != "unknown":
        print(f"[INFO] Latest version on NuGet.org: {nuget_latest}")
    print()

    # ===== STEP 1.5: READ PROJECT INFORMATION =====
    print("[STEP 1.5] Reading project information from .csproj...")
    package_name, local_version, target_frameworks = get_project_properties()

    print(f"[INFO] Package: {package_name}")
    print(f"[INFO] Target frameworks: {target_frameworks}")
    print()

    # ===== STEP 2: READ LOCAL VERSION FROM CSPROJ =====
    print("[STEP 2] Reading local version from .csproj...")
    print(f"[INFO] Local version: {local_version}")
    print()

    # ===== STEP 3: COMPARE VERSIONS =====
    print("[STEP 3] Checking version conflict...")

    if nuget_latest == "unknown":
        print("[WARN] Skipping version comparison due to network error.")

    elif local_version == nuget_latest:
        print(
            f"[WARN] Local version {local_version} matches "
            f"NuGet version {nuget_latest}."
        )
        print(
            "[WARN] Publishing the same version will NOT work. "
            "NuGet rejects duplicate versions."
        )
        print(
            "[WARN] Only continue if you are RE-PUBLISHING the same "
            "version for fixing metadata."
        )
        print()

        print("[INFO] Metadata:")
        print(f"  - Package: {package_name}")
        print(f"  - Version: {local_version}")
        print("  - Status: Already listed on NuGet.org")
        print(
            "  - Note: This version is normally immutable. "
            "NuGet may reject re-publishing it."
        )
        print()

        if not ask_confirmation():
            print("[INFO] Pack cancelled by user.")
            print(
                "[INFO] Please update the version in .csproj if you want "
                "to publish a new version."
            )
            sys.exit(0)

        print(f"[INFO] User confirmed to continue with version {local_version}.")
        print("[WARN] NuGet may reject this push if the package already exists.")

    else:
        print(
            f"[INFO] Local version {local_version} is different from "
            f"NuGet latest {nuget_latest}."
        )
        print("[INFO] No version conflict. Ready to publish new version!")

    print()

    # ===== STEP 4: CHECK LOCAL PACKAGE =====
    print("[STEP 4] Checking local package...")
    nupkg_path = OUTPUT_DIR / f"{package_name}.{local_version}.nupkg"

    if nupkg_path.exists():
        print(
            f"[WARN] Package {local_version} already exists in "
            f"{OUTPUT_DIR} folder!"
        )
        print("[WARN] It will be overwritten during pack.")
    else:
        print("[INFO] No existing package with this version found.")
    print()

    # ===== STEP 5: CLEAN OLD OUTPUT FOLDER =====
    print(f"[STEP 5] Cleaning old {OUTPUT_DIR} folder...")
    clean_output_directory()
    print()

    # ===== STEP 6: BUILD RELEASE =====
    print("[STEP 6] Building Release...")
    print(f"[INFO] Target frameworks: {target_frameworks}")
    print("[INFO] This may take a few seconds...")

    run_command(
        ["dotnet", "build", "-c", "Release", "--no-restore"],
        "Build",
    )

    print("[INFO] Build completed successfully.")
    print()

    # ===== STEP 7: PACK =====
    print("[STEP 7] Packing...")
    print(
        f"[INFO] Creating package: "
        f"{package_name}.{local_version}.nupkg"
    )

    run_command(
        ["dotnet", "pack", "-c", "Release", "-o", str(OUTPUT_DIR)],
        "Pack",
    )

    if not nupkg_path.exists():
        print(
            "[ERROR] dotnet pack completed, but the expected package "
            f"was not found: {nupkg_path}"
        )
        sys.exit(1)

    print("[INFO] Pack completed successfully.")
    print()

    # ===== STEP 8: DISPLAY RESULTS =====
    print("=" * 59)
    print("  PACK SUCCESSFUL")
    print("=" * 59)
    print()
    print(f"[INFO] Package: {nupkg_path.name}")
    print(f"[INFO] Location: ./{OUTPUT_DIR}/")
    print()
    print("[INFO] Next steps:")
    print(
        f"  1. Test the package locally:\n"
        f"     dotnet add package {package_name} --version {local_version}"
    )
    print()
    print(
        f"  2. Push to NuGet:\n"
        f"     dotnet nuget push ./{OUTPUT_DIR}/{nupkg_path.name} "
        f"-k YOUR_API_KEY -s {NUGET_API_URL}"
    )
    print()


if __name__ == "__main__":
    main()
