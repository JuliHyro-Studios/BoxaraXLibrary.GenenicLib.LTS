# 📦 BoxaraXLibrary.GenenicLib.LTS

LTS .NET 10 APACHE 2.0

**Generic Library LTS** — Core framework for building shell-based CLI applications with command handling, rich console UI, and fluent API.

---

## 📖 Overview

**BoxaraXLibrary.GenenicLib.LTS** is a lightweight, high-performance framework designed for building **shell-based CLI applications** in .NET. It provides a complete infrastructure for command registration, shell lifecycle management, and interactive console experiences.

### ✨ Key Features

- **Shell Engine** — Build and run interactive shells with customizable prompts and headers
- **Command System** — Register, discover, and execute commands via reflection or manual registration
- **External Command Support** — Register commands from external sources via `ExternalCommandManager`
- **Fluent API** — Build shells with a clean, expressive fluent interface
- **Rich Console UI** — 16+ header styles, 10+ prompt styles, table formatter, colored logs
- **Cross-Platform** — Works on Windows, Linux, and macOS via .NET 10
- **Lightweight** — Zero external dependencies, minimal footprint
- **Extensible** — Easy to extend with custom commands, shells, and authentication handlers

### 🏗️ Architecture Overview

- **Interface Layer** — `ICommand`, `IShell`, `IAuthenticator` define the core contracts
- **Shell Engine** — Registry, loop, templates, and API build system
- **Command Discovery** — Automatic reflection-based loading, external command registration
- **Logging & UI** — Colored console logging, table rendering, clear/refresh utilities

### 📦 Use Cases

- **CLI Tools** — Build powerful command-line utilities
- **DevOps Tools** — Interactive scripting and automation shells
- **Game Consoles** — Admin panels or debug consoles for game engines
- **Educational Shells** — Learn command design patterns through hands-on shell building

### 🔧 Core Dependencies

- **.NET 10** — Minimum required runtime
- **System.Reflection** — Command discovery and dynamic loading
- **System.Text.Json** — Configuration and state serialization

---

## 📦 Installation

### Via NuGet Package

```
dotnet add package BoxaraXLibrary.GenenicLib.LTS
```

### Via Project Reference

```
<ProjectReference Include="..\BoxaraXLibrary.GenenicLib.LTS\BoxaraXLibrary.GenenicLib.LTS.csproj" />
```

---

## 🛠️ Build the DLL

### 1. Clone the repository

```
git clone https://github.com/your-repo/BoxaraXLibrary.GenenicLib.LTS.git
cd BoxaraXLibrary.GenenicLib.LTS
```

### 2. Build the project

Using **.NET CLI**:

```
dotnet build -c Release
```

Or using **Visual Studio**:

- Open `BoxaraXLibrary.GenenicLib.LTS.sln`
- Set build configuration to **Release**
- Build the solution (Ctrl+Shift+B)

### 3. Locate the DLL

```
BoxaraXLibrary.GenenicLib.LTS/bin/Release/net10.0/BoxaraXLibrary.GenenicLib.LTS.dll
```

### 4. (Optional) Pack to NuGet

```
dotnet pack -c Release
```

---

## 🚀 Quick Start

### 1. Implement a Shell

```
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

public class MyShell : IShell
{
    public string ShellName => "MyShell";
    public string DisplayName => "My Custom Shell";
    public string Description => "A custom shell example";
    public string Category => "Demo";
    public string ShellVersion => "1.0.0";

    public void Execute()
    {
        ShelliftAPIBuild.Create()
            .SelectCommandShellLoad(ShellName)
            .WithTitle("My Shell", "Starting custom shell...")
            .SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome to MyShell!\n")
            .SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
            .WithAppName("MyApp")
            .WithAppVersion("1.0.0")
            .Build();
    }
}
```

### 2. Implement a Command

```
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;

public class HelloCommand : ICommand
{
    public string Name => "hello";
    public string DisplayName => "Say Hello";
    public string[] Aliases => new[] { "hi" };
    public string Category => "Demo";
    public string Shell => "MyShell";
    public string Description => "Prints a greeting message";
    public string CommandVersion => "1.0.0";
    public string[] Parameter => Array.Empty<string>();

    public void Execute()
    {
        Console.WriteLine("Hello, World!");
    }

    public void ParameterExecute(string[] args) { }
}
```

### 3. Register and Run

```
ShellRegistry.Initialize();
ShelliftAPIBuild.OpenShellWithResult("MyShell");
```

---

## 🧩 Core Interfaces

### IShell

