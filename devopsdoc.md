# 📚 BoxaraXLibrary.GenenicLib.LTS — Development Documentation

[![NuGet](https://img.shields.io/nuget/v/BoxaraXLibrary.GenenicLib.LTS?style=for-the-badge&logo=nuget&color=004880)](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS)
[![NuGet Downloads](https://img.shields.io/nuget/dt/BoxaraXLibrary.GenenicLib.LTS?style=for-the-badge&logo=nuget&color=004880)](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS)
[![GitHub Repo](https://img.shields.io/badge/GitHub-Repo-181717?style=for-the-badge&logo=github)](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS)
[![.NET Version](https://img.shields.io/badge/.NET-7.0%20%7C%208.0%20%7C%209.0%20%7C%2010.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=for-the-badge)](https://www.apache.org/licenses/LICENSE-2.0)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-lightgrey?style=for-the-badge)](https://dotnet.microsoft.com/)

---

| Property | Value |
|----------|-------|
| **Author** | JuliHyro Studios Workspace |
| **License** | Apache 2.0 |
| **Target Frameworks** | .NET 7.0, 8.0, 9.0, 10.0 |
| **Repository** | [GitHub](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS) |
| **NuGet** | [BoxaraXLibrary.GenenicLib.LTS](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS) |

---

## 📖 Table of Contents

1. [Overview](#overview)
2. [Getting Started](#getting-started)
3. [Contributing](#contributing)
4. [Testing](#testing)
5. [API Reference (Quick Summary)](#api-reference-quick-summary)
6. [Troubleshooting](#troubleshooting)
7. [Project Structure](#project-structure)
8. [Core Interfaces](#core-interfaces)
9. [Shell Engine](#shell-engine)
10. [Command System](#command-system)
11. [Error Handling Templates](#error-handling-templates)
12. [External Command Loading](#external-command-loading)
13. [Authentication System (removed in v1.0.7.3)](#authentication-system-removed-in-v1073)
14. [Utility Helpers](#utility-helpers)
15. [Logging System](#logging-system)
16. [Real-Time Logging (LogManager)](#real-time-logging-logmanager)
17. [UI Components](#ui-components)
18. [Fluent API Reference](#fluent-api-reference)
19. [Shell Events](#shell-events)
20. [Delegate Hooks](#delegate-hooks)
21. [Performance Considerations](#performance-considerations)
22. [Thread Safety](#thread-safety)
23. [Extensibility Points](#extensibility-points)
24. [Advanced Examples](#advanced-examples)
25. [Best Practices](#best-practices)
26. [FAQ](#faq)
27. [Changelog](#changelog)

---

## Overview

**BoxaraXLibrary.GenenicLib.LTS** is a lightweight, high-performance framework for building shell-based CLI applications in .NET. It provides:

- Shell engine with customizable prompts and headers
- Command registration and discovery via reflection
- External command loading (`ExternalCommandManager`)
- Fluent API for shell building
- Rich console UI with 16+ header styles and 10+ prompt styles
- Real-time logging with `LogManager`
- Thread-safe design
- Zero external dependencies
- Cross-platform support (.NET 7, 8, 9, 10)

## 🚀 Getting Started

### Prerequisites

- .NET SDK 7.0 or higher
- Visual Studio 2022, VS Code, or Rider

### Build from Source

```bash
# Clone the repository
git clone https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS
cd BoxaraXLibrary.GenenicLib.LTS

# Build the library
dotnet build -c Release

# Pack to NuGet
dotnet pack -c Release -o ./nupkgs
```

The library targets multiple frameworks (`net7.0`, `net8.0`, `net9.0`, and
`net10.0`). Build artifacts are generated under `bin/Release/netx.x/`, where
`netx.x` represents the target framework being inspected; the documentation does
not prescribe a single framework-specific output directory.

> **Note:** `dotnet test` requires a test project to be present in the solution.

## 🛠️ Internal Testing Project

The repository includes a pre-configured test project to verify the library's functionality without needing to set up a separate application.

### Running the Test Shell
To quickly verify the current state of the library, you can run the built-in test console:

```bash
dotnet run --project ./LibraryTestestProjects/ConsoleApp
```

> **Important Note:** `LibraryTestestProjects` is an internal project used exclusively for library development, verification, and demonstration. It is **not** part of the NuGet distribution and is not intended to be forked or modified by end-users.

### First App

```csharp
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

// 1. Implement a command
public class HelloCommand : ICommand
{
	public string Name => "hello";
	public string DisplayName => "Say Hello";
	public string[] Aliases => new[] { "hi" };
	public string Category => "Demo";
	public string Shell => "MainShell";
	public string Description => "Prints a greeting";
	public string CommandVersion => "1.0.0";
	public string[] Parameter => Array.Empty<string>();

	public void Execute()
	{
		Console.WriteLine("Hello, World!");
	}

	public void ParameterExecute(string[] args)
	{
	}
}

// 2. Register and run
ShellRegistry.Initialize();
ShelliftAPIBuild.OpenShellWithResult("MainShell");
```

---

## 👥 Contributing

### Code Style

- Use **PascalCase** for public members.
- Use **camelCase** for private fields.
- Prefer **explicit types** when possible.
- Add XML documentation for public APIs.
- Use `#nullable enable` as the project default.

### Commit Convention

```text
feat: Add new feature
fix: Fix bug
docs: Update documentation
refactor: Code refactoring
test: Add or update tests
chore: Maintenance tasks
```

### Pull Request Process

1. Fork the repository.
2. Create a feature branch:

```bash
   git checkout -b feature/my-feature
```

3. Commit your changes.
4. Push the branch to your fork.
5. Create a Pull Request.

---

## 🧪 Testing

### Running Tests

```bash
dotnet test
```

### Writing Tests

The following example uses xUnit-style syntax:

```csharp
[Fact]
public void Command_ShouldExecuteSuccessfully()
{
	var command = new HelloCommand();

	command.Execute();

	// Assert...
}
```

> **Note:** The source document does not define a specific test framework or test project structure beyond this example.

---

## 📚 API Reference (Quick Summary)

### ⚠️ Internal Components (Framework Only)
The following components are marked as `internal` and are managed automatically by the framework. They are **not accessible** to Dev-Apps:
- `ErrorShellTemplate` — Handles the internal rendering of standardized error messages.
- `LogConsole` — Internal console renderer used by LogManager and Shell Engine.
- `codeint` — Defines core system return codes used for internal logic.

> **Version compatibility:** `ErrorShellTemplate` became `internal` in v1.0.7.1 and
> `LogConsole` became `internal` in v1.0.7.3. The historical public examples below
> are retained in collapsed sections for applications using older package versions.

### Core Interfaces
...existing code...

- `ICommand` — Command contract
- `IShell` — Shell contract

### Shell Building

- `ShelliftAPIBuild` — Fluent API builder
- `ShellRegistry` — Shell registration and discovery
- `ShellLoopTemplate` — Main shell loop

### Command Processing

- `CommandProcessorTemplate` — Command execution
- `ExternalCommandManager` — External command loading
- `ReflectionCommandShellTemplate` — Automatic command discovery

### Logging

- `LogManager` — Real-time logging system

### UI

- FormatterTemplate` — Structured table output
- `QuestionShellTemplate` — User interaction and confirmation prompts
- `HeaderStyle` / `PromptStyle` — Built-in UI styles

---

## 🔧 Troubleshooting

### Common Issues

| **Issue** | **Solution** |
| --- | --- |
| **`ShelliftAPIBuild.Create()` throws an exception** | Call it from an `IShell` implementation. |
| **Commands are not found** | Check that the `Shell` property matches an existing shell name. |
| **Logs are not appearing** | Use `LogManager.Log()` instead of `Console.WriteLine()` while the shell is running. |
| **Prompt is not rendering** | Check that `ShellLoopTemplate` is running. |
| **`AssemblyLoadContext` memory leak** | Use a collectible `AssemblyLoadContext` and always call `Unload()` when finished. |
| **`codeint` warning CS8981** | The source documentation describes this as intentional naming; verify the project configuration before suppressing the warning. |

### Debugging

```csharp
// Enable verbose logging
LogManager.Log("[DEBUG] Debug message");

// Check active commands
var commands = ReflectionCommandShellTemplate.GetCommandAllInterface();

// Check external commands
var external = ExternalCommandManager.GetExternalCommands();

// Check current shell
var shell = ReflectionShellTemplate.GetCurrentShell();
```

---

## Project Structure

```
BoxaraXLibrary.GenenicLib.LTS/
├── Commons/
│   ├── basicUtils/ # Utility helpers
│   │   ├── ConvertSymbolUniverse.cs
│   ├── Interface/ # Core contracts
│   │   ├── ICommand.cs
│   │   ├── IShellExecute.cs (IShell)
│   │   └── NonLoadableCommandAttribute.cs
│   ├── Log/ # Logging system
│   │   ├── LogConsole.cs
│   │   └── LogManager.cs
│   └── ShellHandle/ # Shell engine
│       ├── CommandProcessorTemplate.cs
│       ├── CommandPromptTemplate.cs
│       ├── CommandPromptTitleSEt.cs
│       ├── ErrorShellTemplate.cs
│       ├── ExternalCommandManager.cs
│       ├── QuestionShellTemplate.cs
│       ├── ReflectionCommandShellTemplate.cs
│       ├── ReflectionShellTemplate.cs
│       ├── ShellHeaderTemplate.cs
│       ├── ShelliftAPIBuild.cs
│       ├── ShellLoopTemplate.cs
│       ├── ShellRegistry.cs
│       └── TableFormatterTemplate.cs
├── CallDll.cs # Library availability helper
├── codeint.cs # Return codes (internal)
├── LibraryTestestProjects/ # Internal developer verification projects
│   └── ConsoleApp/ # Manual shell test application
├── LICENSE.txt # Apache 2.0 license
├── README.md # User documentation
└── BoxaraXLibrary.GenenicLib.LTS.csproj
```

> **Current structure:** The tree above describes v1.0.7.3 and later. The
> `LibraryTestestProjects` directory belongs to the repository's developer
> tooling and is excluded from the library assembly compilation and NuGet API.

<details>
<summary><strong>DESCRIBED BY v1.0.7.2 AND EARLIER — historical authentication structure</strong></summary>

Before v1.0.7.3, the repository also contained the authentication subsystem:

```
Commons/
├── AuthHandle/
│   ├── AuthenticationHelper.cs
│   └── ReflectionAuthenticatorTemplate.cs
├── Interface/
│   └── IAuthenticator.cs
└── basicUtils/
	└── AuthMode.cs
```

These files were removed in v1.0.7.3 together with the `IAuthenticator`,
`AuthHandle`, and `AuthMode` APIs. This historical tree is retained for
maintainers working with package versions that still included Authentication.

</details>

---

## Core Interfaces

### `ICommand`

The contract for all commands in the framework.

```csharp
public interface ICommand
{
	string Name { get; }
	string DisplayName { get; }
	string[] Aliases { get; }
	string Category { get; }
	string Shell { get; }
	string Description { get; }
	string CommandVersion { get; }
	string[] Parameter { get; }

	void Execute();
	void ParameterExecute(string[] args);
}
```

**Usage:**

```csharp
public class MyCommand : ICommand
{
	public string Name => "mycommand";
	public string DisplayName => "My Custom Command";
	public string[] Aliases => new[] { "mc", "my" };
	public string Category => "General";
	public string Shell => "MainShell";
	public string Description => "This is my custom command";
	public string CommandVersion => "1.0.0";
	public string[] Parameter => new[] { "arg1", "arg2" };

	public void Execute()
	{
		Console.WriteLine("Command executed without parameters");
	}

	public void ParameterExecute(string[] args)
	{
		if (args.Length < 2)
		{
			throw new ArgumentException("Expected 2 arguments");
		}
		Console.WriteLine($"Executed with args: {string.Join(", ", args)}");
	}
}
```

---

### `IShell`

The contract for shell implementations.

```csharp
public interface IShell
{
	string ShellName { get; }
	string DisplayName { get; }
	string Description { get; }
	string Category { get; }
	string ShellVersion { get; }
	void Execute();
}
```

**Usage:**

```csharp
public class MyShell : IShell
{
	public string ShellName => "MyShell";
	public string DisplayName => "My custom shell";
	public string Description => "A professional shell implementation";
	public string Category => "Custom";
	public string ShellVersion => "1.0.0";

	public void Execute()
	{
		ShelliftAPIBuild.Create()
			.SelectCommandShellLoad(ShellName)
			.WithTitle("My Shell", "Starting...")
			.SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
			.SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
			.WithAppName("MyApp")
			.WithAppVersion("1.0.0")
			.Build();
	}
}
```

---

## Authentication System (Removed in v1.0.7.3)

<details>
<summary><strong>DESCRIBED BY v1.0.7.3</strong></summary>

The authentication system was removed in v1.0.7.3. The following API is retained here only as historical documentation and is not available in the current framework:

```csharp
public interface IAuthenticator
{
	AuthMode Mode { get; }
	string DisplayName { get; }
	string Description { get; }

	bool Authenticate(string prompt, int timeRedirect);
}
```

`IAuthenticator`, `AuthHandle`, and `AuthMode` are no longer part of the library. Do not use the following historical example with current versions:

```csharp
public class SimpleAuthenticator : IAuthenticator
{
	public AuthMode Mode => AuthMode.Local;
	public string DisplayName => "SimpleAuth";
	public string Description => "A basic local authenticator";

	public bool Authenticate(string prompt, int timeRedirect)
	{
		Console.Write(prompt);
		var password = Console.ReadLine();
		return password == "password123";
	}
}
```

</details>

---

### `NonLoadableCommandAttribute`

Prevents a command from being auto-loaded by reflection.

```csharp
[NonLoadableCommand]
public class HiddenCommand : ICommand
{
	// This command won't be auto-discovered
}
```

---

## Shell Engine

### `ShelliftAPIBuild` (Fluent API)

The main builder for creating and configuring shells.

```csharp
public sealed class ShelliftAPIBuild
{
	// --- Quick Launch Methods ---

	// Open a shell immediately using default settings
	public static void OpenShell(string shellName) { }

	// Open a shell and return the exit code
	public static int OpenShellWithResult(string shellName) { }

	// Open a shell with custom inline configuration
	public static void OpenShell(string shellName, Action<ShelliftAPIBuild>? config) { }

	// Open a shell with config and return detailed result (Code & Message)
	public static (int Code, string Message) OpenShellWithResult(string shellName, Action<ShelliftAPIBuild>? config) { }

	// --- Fluent Configuration ---

	// Create a new shell configuration (Must be called from an IShell implementation)
	public static ShelliftAPIBuild Create() { }

	// Load commands from a specific shell registration
	public ShelliftAPIBuild SelectCommandShellLoad(string shellName) { }

	// Set window title and startup message (supports multiple reasons)
	public ShelliftAPIBuild WithTitle(string title, params string[] reasons) { }

	// Select built-in header style (e.g., HeaderStyle.Modern)
	public ShelliftAPIBuild SelectShellHeaderTemplate(HeaderStyle style, string? welcomeMessage = null) { }

	// Select custom header renderer logic
	public ShelliftAPIBuild SelectCustomHeader(Action renderHeader) { }

	// Select built-in prompt style (e.g., PromptStyle.FullInfo)
	public ShelliftAPIBuild SelectShellPrompt(PromptStyle style, string customName = "BoxaraHS") { }

	// Select custom prompt generator logic
	public ShelliftAPIBuild SelectCustomPrompt(Func<string> promptGenerator, ConsoleColor color = ConsoleColor.Cyan) { }

	// Set app metadata for display in prompts/headers
	public ShelliftAPIBuild WithAppName(string appName) { }
	public ShelliftAPIBuild WithAppVersion(string version) { }

	// Quick Header/Prompt overrides
	public ShelliftAPIBuild WithCustomHeader(string customHeader) { }
	public ShelliftAPIBuild WithExtraHeaderInfo(string extraInfo) { }
	public ShelliftAPIBuild WithCustomPromptText(string customText) { }

	// Added in v1.0.3: custom header/prompt support

	// Custom Input/Process Pipeline
	public ShelliftAPIBuild WithInputProvider(Func<string> inputProvider) { }
	public ShelliftAPIBuild WithPreProcessor(Action<string> preProcessor) { }
	public ShelliftAPIBuild WithPostProcessor(Action<string, bool> postProcessor) { }
	public ShelliftAPIBuild WithExitCondition(Func<bool> exitCondition) { }
	// Added in v1.0.4; WithInputProvider is retained for compatibility but is
	// not invoked by the current key-based shell loop.

	// Command Lifecycle Hooks (Fluent)
	public ShelliftAPIBuild WithCommandPreAction(Action<string, string[]> preAction) { }
	public ShelliftAPIBuild WithCommandPostAction(Action<string, string[], bool> postAction) { }

	// Global Shell Event Hooks (Fluent)
	public ShelliftAPIBuild OnShellStart(Action onStart) { }
	public ShelliftAPIBuild OnShellEnd(Action onEnd) { }
	public ShelliftAPIBuild OnShellError(Action<Exception> onError) { }
	public ShelliftAPIBuild OnCommandsLoaded(Action<List<ICommand>> onLoaded) { }
	public ShelliftAPIBuild OnCommandExecuted(Action<string> onExecuted) { }
	public ShelliftAPIBuild OnCommandFailed(Action<string> onFailed) { }
	public ShelliftAPIBuild OnPromptRendered(Action<string> onRendered) { }

	// Title Hooks
	public ShelliftAPIBuild WithTitlePreAction(Action<string, string[], DateTime, string> preAction) { }
	public ShelliftAPIBuild WithTitlePostAction(Action<string, string[], DateTime, string> postAction) { }

	// Build and run the shell (terminal blocking)
	public int Build() { }
}
```

**Version notes:** `SelectCustomHeader`, `SelectCustomPrompt`, and
`WithExtraHeaderInfo` were introduced in v1.0.3. The input and processor
callbacks were introduced in v1.0.4. The command and title hook methods were
introduced in v1.0.5. Existing applications may continue using the original
signatures documented in those releases; the current builder methods are the
supported form for new applications.

**Example:**

```csharp
ShelliftAPIBuild.Create()
	.SelectCommandShellLoad("MainShell")
	.WithTitle("My CLI App", "Initializing...")
	.SelectShellHeaderTemplate(HeaderStyle.Minimal)
	.SelectShellPrompt(PromptStyle.Simple, ">>")
	.WithAppName("MyApp")
	.WithAppVersion("2.0.0")
	.Build();
```

---

### `ShellLoopTemplate`

Manages the main command input loop with real-time prompt rendering. Since v1.0.6,
the default loop reads individual keys with `Console.ReadKey` and polls
`Console.KeyAvailable`; it no longer uses `Console.ReadLine` for normal shell input.
The `inputProvider` parameter remains in the signature for compatibility with the
v1.0.4 API, but the current loop does not invoke it. Since v1.0.6, this provider
is completely disconnected from the execution logic in favor of the key-based loop.

```csharp
public static class ShellLoopTemplate
{
	public static void Run(
		List<PromptSegment> segments,
		List<ICommand> commands,
		string shellName,
		Func<string>? inputProvider = null,
		Action<string>? preProcessor = null,
		Action<string, bool>? postProcessor = null,
		Func<bool>? exitCondition = null,
		Action<string, string[]>? commandPreAction = null,
		Action<string, string[], bool>? commandPostAction = null
	)
	{ }
}
```

---

### `ShellRegistry`

Manages shell discovery and registration.

```csharp
public static class ShellRegistry
{
	// Initialize the registry with all available shells
	public static int Initialize() { }

	// Get all registered shells
	public static (int code, List<IShell> shells) GetAllShells() { }

	// Open and execute a shell by name
	public static int OpenShell(string name) { }
}
```

---

## Command System

### `CommandProcessorTemplate`

Handles command execution and error handling.

```csharp
public static class CommandProcessorTemplate
{
	public static bool Process(
		string input,
		List<ICommand> commands,
		Action<string, string[]>? preAction = null,
		Action<string, string[], bool>? postAction = null) { }

	public static void WithCommandPreAction(Action<ICommand> action) { }
	public static void WithCommandPostAction(Action<ICommand> action) { }
}
```

#### Duplicate command selection (v1.0.7.2)

When multiple different command types match the same name or alias, the processor
shows a selection menu containing each command's display name, assembly, type,
aliases, and description. Instances of the same runtime type are deduplicated.
The user can select a command by number or enter `exit`/`cancel` to stop without
executing one.

---

### `ReflectionCommandShellTemplate`

Uses reflection to discover and load commands.

```csharp
public static class ReflectionCommandShellTemplate
{
	// Get all commands implementing ICommand
	public static List<ICommand> GetCommandAllInterface() { }

	// Get commands filtered by shell
	public static List<ICommand> GetCommandsForShell(string shellName) { }
}
```

---

### External Command Loading

> **NOTE:** This is NOT a full plugin system. It's a mechanism to load additional commands at runtime.

To load external commands:

```csharp
ExternalCommandManager.RegisterExternalCommands(
	new List<ICommand>
	{
		new ExternalCommand1(),
		new ExternalCommand2()
	}
);
```

---

## Error Handling Templates

### `ErrorShellTemplate` (framework-internal since v1.0.7.1)

The framework uses this internal renderer for standardized command errors. Dev-Apps
cannot call it from current versions. Throw an appropriate exception from a command
and let `CommandProcessorTemplate` render the error response.

<details>
<summary><strong>DESCRIBED BY v1.0.7.1 — historical public API</strong></summary>

```csharp
public static class ErrorShellTemplate
{
	private static string GetCurrentTime() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

	public static void ShowCommandNotFound(string input, List<ICommand> allCommands) { }
	public static void ShowPrefixMatches(string input, List<ICommand> prefixMatches, List<ICommand> allCommands) { }
	public static void ShowCommandNotFound(string input) { }
	public static void ShowCommandInvalidParameter(string commandName, string details = "") { }
}
```

</details>

**Features:**

- **Command Not Found (with suggestions)** - Automatically suggests similar commands based on prefix matching
- **Prefix Matching Display** - Shows available commands that start with the user's input
- **Invalid Parameter Display** - Displays detailed error messages for incorrect parameters

**Note:** `ErrorShellTemplate` is used internally by `CommandProcessorTemplate`. Developers should throw appropriate exceptions within their commands instead of calling this type directly.

**Internal Implementation:**

- `GetCurrentTime()` - Formats current time as `yyyy-MM-dd HH:mm:ss`
- Uses the framework-internal renderer for colored output (Red for errors, Yellow for suggestions)
- Automatically counts and displays total available commands

---

## External Command Loading

### `ExternalCommandManager` API

Allows loading additional commands at runtime without modifying the main assembly.

```csharp
public static class ExternalCommandManager
{
	// Register external commands
	public static void RegisterExternalCommands(IEnumerable<ICommand> commands) { }

	// Get all registered external commands
	public static List<ICommand> GetExternalCommands() { }

	// Clear external commands
	public static void ClearExternalCommands() { }
}
```

**Use Cases:**

- Loading commands from a plugin directory
- Injecting commands from a different assembly
- Dynamic command registration at runtime
- Loading commands from configuration

**Example: Load Commands from a DLL**

```csharp
using System.Reflection;

// 1. Load assembly
var assembly = Assembly.LoadFrom("Plugins/MyPlugin.dll");

// 2. Find all ICommand implementations
var commandTypes = assembly.GetTypes()
	.Where(t => typeof(ICommand).IsAssignableFrom(t) && !t.IsInterface);

// 3. Create instances
var commands = commandTypes
	.Select(t => (ICommand)Activator.CreateInstance(t)!)
	.ToList();

// 4. Register
ExternalCommandManager.RegisterExternalCommands(commands);
```

---

## Utility Helpers

### `ConvertSymbolUniverse`

Provides masked console input through its nested `ConvertTextToAsterisk` helper.

```csharp
public class ConvertSymbolUniverse
{
	public class ConvertTextToAsterisk
	{
		public static (int code, string result) ReadMaskedInput() { }
	}
}
```

**Usage:**

```csharp
var (code, password) =
	ConvertSymbolUniverse.ConvertTextToAsterisk.ReadMaskedInput();
```

---

### `CallDll`

Provides a basic library availability message. It does not load DLLs or expose a
library version API.

```csharp
public class CallDll
{
	public static string IsAvailivable() { }
}
```

**Usage:**

```csharp
Console.WriteLine(CallDll.IsAvailivable());
```

---

## Logging System

### `LogConsole` (framework-internal since v1.0.7.3)

`LogConsole` is the framework's internal console renderer and is not accessible to
Dev-Apps in current versions. Use `LogManager.Log(...)` for messages and
`LogManager.Clear(...)` for screen clearing.

<details>
<summary><strong>DESCRIBED BY v1.0.7.3 — historical public API</strong></summary>

```csharp
public static class LogConsole
{
	public static ConsoleColor ForegroundColor { get; set; }

	public static void WriteLine(string message, string? time = null) { }
	public static void ResetColor() { }
}
```

**Historical usage:**

```csharp
LogConsole.ForegroundColor = ConsoleColor.Green;
LogConsole.WriteLine("[✓] Operation successful", DateTime.Now.ToString("HH:mm:ss"));
LogConsole.ResetColor();
```

</details>

---

### `LogManager`

Real-time logging with non-blocking input capabilities.

```csharp
public static class LogManager
{
	public static void Log(string message) { }
	public static void Clear(bool isShowShell = false) { }
	public static void FlushLogs(List<PromptSegment> segments) { }
}
```

---

## Real-Time Logging (LogManager)

### How It Works

`LogManager` provides non-blocking logging that doesn't interfere with user input:

**Introduced in v1.0.6:** `LogManager` provides the supported logging path for
messages emitted while the interactive shell is active. The normal shell loop
also changed to key-based input in that release; older `Console.ReadLine()`
behavior is preserved in the version history below for compatibility reference.

```csharp
// Clear the console screen safely
LogManager.Clear();

// Clear the screen and re-render the shell prompt/header
LogManager.Clear(isShowShell: true);

// Background task for monitoring
Task.Run(async () =>
{
	int count = 0;
	while (true)
	{
		await Task.Delay(5000);
		count++;
		LogManager.Log($"Monitor: {count} seconds elapsed");
	}
});
```

// Start shell - user can still type while background logs appear
ShelliftAPIBuild.Create()
	.SelectCommandShellLoad("MainShell")
	.Build();
```

### Thread Safety

- `LogManager` is fully thread-safe
- Multiple tasks can log simultaneously without locks blocking execution
- Logs are queued and displayed without interrupting user input

### Best Practices

```csharp
// ✓ Good: Use LogManager for background logging
Task.Run(() =>
{
	while (true)
	{
		LogManager.Log("Background status");
		Task.Delay(1000).Wait();
	}
});

// ✗ Poor: Console.WriteLine blocks input
Task.Run(() =>
{
	while (true)
	{
		Console.WriteLine("This will interfere with prompt");
		System.Threading.Thread.Sleep(1000);
	}
});
```

---

## UI Components

### `HeaderStyle` Enum

Pre-built header styles for shell initialization.

```csharp
public enum HeaderStyle
{
	Classic, DoubleLine, StarBorder, Boxed, Minimal, Clean, Fancy, Banner,
	AsciiArt, Cyber, Neon, Retro, Matrix, Minimalist, Modern, Elegant
}
```

---

### `PromptStyle` Enum

Pre-built prompt styles for command input.

```csharp
public enum PromptStyle
{
	Default, Linux, Powerline, Minimal, FullInfo, Dark, SimpleArrow,
	Brackets, DoubleArrow, Custom
}
```

---

### `TableFormatterTemplate`

Builds and renders structured console tables through an instance-based fluent API.

```csharp
public class TableFormatterTemplate
{
	public TableFormatterTemplate AddColumn(string header, ConsoleColor? color = null, int? fixedWidth = null) { }
	public TableFormatterTemplate AddRow(params string[] values) { }
	public void Render() { }
	public string RenderToString() { }
}
```

**Usage:**

```csharp
var table = new TableFormatterTemplate()
	.AddColumn("Name", ConsoleColor.Cyan)
	.AddColumn("Status", ConsoleColor.Green)
	.AddRow("Boxara CLI", "Ready")
	.AddRow("Worker", "Running");

table.Render();
```

Each row must provide exactly one value for every declared column. A column can
optionally define a fixed width; otherwise its width expands to fit the longest
header or row value. The formatter supports up to 20 columns.

> **⚠️ Critical Warning:** `RenderToString` temporarily replaces global `Console.Out` without a `finally` block. If an exception occurs during rendering, `Console.Out` may remain redirected, causing subsequent output to be lost or misdirected. Use with caution in unstable environments.

---

### `QuestionShellTemplate`

Provides user confirmation prompts and interactive questions.

```csharp
public static class QuestionShellTemplate
{
	public static bool ShowQuestion(
		string message,
		string confirmText = "Y",
		string cancelText = "N",
		int timeoutSeconds = -1,
		string timeoutMessage = "Operation timed out. Defaulting action.",
		bool continueOnTimeout = false) { }
}
```

---

## Fluent API Reference

### Complete Fluent Builder Example

```csharp
ShelliftAPIBuild.Create()
	.SelectCommandShellLoad("MainShell")
	.WithTitle("Enterprise CLI", "Initializing core modules...")
	.SelectShellHeaderTemplate(HeaderStyle.Boxed, "System Status: Ready")
	.SelectShellPrompt(PromptStyle.FullInfo, "admin")
	.WithAppName("EnterpriseApp")
	.WithAppVersion("3.2.1")
	.WithCommandPreAction((command, args) => LogManager.Log($"Executing: {command}"))
	.WithCommandPostAction((command, args, success) => LogManager.Log($"Completed: {command}"))
	.WithTitlePreAction((title, reasons, timestamp, fileName) => Console.Clear())
	.WithTitlePostAction((title, reasons, timestamp, fileName) => Console.Beep())
	// Retained from v1.0.4; current input uses the v1.0.6 key-reading loop.
	.WithInputProvider(() => Console.ReadLine() ?? "")
	.WithPreProcessor(input => LogManager.Log($"Input: {input}"))
	.WithPostProcessor((input, success) => { })
	.WithExitCondition(() => false)
	.Build();
```

---

## Shell Events (v1.0.6)

Shell lifecycle callbacks are configured on `ShelliftAPIBuild`; they are not
static events on `ShellRegistry`:

```csharp
ShelliftAPIBuild.Create()
	.OnShellStart(() => LogManager.Log("[i] Shell started"))
	.OnShellEnd(() => LogManager.Log("[i] Shell ended"))
	.OnCommandsLoaded(commands => LogManager.Log($"Loaded {commands.Count} commands"))
	.OnCommandExecuted(name => LogManager.Log($"OK: {name}"))
	.OnCommandFailed(name => LogManager.Log($"FAIL: {name}"))
	.OnPromptRendered(prompt => LogManager.Log($"Prompt: {prompt}"))
	.OnShellError(error => LogManager.Log($"FAIL: {error.Message}"));
```

The v1.0.6 event set includes `OnShellStart`, `OnShellEnd`, `OnShellError`,
`OnCommandsLoaded`, `OnCommandExecuted`, `OnCommandFailed`, and
`OnPromptRendered`. The callback signatures shown above match the current
builder API.

<details>
<summary><strong>DESCRIBED BY v1.0.6 — legacy event notation</strong></summary>

Older documentation described these callbacks as events on `ShellRegistry`.
That notation is retained for older applications, but current versions configure
the callbacks through `ShelliftAPIBuild` as shown above.

```csharp
ShellRegistry.OnShellStart += () => Console.WriteLine("[i] Shell started");
ShellRegistry.OnShellEnd += () => Console.WriteLine("[i] Shell ended");
ShellRegistry.OnCommandsLoaded += cmds => LogManager.Log($"Loaded {cmds.Count} commands");
ShellRegistry.OnCommandExecuted += cmd => LogManager.Log($"OK: {cmd.Name}");
ShellRegistry.OnCommandFailed += (cmd, ex) => LogManager.Log($"FAIL: {cmd.Name} - {ex.Message}");
```

</details>

---

## Delegate Hooks

### Command Processor Hooks

The current public hook surface is configured through `ShelliftAPIBuild`.
The direct static hook methods below are retained only as historical v1.0.5 API
documentation. They are not available in the current `CommandProcessorTemplate`.

<details>
<summary><strong>DESCRIBED BY v1.0.5 — historical API</strong></summary>

```csharp
CommandProcessorTemplate.WithCommandPreAction(cmd =>
{
	LogManager.Log($"[>] Executing: {cmd.Name}");
});

CommandProcessorTemplate.WithCommandPostAction(cmd =>
{
	LogManager.Log($"[<] Completed: {cmd.Name}");
});
```

</details>

### Title Hooks

`CommandPromptTitleSEt` is an internal framework helper in current versions.
Applications should use `WithTitlePreAction` and `WithTitlePostAction` on
`ShelliftAPIBuild` instead. The following remains as historical v1.0.5 guidance
and is not current application code.

<details>
<summary><strong>DESCRIBED BY v1.0.5 — historical API</strong></summary>

```csharp
var titleSet = new CommandPromptTitleSEt();
titleSet.WithTitlePreAction(() => Console.Clear());
titleSet.WithTitlePostAction(() => Console.Beep());
```

</details>

### Shell Loop Hooks

The `inputProvider` callback is part of the v1.0.4 compatibility surface. The
default implementation introduced in v1.0.6 reads keyboard input directly.

<details>
<summary><strong>DESCRIBED BY v1.0.4 — original shell-loop extension API</strong></summary>

The following callbacks were introduced in v1.0.4 and are preserved here for
applications using that API. In current versions, normal shell input is handled
by the v1.0.6 key-reading loop; `inputProvider` remains in the signature but is
not invoked by the current implementation.

</details>

```csharp
ShellLoopTemplate.Run(
	segments: myPromptSegments,
	commands: myCommands,
	shellName: "MainShell",
	inputProvider: () => Console.ReadLine() ?? "",
	preProcessor: input => LogManager.Log($"Processing: {input}"),
	postProcessor: (input, result) => LogManager.Log($"Processed {input}. Success: {result}"),
	exitCondition: () => someExitFlag
);
```

---

## Performance Considerations

### Command Discovery

- **Reflection-based loading** happens once during `ShellRegistry.Initialize()`
- Reflection scanning is cached; subsequent calls don't re-scan assemblies
- For performance-critical applications, pre-filter commands using `NonLoadableCommandAttribute`

### Logging Performance

- `LogManager` queues and renders logs while holding its synchronization lock
- The console renderer is internal and synchronous - avoid direct console manipulation
- Use `LogManager` for background logging instead

### External Command Loading

- Loading external assemblies via `ExternalCommandManager` uses `AssemblyLoadContext`
- Unloaded contexts can cause memory leaks if not properly disposed
- Always call `.Unload()` when done with plugin contexts

**Example: Proper Plugin Unloading**

```csharp
var context = new AssemblyLoadContext("PluginContext", isCollectible: true);
try
{
	var assembly = context.LoadFromAssemblyPath("plugin.dll");
	// Use assembly...
}
finally
{
	context.Unload();
	GC.Collect();
	GC.WaitForPendingFinalizers();
}
```

### Shell Loop Performance

- Since v1.0.6, the main shell loop uses `Console.KeyAvailable` and `Console.ReadKey`
	instead of blocking on `Console.ReadLine()`
- `WithInputProvider` is retained from v1.0.4 for API compatibility, but is not
	consumed by the current loop implementation
- Real-time logging via `LogManager` doesn't impact shell responsiveness

---

## Thread Safety

### Thread-Safe Components

| Component | Thread-Safe | Notes |
|-----------|------------|-------|
| `ShellRegistry` | ✓ | Uses internal locks for concurrent access |
| `LogManager` | ✓ | Queue-based async logging |
| `ExternalCommandManager` | ✓ | Synchronized command registration |
| `LogConsole` | Internal | Framework renderer; use `LogManager` instead |
| `ICommand` implementations | ✗ | User-defined; not thread-safe by default |
| `CommandProcessorTemplate` | ✓ | Event firing is synchronized |

### Commands Are NOT Thread-Safe

Individual command implementations are not thread-safe. If commands are called concurrently:

```csharp
// ✗ NOT thread-safe
public class UnsafeCommand : ICommand
{
	private int counter = 0;

	public void Execute()
	{
		counter++; // Race condition if called from multiple threads
		Console.WriteLine($"Count: {counter}");
	}
}

// ✓ Thread-safe
public class SafeCommand : ICommand
{
	private readonly object lockObj = new();
	private int counter = 0;

	public void Execute()
	{
		lock (lockObj)
		{
			counter++;
			Console.WriteLine($"Count: {counter}");
		}
	}
}
```

### Logging from Multiple Threads

```csharp
// ✓ Safe: Multiple threads can log
Task.Run(() => LogManager.Log("Thread 1 message"));
Task.Run(() => LogManager.Log("Thread 2 message"));
Task.Run(() => LogManager.Log("Thread 3 message"));

// ✗ Avoid: Multiple threads calling Console.WriteLine
Task.Run(() => Console.WriteLine("Message 1"));
Task.Run(() => Console.WriteLine("Message 2"));
```

---

## Extensibility Points

### Extending `ICommand`

Create custom command behaviors:

```csharp
public abstract class AsyncCommand : ICommand
{
	public string Name { get; protected set; } = "";
	public string DisplayName { get; protected set; } = "";
	public string[] Aliases { get; protected set; } = Array.Empty<string>();
	public string Category { get; protected set; } = "";
	public string Shell { get; protected set; } = "";
	public string Description { get; protected set; } = "";
	public string CommandVersion { get; protected set; } = "";
	public string[] Parameter { get; protected set; } = Array.Empty<string>();

	protected abstract Task ExecuteAsync();

	public void Execute()
	{
		ExecuteAsync().Wait();
	}

	public void ParameterExecute(string[] args)
	{
		ExecuteAsync().Wait();
	}
}

// Usage
public class MyAsyncCommand : AsyncCommand
{
	protected override async Task ExecuteAsync()
	{
		await Task.Delay(1000);
		Console.WriteLine("Async work complete");
	}
}
```

### Custom Shell Implementation

```csharp
public class AdvancedShell : IShell
{
	public string ShellName => "AdvancedShell";
	public string Description => "Advanced shell with custom features";
	public string Category => "Professional";
	public string ShellVersion => "2.0.0";

	public void Execute()
	{
		// Custom pre-initialization
		InitializeEnvironment();

		// Build shell with custom hooks
		ShelliftAPIBuild.Create()
			.SelectCommandShellLoad(ShellName)
			.WithCommandPreAction((commandName, args) => AuditLog(commandName, args))
			.Build();
	}

	private void InitializeEnvironment()
	{
		LogManager.Log("[i] Initializing advanced environment");
	}

	private void AuditLog(string commandName, string[] args)
	{
		LogManager.Log($"[AUDIT] User executed: {commandName} at {DateTime.Now}");
	}
}
```

---

## Advanced Examples

### Example 1: Database-Backed Commands

```csharp
public class DatabaseCommand : ICommand
{
	private readonly string connectionString;

	public string Name => "dbquery";
	public string DisplayName => "Database Query";
	public string[] Aliases => new[] { "db", "query" };
	public string Category => "Data";
	public string Shell => "AdminShell";
	public string Description => "Execute database queries";
	public string CommandVersion => "1.0.0";
	public string[] Parameter => new[] { "sql_query" };

	public DatabaseCommand(string connStr)
	{
		connectionString = connStr;
	}

	public void Execute()
	{
		Console.WriteLine("No query provided");
	}

	public void ParameterExecute(string[] args)
	{
		if (args.Length == 0)
			throw new ArgumentException("SQL query required", Name);

		try
		{
			var query = string.Join(" ", args);
			// Execute database query
			LogManager.Log($"[>] Executing: {query}");
			// Results display
			LogManager.Log("[<] Query completed");
		}
			catch (Exception ex)
			{
				throw new InvalidOperationException(ex.Message, ex);
			}
	}
}
```

### Example 2: Multi-Level Shell Hierarchy

```csharp
public class MainShell : IShell
{
	public string ShellName => "MainShell";
	public string Description => "Main system shell";
	public string Category => "System";
	public string ShellVersion => "1.0.0";

	public void Execute()
	{
		ShelliftAPIBuild.Create()
			.SelectCommandShellLoad(ShellName)
			.WithTitle("System Shell", "Ready")
			.Build();
	}
}

public class AdminShell : IShell
{
	public string ShellName => "AdminShell";
	public string Description => "Admin-only shell";
	public string Category => "System";
	public string ShellVersion => "1.0.0";

	public void Execute()
	{
		// AdminShell commands would be registered separately
		ShelliftAPIBuild.Create()
			.SelectCommandShellLoad(ShellName)
			.WithTitle("Admin Shell", "Authorized")
			.Build();
	}
}

// Switch shells via commands
public class SwitchShellCommand : ICommand
{
	public string Name => "shell";
	public string DisplayName => "Switch Shell";
	public string[] Aliases => new[] { "sh" };
	public string Category => "System";
	public string Shell => "MainShell";
	public string Description => "Switch to another shell";
	public string CommandVersion => "1.0.0";
	public string[] Parameter => new[] { "shell_name" };

	public void Execute() { }

	public void ParameterExecute(string[] args)
	{
		if (args.Length == 0)
			throw new ArgumentException("A shell name is required", Name);

		ShellRegistry.OpenShell(args[0]);
	}
}
```

### Example 3: Command with External Data Loading

```csharp
public class ConfigCommand : ICommand
{
	public string Name => "config";
	public string DisplayName => "Configuration";
	public string[] Aliases => new[] { "cfg", "conf" };
	public string Category => "System";
	public string Shell => "MainShell";
	public string Description => "Display or modify configuration";
	public string CommandVersion => "1.0.0";
	public string[] Parameter => new[] { "action", "key", "value" };

	public void Execute()
	{
		Console.WriteLine("Configuration Manager");
		Console.WriteLine("Use: config [get|set|list] [key] [value]");
	}

	public void ParameterExecute(string[] args)
	{
		if (args.Length == 0)
		{
			Execute();
			return;
		}

		var action = args[0].ToLower();
		switch (action)
		{
			case "get":
				if (args.Length < 2)
						throw new ArgumentException("Expected: config get <key>", Name);
				DisplayConfig(args[1]);
				break;

			case "set":
				if (args.Length < 3)
						throw new ArgumentException("Expected: config set <key> <value>", Name);
				SetConfig(args[1], args[2]);
				break;

			case "list":
				ListAllConfig();
				break;

			default:
					throw new ArgumentException($"Unknown action: {action}", Name);
		}
	}

	private void DisplayConfig(string key)
	{
		LogManager.Log($"[i] Config[{key}] = value");
	}

	private void SetConfig(string key, string value)
	{
		LogManager.Log($"[+] Config[{key}] set to {value}");
	}

	private void ListAllConfig()
	{
		LogManager.Log("[i] Available config keys:");
		LogManager.Log("  - setting1");
		LogManager.Log("  - setting2");
	}
}
```

---

## Best Practices

### 1. Command Design

- Keep commands focused on a single responsibility
- Use meaningful names and descriptions
- Provide clear error messages via `ErrorShellTemplate`
- Support both `Execute()` (no-args) and `ParameterExecute()` (with args)

> **v1.0.7.1 compatibility note:** Direct calls to `ErrorShellTemplate` in the
> historical examples below work only with versions before it became `internal`.
> Current commands should throw `ArgumentException` or another appropriate
> exception and let the framework render the response.

### 2. Error Handling

The framework's `CommandProcessorTemplate` automatically catches exceptions thrown within commands and renders them via `ErrorShellTemplate`. Dev-Apps should throw exceptions instead of calling error templates directly.

```csharp
public void ParameterExecute(string[] args)
{
	if (args == null || args.Length == 0)
	{
		throw new ArgumentException("No arguments provided");
	}

	// Command logic - if any exception occurs here, the framework will catch and display it automatically
}
```

### 3. Logging

- Use `LogManager` for background operations
- Use `LogManager.Log()` for immediate user feedback
- Always include timestamps for audit trails

### 4. Performance

- Cache frequently accessed data
- Use `ExternalCommandManager` to avoid loading all plugins upfront
- Implement proper cleanup for `AssemblyLoadContext` instances

### 5. ShelliftAPIBuild Configuration

**Important:** `ShelliftAPIBuild.Build()` now validates all required configuration before launching the shell. Ensure:

- ✅ Call `SelectCommandShellLoad(shellName)` with a valid registered shell
- ✅ Call either `SelectShellHeaderTemplate()` OR `SelectCustomHeader()` (not both, not neither)
- ✅ Call either `SelectShellPrompt()` OR `SelectCustomPrompt()` (not both, not neither)
- ✅ No conflicting configuration flags

**Example** (❌ WRONG - will throw InvalidOperationException):
```csharp
ShelliftAPIBuild.Create()
    // Missing SelectCommandShellLoad() - shell name defaults to "MainShell"
    // Missing header selection - validation will fail
    // Missing prompt selection - validation will fail
    .Build();  // InvalidOperationException thrown
```

**Example** (✅ CORRECT):
```csharp
ShelliftAPIBuild.Create()
    .SelectCommandShellLoad("MyShell")
    .SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
    .SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
    .Build();  // Success - all validation passed
```

---

## FAQ

### Q: Why does `ShelliftAPIBuild.Create()` throw an exception?

**A:** `Create()` checks its immediate caller using a stack trace and requires that caller to be an `IShell` implementation. This is a calling convention enforced by the builder; it does not itself register or validate a shell. For direct launching, use `ShelliftAPIBuild.OpenShell(...)` or `ShelliftAPIBuild.OpenShellWithResult(...)`.

### Q: How do I log while the shell is running?

**A:** Use `LogManager.Log()` so output is synchronized with the active prompt. Direct `Console.WriteLine()` calls can interfere with interactive input rendering.

### Q: How do I add external commands?

**A:** Create `ICommand` instances and pass them to `ExternalCommandManager.RegisterExternalCommands()`. This registers command instances at runtime; it is not a complete plugin lifecycle or dependency-management system.

### Q: Why can't I use both `SelectShellPrompt` and `SelectCustomPrompt`?

**A:** A builder should use one prompt source only: either a built-in `PromptStyle` through `SelectShellPrompt(...)` or a generator through `SelectCustomPrompt(...)`. Configure the choice once before calling `Build()`.

### Q: Why can't I use both `SelectShellHeaderTemplate` and `SelectCustomHeader`?

**A:** A builder should use one header source only: either a built-in `HeaderStyle` through `SelectShellHeaderTemplate(...)` or a renderer through `SelectCustomHeader(...)`. Configure the choice once before calling `Build()`.

### Q: Is the framework thread-safe?

**A:** There is no blanket thread-safety guarantee for every component. `LogManager` synchronizes prompt/log rendering, and `ShellRegistry` protects initialization. `ICommand` implementations and mutable collections returned by registry APIs remain the application's responsibility.

### Q: What happens if a command throws an exception?

**A:** `CommandProcessorTemplate` catches command exceptions, renders an error, and normally keeps the shell loop running. `OnShellError` is the shell/build-level error callback; it is not a replacement for command-level error handling.

### Q: Why does the shell ask me to select a command?

**A:** Since v1.0.7.2, if different command types share the same name or alias, the framework displays their metadata and asks you to select one. Enter a number to execute it, or `exit`/`cancel` to stop without execution. Duplicate instances of the same runtime type are deduplicated.

### Q: Why does `WithInputProvider(...)` not replace keyboard input?

**A:** `WithInputProvider(...)` was introduced in v1.0.4 and remains in the compatibility surface. Since v1.0.6, the current shell loop reads keys through `Console.KeyAvailable` and `Console.ReadKey`; the current implementation does not invoke the provider callback.

### Q: How do I extend the framework?

**A:** Implement `ICommand` for commands or `IShell` for shell entry points. You can also build wrappers such as an `AsyncCommand` base class. The authentication APIs were removed in v1.0.7.3.

### Q: Can I load commands from external assemblies?

**A:** Yes, use `ExternalCommandManager.RegisterExternalCommands()` and `AssemblyLoadContext` to load from DLLs.

### Q: What's the difference between "External Command Loading" and "Plugin Architecture"?

**A:** External Command Loading is a mechanism to register additional commands at runtime. It is NOT a full plugin system with lifecycle management, versioning, or dependency resolution.

---

## Changelog

### v1.0.7.5 — TableFormatterTemplate Usage Documentation

- **Added**: Added complete `TableFormatterTemplate` usage documentation, including the `AddColumn()` → `AddRow()` → `Render()` flow, optional column colors, `fixedWidth` configuration, row value requirements, and the maximum 20-column limit.

### v1.0.7.4 — Documentation Alignment & NuGet Readme Cleanup

- **README Scope**: Kept `README.md` focused on NuGet installation and basic usage; moved source-build and maintainer workflow guidance to `devopsdoc.md`.
- **NuGet Links**: Replaced relative README links to `devopsdoc.md` and `AuthorInfo.md` with HTTPS GitHub URLs so they resolve correctly when the README is displayed from NuGet.
- **Multi-Target Documentation**: Documented build output using the generic `bin/Release/netx.x/` path instead of implying a single `net7.0` output.
- **Version History**: Added historical `DESCRIBED BY` sections for removed Authentication APIs, internalized framework components, legacy shell events, and obsolete delegate hooks.
- **Release Traceability**: Added version markers and compatibility notes for the v1.0.3 custom UI APIs, v1.0.4 shell-loop callbacks, v1.0.5 fluent hooks, and v1.0.6 real-time logging/input behavior.
- **API Accuracy**: Synchronized documented declarations and examples with the current APIs for `ShelliftAPIBuild`, `HeaderStyle`, `PromptStyle`, `TableFormatterTemplate`, `QuestionShellTemplate`, `ConvertSymbolUniverse`, `CallDll`, and `ExternalCommandManager`.
- **Error Handling**: Updated current examples to throw exceptions instead of calling the internal `ErrorShellTemplate` directly.
- **Plugin Documentation**: Clarified that application code owns external assembly loading and `AssemblyLoadContext` lifetime; `ExternalCommandManager` only registers command instances.
- **Logging Documentation**: Corrected the description of `LogManager` synchronization and distinguished current `LogManager` usage from historical `LogConsole` access.

### v1.0.7.3 — Internal Tooling & Cleanup

- **Developer Tooling**: Added `pack.py` as a cross-platform Python equivalent to `pack.bat`.
  - *Note: This is an internal developer tool used exclusively for library packaging and verification; it is not intended for end-users of the library.*

- **Workflow Synchronization**: Provides a consistent build and packaging workflow across supported operating systems using the Python standard library.

- **Framework Lean-up**: Completely removed the Authentication system (`IAuthenticator`, `AuthHandle`, `AuthMode`) to reduce complexity and focus on core shell functionality.

- **Logging Refactor**: Internalized `LogConsole` to prevent direct console manipulation by apps, exposing a safe `LogManager.Clear()` method for screen clearing.

### v1.0.7.2 — Duplicate Command Selection Feature
- **Command Resolution**: Implemented "Select Command Duplicate" feature in `CommandProcessorTemplate` to handle multiple commands with the same name or alias.
- **User Interaction**: Added an interactive selection menu showing command metadata (DisplayName, Assembly, Type, Aliases, Description) when duplicates are detected.
- **Control Tokens**: Introduced internal selection control tokens (`exit` and `cancel`) to allow users to cancel the selection process without executing any command.
- **Type Deduplication**: Integrated `GroupBy` logic to distinguish between multiple instances of the same Type (deduplicated) and different Types sharing the same command name (retained for selection).
- **Robustness**: Added input validation for selection indices and null-checks to prevent crashes during cancelled selections.
- **Internal Testing**: Added `LibraryTestestProjects` internal project to facilitate development and verification of duplicate command scenarios.

### v1.0.7.1 — Internal Refactoring & Documentation Cleanup
- **Encapsulation**: Changed `ErrorShellTemplate` to `internal` to prevent direct calls from Dev-Apps, enforcing the use of exception-based error handling.
- **Documentation**: Updated `devopsdoc.md` to reflect internal components and correct the error handling flow (Throw Exception $\rightarrow$ Framework Catch).
- **Cleanup**: Removed misleading 'help' command references from error messages as the framework does not provide a built-in help command.
- **Accuracy**: Corrected `devopsdoc.md` length description in `README.md`.
- **Standardization**: Aligned error handling examples with the latest framework architecture.

### v1.0.6 — Shell Events & Real-Time Logging

- Added 7 shell events (`OnShellStart`, `OnShellEnd`, `OnShellError`, `OnCommandsLoaded`, `OnCommandExecuted`, `OnCommandFailed`, `OnPromptRendered`)
- Added `LogManager` for real-time logging with non-blocking input
- Replaced `Console.ReadLine()` with non-blocking input loop

### v1.0.5 — CommandProcessor Hooks & Title Customization

- Added `WithCommandPreAction` and `WithCommandPostAction`
- Added `WithTitlePreAction` and `WithTitlePostAction`
- Added delegate hooks to `CommandProcessorTemplate` and `CommandPromptTitleSEt`

### v1.0.4 — Shell Loop Customization & Fluent API

- Added `WithInputProvider`, `WithPreProcessor`, `WithPostProcessor`, `WithExitCondition`
- Added shell loop delegate hooks

### v1.0.3 — Custom Header & Prompt

- Added `SelectCustomHeader`
- Added `SelectCustomPrompt`
- Added `WithExtraHeaderInfo`

### v1.0.2 — .NET 7 Support

- Added .NET 7.0 target framework

### v1.0.1 — Multi-Target Support

- Added .NET 8.0 and 9.0 support

### v1.0.0 — Initial Release

- Core shell engine
- ICommand and IShell interfaces
- Fluent API
- Built-in prompt and header styles
- External command support
- Logging system

---

## 📄 License

This project is licensed under the **Apache License 2.0**.

[View full license](https://www.apache.org/licenses/LICENSE-2.0)

Copyright (c) 2026 JuliHyro Studios Workspace