| Property | Description |
| --- | --- |
| `ShellName` | Unique identifier for the shell |
| `DisplayName` | User-friendly name |
| `Description` | Brief description |
| `Category` | Grouping category |
| `ShellVersion` | Version string |

### ICommand

| Property | Description |
| --- | --- |
| `Name` | Command name (used in CLI) |
| `DisplayName` | User-friendly name |
| `Aliases` | Alternative command names |
| `Category` | Grouping category |
| `Shell` | Target shell name |
| `Description` | Brief description |
| `CommandVersion` | Version string |
| `Parameter` | List of supported parameters |

---

## 🛠️ Shell API (Fluent)

```
ShelliftAPIBuild.Create()
    .SelectCommandShellLoad("MyShell")
    .WithTitle("My App", "Starting...")
    .SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
    .WithExtraHeaderInfo("External commands: 5")
    .SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
    .WithAppName("MyApp")
    .WithAppVersion("1.0.0")
    .Build();
```

---

## 🎨 Header Styles

- `Classic` — ===== App v1.0.0 =====
- `DoubleLine` — ═══ App v1.0.0 ═══
- `StarBorder` — *** App v1.0.0 ***
- `Boxed` — Box with borders
- `Minimal` — Plain text
- `Modern` — Clean and flat
- `Cyber` — Cyberpunk style
- `Matrix` — Matrix green style
- `Neon` — Neon glow style
- `Retro` — Retro 80s style
- ... and more

## 💬 Prompt Styles

- `Default` — BoxaraHS>
- `Linux` — [user@hostname]$
- `FullInfo` — [user@hostname BoxaraHS]>
- `Minimal` — $
- `SimpleArrow` — ➜ BoxaraHS $
- `Brackets` — [BoxaraHS]>
- `DoubleArrow` — >> BoxaraHS >>

---

## 📝 Logging

```
LogConsole.WriteLine("Hello", DateTime.Now.ToString());
LogConsole.Clear(IsShowShell: true);
LogConsole.ForegroundColor = ConsoleColor.Green;
```

---

## 🚀 Advanced / Senior

  **🎯 Target Audience:** Senior developers, architects, and engineers building complex CLI applications or extending the framework itself.

### 🧠 Reflection-Based Command Discovery

The framework uses **assembly scanning** to automatically discover and register commands. All types implementing `ICommand` are automatically loaded via reflection.

```
// Assembly scanning example
var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
    .Where(t => t.GetCustomAttribute<NonLoadableCommandAttribute>() == null);

foreach (var type in commandTypes)
{
    var instance = (ICommand)Activator.CreateInstance(type);
    RegisterCommand(instance);
}
```

### 🧩 External Command Registration

Use `ExternalCommandManager` to inject commands from external sources into the shell without modifying the core assembly.

```
// Register external commands
ExternalCommandManager.RegisterExternalCommands(externalCommands);

// Retrieve merged command list
var allCommands = ExternalCommandManager.MergeCommands(coreCommands);
```

### 📦 AssemblyLoadContext for Dynamic Loading

Use **collectible AssemblyLoadContext** to load external assemblies with proper isolation and unloadability.

```
var context = new AssemblyLoadContext("ExternalContext", isCollectible: true);
var assembly = context.LoadFromAssemblyPath(assemblyPath);
var instance = (ICommand)Activator.CreateInstance(type);

// Unload when no longer needed
context.Unload();
```

### 🛠️ Custom Header & Prompt Styles

Extend `ShellHeaderTemplate` and `CommandPromptTemplate` to implement custom UI styles.

```
// Custom header style
ShelliftAPIBuild.Create()
    .SelectShellHeaderTemplate(HeaderStyle.Custom, "Custom header")
    .WithCustomHeader("[ CUSTOM HEADER ]")
    .Build();

// Custom prompt style
var customPrompt = CommandPromptTemplate.GetCustomPrompt("[MyApp]", ConsoleColor.Magenta);
```

### 📄 TableFormatter for Structured Output

Use `TableFormatterTemplate` to render structured tabular data with dynamic column widths and colors.

```
var table = new TableFormatterTemplate();
table.AddColumn("Name", ConsoleColor.Yellow, 15);
table.AddColumn("Value", ConsoleColor.Cyan, 20);
table.AddRow("Key1", "Value1");
table.AddRow("Key2", "Value2");
table.Render();
```

### 🔐 QuestionShellTemplate for User Interaction

Prompt users with timeout support and customizable confirmation/cancel keys.

```
var confirmed = QuestionShellTemplate.ShowQuestion(
    message: "Do you want to proceed?",
    confirmText: "Y",
    cancelText: "N",
    timeoutSeconds: 10,
    timeoutMessage: "Operation timed out.",
    continueOnTimeout: false
);

if (confirmed)
{
    // Proceed with operation
}
```

### 🧵 LogConsole with Shell-Aware Clearing

Use `LogConsole.Clear()` with `IsShowShell` to automatically re-render the current shell after clearing.

```
// Clear and re-display the current shell
LogConsole.Clear(IsShowShell: true);
```

### 🧠 ReflectionShellTemplate for Shell Discovery

Programmatically discover shells and retrieve the current active shell instance.

```
// Get current shell name
string currentShell = ReflectionShellTemplate.GetCurrentShell();

// Get current shell object
var shell = ReflectionShellTemplate.GetCurrentShellObject();

// List all available shells
string[] shellNames = ReflectionShellTemplate.GetShellNames();
```

### ⚡ Performance Considerations

- **Assembly scanning** is cached after first load via `ShellRegistry.Initialize()`.
- **External commands** are stored in a static `List<ICommand>` with thread-safe locking.
- **Reflection** is minimized by caching method handles and property getters.
- **Logging** uses direct console output to avoid buffering overhead.

### 🧪 Extensibility Points

- **ICommand** — Implement custom commands
- **IShell** — Implement custom shells
- **IAuthenticator** — Implement custom authentication providers
- **ExternalCommandManager** — Inject external commands dynamically
- **ShelliftAPIBuild** — Extend fluent API with custom builder methods
- **ErrorShellTemplate** — Customize error messages and user feedback

---

## ⚠️ IMPORTANT — Framework Critical Notes

  **🔴 MUST READ:** These are critical considerations for using the framework in production-grade applications.

### 🧠 Assembly Loading & Memory Management

The framework uses **AssemblyLoadContext** for external assembly loading. When loading external commands, always use a collectible context.

```
// ✅ CORRECT: Using collectible context
var context = new AssemblyLoadContext($"ExternalContext_{Guid.NewGuid()}", isCollectible: true);
var assembly = context.LoadFromAssemblyPath(dllPath);
// ... use assembly ...
context.Unload();  // ⚠️ MUST call Unload() to free memory

// ❌ WRONG: Loading into default context (cannot unload)
var assembly = Assembly.LoadFrom(dllPath);  // MEMORY LEAK!
```

  **🔴 Critical:** Using `Assembly.LoadFrom` or `Assembly.LoadFile` without a collectible context will cause **memory leaks** because the assembly cannot be unloaded. Always use `AssemblyLoadContext` for external assemblies.

### ⚡ Performance Considerations

| Area | Recommendation | Impact |
| --- | --- | --- |
| **Command Discovery** | Call `ShellRegistry.Initialize()` once at startup. Results are cached. | 🟢 Reduces reflection overhead |
| **External Command Registration** | Use `ExternalCommandManager` with `lock` for thread safety. | 🟢 O(1) lookups, thread-safe |
| **Reflection** | Cache `MethodInfo`, `PropertyInfo`, and `ConstructorInfo` when possible. | 🟡 Significant performance gain |
| **Logging** | `LogConsole` writes directly to console. Avoid excessive calls in tight loops. | 🟡 Can impact throughput |
| **TableFormatter** | Renders synchronously. For large datasets, consider pagination. | 🟡 Memory allocation per render |

### 🧵 Thread Safety

- **`ExternalCommandManager`** is thread-safe with `lock (_lock)`.
- **`ShellRegistry`** is thread-safe after initialization.
- **Commands** are executed on the caller thread — implement your own threading if needed.

```
// ✅ Thread-safe external command registration
ExternalCommandManager.RegisterExternalCommands(commands);

// ⚠️ Commands are NOT thread-safe by default
public void Execute()
{
    // If your command is multi-threaded, implement locking yourself
    lock (_commandLock)
    {
    // ... critical section ...
    }
}
```

### 🧩 Shell & Command Compatibility

- **Shell validation** ensures commands target existing shells via `ShellExists()`.
- **Command name conflicts** are detected and rejected during registration.
- **Commands with no shell** are skipped automatically.

```
// ✅ Framework validates shell existence
if (!ReflectionShellTemplate.ShellExists(shellName))
{
    // Command will not be registered
}
```

### 🔄 Framework Lifecycle

| Phase | Action | Who |
| --- | --- | --- |
| **Initialization** | `ShellRegistry.Initialize()` discovers all `IShell` and `ICommand` types | Framework |
| **Shell Build** | `ShelliftAPIBuild.Build()` creates and runs the shell | Framework |
| **Command Processing** | `CommandProcessorTemplate.Process()` handles input, merges external commands | Framework |
| **External Commands** | `ExternalCommandManager` holds commands registered by the app | App (via framework API) |

### 🔧 Diagnostics & Debugging

- **Enable verbose logging** by using `LogConsole` with appropriate log levels.
- **Use `ReflectionShellTemplate.GetCurrentShell()`** to inspect the active shell.
- **Check `ExternalCommandManager.GetExternalCommands()`** to verify external registrations.

```
// Diagnostic examples
var currentShell = ReflectionShellTemplate.GetCurrentShell();
var externalCount = ExternalCommandManager.GetExternalCommands().Count;
```

### 📌 Summary of Critical Rules

1. **✅ ALWAYS** use `AssemblyLoadContext` for external assembly loading
2. **✅ ALWAYS** call `Unload()` when done with external assemblies
3. **✅ ALWAYS** use `ExternalCommandManager` to register external commands
4. **✅ ALWAYS** check `ShellExists()` before registering commands for a shell
5. **✅ ALWAYS** call `ShellRegistry.Initialize()` before opening a shell
6. **❌ NEVER** use `Assembly.LoadFrom` without a collectible context
7. **❌ NEVER** hardcode shell names in framework extensions
8. **❌ NEVER** hold references to types from unloaded assemblies
9. **❌ NEVER** ignore thread safety when accessing shared state
10. **❌ NEVER** call `Build()` from a non-`IShell` class

---

## 📄 License

This project is licensed under the **Apache License 2.0**.

  Apache License Version 2.0, January 2004 http://www.apache.org/licenses/ TERMS AND CONDITIONS FOR USE, REPRODUCTION, AND DISTRIBUTION 1. Definitions. "License" shall mean the terms and conditions for use, reproduction, and distribution as defined by Sections 1 through 9 of this document. "Licensor" shall mean the copyright owner or entity authorized by the copyright owner that is granting the License. "Legal Entity" shall mean the union of the acting entity and all other entities that control, are controlled by, or are under common control with that entity. For the purposes of this definition, "control" means (i) the power, direct or indirect, to cause the direction or management of such entity, whether by contract or otherwise, or (ii) ownership of fifty percent (50%) or more of the outstanding shares, or (iii) beneficial ownership of such entity. "You" (or "Your") shall mean an individual or Legal Entity exercising permissions granted by this License. "Source" form shall mean the preferred form for making modifications, including but not limited to software source code, documentation source, and configuration files. "Object" form shall mean any form resulting from mechanical transformation or translation of a Source form, including but not limited to compiled object code, generated documentation, and conversions to other media types. "Work" shall mean the work of authorship, whether in Source or Object form, made available under the License, as indicated by a copyright notice that is included in or attached to the work (an example is provided in the Appendix below). "Derivative Works" shall mean any work, whether in Source or Object form, that is based on (or derived from) the Work and for which the editorial revisions, annotations, elaborations, or other modifications represent, as a whole, an original work of authorship. For the purposes of this License, Derivative Works shall not include works that remain separable from, or merely link (or bind by name) to the interfaces of, the Work and Derivative Works thereof. "Contribution" shall mean any work of authorship, including the original version of the Work and any modifications or additions to that Work or Derivative Works thereof, that is intentionally submitted to Licensor for inclusion in the Work by the copyright owner or by an individual or Legal Entity authorized to submit on behalf of the copyright owner. For the purposes of this definition, "submitted" means any form of electronic, verbal, or written communication sent to the Licensor or its representatives, including but not limited to communication on electronic mailing lists, source code control systems, and issue tracking systems that are managed by, or on behalf of, the Licensor for the purpose of discussing and improving the Work, but excluding communication that is conspicuously marked or otherwise designated in writing by the copyright owner as "Not a Contribution." "Contributor" shall mean Licensor and any individual or Legal Entity on behalf of whom a Contribution has been received by Licensor and subsequently incorporated within the Work. 2. Grant of Copyright License. Subject to the terms and conditions of this License, each Contributor hereby grants to You a perpetual, worldwide, non-exclusive, no-charge, royalty-free, irrevocable copyright license to reproduce, prepare Derivative Works of, publicly display, publicly perform, sublicense, and distribute the Work and such Derivative Works in Source or Object form. 3. Grant of Patent License. Subject to the terms and conditions of this License, each Contributor hereby grants to You a perpetual, worldwide, non-exclusive, no-charge, royalty-free, irrevocable (except as stated in this section) patent license to make, have made, use, offer to sell, sell, import, and otherwise transfer the Work, where such license applies only to those patent claims licensable by such Contributor that are necessarily infringed by their Contribution(s) alone or by combination of their Contribution(s) with the Work to which such Contribution(s) was submitted. If You institute patent litigation against any entity (including a cross-claim or counterclaim in a lawsuit) alleging that the Work or a Contribution incorporated within the Work constitutes direct or contributory patent infringement, then any patent licenses granted to You under this License for that Work shall terminate as of the date such litigation is filed. 4. Redistribution. You may reproduce and distribute copies of the Work or Derivative Works thereof in any medium, with or without modifications, and in Source or Object form, provided that You meet the following conditions: (a) You must give any other recipients of the Work or Derivative Works a copy of this License; and (b) You must cause any modified files to carry prominent notices stating that You changed the files; and (c) You must retain, in the Source form of any Derivative Works that You distribute, all copyright, patent, trademark, and attribution notices from the Source form of the Work, excluding those notices that do not pertain to any part of the Derivative Works; and (d) If the Work includes a "NOTICE" text file as part of its distribution, then any Derivative Works that You distribute must include a readable copy of the attribution notices contained within such NOTICE file, excluding those notices that do not pertain to any part of the Derivative Works, in at least one of the following places: within a NOTICE text file distributed as part of the Derivative Works; within the Source form or documentation, if provided along with the Derivative Works; or, within a display generated by the Derivative Works, if and wherever such third-party notices normally appear. The contents of the NOTICE file are for informational purposes only and do not modify the License. You may add Your own attribution notices within Derivative Works that You distribute, alongside or as an addendum to the NOTICE text from the Work, provided that such additional attribution notices cannot be construed as modifying the License. You may add Your own copyright statement to Your modifications and may provide additional or different license terms and conditions for use, reproduction, or distribution of Your modifications, or for any such Derivative Works as a whole, provided Your use, reproduction, and distribution of the Work otherwise complies with the conditions stated in this License. 5. Submission of Contributions. Unless You explicitly state otherwise, any Contribution intentionally submitted for inclusion in the Work by You to the Licensor shall be under the terms and conditions of this License, without any additional terms or conditions. Notwithstanding the above, nothing herein shall supersede or modify the terms of any separate license agreement you may have executed with Licensor regarding such Contributions. 6. Trademarks. This License does not grant permission to use the trade names, trademarks, service marks, or product names of the Licensor, except as required for reasonable and customary use in describing the origin of the Work and reproducing the content of the NOTICE file. 7. Disclaimer of Warranty. Unless required by applicable law or agreed to in writing, Licensor provides the Work (and each Contributor provides its Contributions) on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied, including, without limitation, any warranties or conditions of TITLE, NON-INFRINGEMENT, MERCHANTABILITY, or FITNESS FOR A PARTICULAR PURPOSE. You are solely responsible for determining the appropriateness of using or redistributing the Work and assume any risks associated with Your exercise of permissions under this License. 8. Limitation of Liability. In no event and under no legal theory, whether in tort (including negligence), contract, or otherwise, unless required by applicable law (such as deliberate and grossly negligent acts) or agreed to in writing, shall any Contributor be liable to You for damages, including any direct, indirect, special, incidental, or consequential damages of any character arising as a result of this License or out of the use or inability to use the Work (including but not limited to damages for loss of goodwill, work stoppage, computer failure or malfunction, or any and all other commercial damages or losses), even if such Contributor has been advised of the possibility of such damages. 9. Accepting Warranty or Additional Liability. While redistributing the Work or Derivative Works thereof, You may choose to offer, and charge a fee for, acceptance of support, warranty, indemnity, or other liability obligations and/or rights consistent with this License. However, in accepting such obligations, You may act only on Your own behalf and on Your sole responsibility, not on behalf of any other Contributor, and only if You agree to indemnify, defend, and hold each Contributor harmless for any liability incurred by, or claims asserted against, such Contributor by reason of your accepting any such warranty or additional liability. END OF TERMS AND CONDITIONS APPENDIX: How to apply the Apache License to your work. To apply the Apache License to your work, attach the following boilerplate notice, with the fields enclosed by brackets "[]" replaced with your own identifying information. (Don't include the brackets!) The text should be enclosed in the appropriate comment syntax for the file format. We also recommend that a file or class name and description of purpose be included on the same "printed page" as the copyright notice for easier identification within third-party archives. Copyright (c) 2026 JuliHyro Studios Workspace Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

---

**Author:** JuliHyro Studios Workspace
 **Project:** BoxaraXLibrary.GenenicLib.LTS
 **Version:** 1.0.0-LTS
