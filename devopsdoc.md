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
| **Version** | 1.0.6-LTS |
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
13. [Authentication System](#authentication-system)
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

# Build
dotnet build -c Release

# Run tests
dotnet test

# Pack to NuGet
dotnet pack -c Release -o ./nupkgs
```

> **Note:** `dotnet test` requires a test project to be present in the solution.

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

### Core Interfaces

- `ICommand` — Command contract
- `IShell` — Shell contract
- `IAuthenticator` — Authentication contract

### Shell Building

- `ShelliftAPIBuild` — Fluent API builder
- `ShellRegistry` — Shell registration and discovery
- `ShellLoopTemplate` — Main shell loop

### Command Processing

- `CommandProcessorTemplate` — Command execution
- `ExternalCommandManager` — External command loading
- `ReflectionCommandShellTemplate` — Automatic command discovery

### Logging

- `LogConsole` — Console logging
- `LogManager` — Real-time logging

### UI

- `TableFormatterTemplate` — Structured table output
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
│   ├── AuthHandle/ # Authentication system
│   │   ├── AuthenticationHelper.cs
│   │   └── ReflectionAuthenticatorTemplate.cs
│   ├── basicUtils/ # Utility helpers
│   │   ├── AuthMode.cs
│   │   └── ConvertSymbolUniverse.cs
│   ├── Interface/ # Core contracts
│   │   ├── IAuthenticator.cs
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
├── CallDll.cs # Library verification & DLL loading
├── codeint.cs # Return codes (internal)
├── LICENSE.txt # Apache 2.0 license
├── README.md # User documentation
└── BoxaraXLibrary.GenenicLib.LTS.csproj
```

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
			ErrorShellTemplate.ShowCommandInvalidParameter(Name, "Expected 2 arguments");
			return;
		}
		Console.WriteLine($"Executed with args: {string.Join(", ", args)}");
	}
}
```

---

### `IShell`

The contract for shell implementations.

```csharp
public interface IShellExecute
{
	string ShellName { get; }
	string Description { get; }
	string Category { get; }
	string ShellVersion { get; }

	void Execute();
}
```

**Usage:**

```csharp
public class MyShell : IShellExecute
{
	public string ShellName => "MyShell";
	public string Description => "My custom shell";
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

### `IAuthenticator`

The contract for authentication providers.

```csharp
public interface IAuthenticator
{
	string AuthenticatorName { get; }
	AuthMode[] SupportedModes { get; }

	bool Authenticate(string username, string password, AuthMode mode);
}
```

**Usage:**

```csharp
public class SimpleAuthenticator : IAuthenticator
{
	public string AuthenticatorName => "SimpleAuth";
	public AuthMode[] SupportedModes => new[] { AuthMode.Local, AuthMode.Remote };

	public bool Authenticate(string username, string password, AuthMode mode)
	{
		if (mode == AuthMode.Local)
		{
			return username == "admin" && password == "password123";
		}
		return false;
	}
}
```

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
public static class ShelliftAPIBuild
{
	// Create a new shell configuration
	public static ShelliftAPIBuild Create() { }

	// Load commands from a specific shell
	public static ShelliftAPIBuild SelectCommandShellLoad(string shellName) { }

	// Set window title and startup message
	public static ShelliftAPIBuild WithTitle(string title, string message) { }

	// Select built-in header style
	public static ShelliftAPIBuild SelectShellHeaderTemplate(HeaderStyle style, string extraInfo = "") { }

	// Select custom header renderer
	public static ShelliftAPIBuild SelectCustomHeader(Action renderHeader) { }

	// Select built-in prompt style
	public static ShelliftAPIBuild SelectShellPrompt(PromptStyle style, string promptName) { }

	// Select custom prompt generator
	public static ShelliftAPIBuild SelectCustomPrompt(Func<string> promptProvider, ConsoleColor color) { }

	// Set app name (for display)
	public static ShelliftAPIBuild WithAppName(string appName) { }

	// Set app version (for display)
	public static ShelliftAPIBuild WithAppVersion(string version) { }

	// Build and run the shell (terminal blocking)
	public static void Build() { }

	// Build and open shell with result (non-blocking)
	public static void OpenShellWithResult(string shellName) { }
}
```

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

Manages the main command input loop.

```csharp
public static class ShellLoopTemplate
{
	public static void InitializeShellLoop(
		Func<string> inputProvider,
		Action<string> preProcessor,
		Action<string> postProcessor,
		Func<string, bool> exitCondition)
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
	public static void Initialize() { }

	// Get all registered shells
	public static List<IShellExecute> GetAllShells() { }

	// Execute a shell by name
	public static void ExecuteShell(string shellName) { }
}
```

---

## Command System

### `CommandProcessorTemplate`

Handles command execution and error handling.

```csharp
public static class CommandProcessorTemplate
{
	public static void ProcessCommand(string input, List<ICommand> commands) { }

	public static void WithCommandPreAction(Action<ICommand> action) { }
	public static void WithCommandPostAction(Action<ICommand> action) { }
}
```

---

### `ReflectionCommandShellTemplate`

Uses reflection to discover and load commands.

```csharp
public static class ReflectionCommandShellTemplate
{
	// Get all commands implementing ICommand
	public static List<ICommand> GetCommandAllInterface() { }

	// Get commands filtered by shell
	public static List<ICommand> GetCommandsByShell(string shellName) { }
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

### `ErrorShellTemplate`

Provides standardized error messages and prompts with consistent formatting and colors.

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

**Features:**

- **Command Not Found (with suggestions)** - Automatically suggests similar commands based on prefix matching
- **Prefix Matching Display** - Shows available commands that start with the user's input
- **Invalid Parameter Display** - Displays detailed error messages for incorrect parameters

**Usage Example:**

```csharp
var allCommands = ReflectionCommandShellTemplate.GetCommandAllInterface();

// Show "command not found" with prefix-based suggestions
ErrorShellTemplate.ShowCommandNotFound("hel", allCommands);
// Output (if "hello" command exists):
// [!] 'hel' is not a complete command. Did you mean:
//   - hello (Prints a greeting)
//   [i] Total available commands: 5

// Show invalid parameter error
ErrorShellTemplate.ShowCommandInvalidParameter("mycommand", "Expected format: mycommand <arg1> <arg2>");
// Output:
// [X] Invalid parameter(s) for command 'mycommand'. [Command Module Response: Expected format: mycommand <arg1> <arg2>]
// [i] Type 'help' or check command usage for more details.
```

**Internal Implementation:**

- `GetCurrentTime()` - Formats current time as `yyyy-MM-dd HH:mm:ss`
- Uses `LogConsole` for colored output (Red for errors, Yellow for suggestions)
- Automatically counts and displays total available commands

---

## External Command Loading

### `ExternalCommandManager` API

Allows loading additional commands at runtime without modifying the main assembly.

```csharp
public static class ExternalCommandManager
{
	// Register external commands
	public static void RegisterExternalCommands(List<ICommand> commands) { }

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

## Authentication System

### `AuthenticationHelper`

Provides utilities for authentication flows.

```csharp
public static class AuthenticationHelper
{
	public static bool AuthenticateUser(string username, string password, IAuthenticator authenticator)
	{
		return authenticator.Authenticate(username, password, AuthMode.Local);
	}
}
```

---

### `ReflectionAuthenticatorTemplate`

Auto-discovers and loads authenticators using reflection.

```csharp
public static class ReflectionAuthenticatorTemplate
{
	public static List<IAuthenticator> GetAllAuthenticators() { }
	public static IAuthenticator? GetAuthenticatorByName(string name) { }
}
```

**Complete Authentication Flow Example:**

```csharp
public class AdminShell : IShellExecute
{
	public string ShellName => "AdminShell";
	public string Description => "Admin-only shell";
	public string Category => "System";
	public string ShellVersion => "1.0.0";

	public void Execute()
	{
		// 1. Get available authenticators
		var authenticators = ReflectionAuthenticatorTemplate.GetAllAuthenticators();
		if (!authenticators.Any())
		{
			Console.WriteLine("[X] No authenticators found");
			return;
		}

		// 2. Prompt for credentials
		Console.Write("Username: ");
		var username = Console.ReadLine() ?? "";
		Console.Write("Password: ");
		var password = Console.ReadLine() ?? "";

		// 3. Authenticate
		var authenticator = authenticators[0];
		var isAuthenticated = AuthenticationHelper.AuthenticateUser(username, password, authenticator);

		if (!isAuthenticated)
		{
			Console.WriteLine("[X] Authentication failed");
			return;
		}

		// 4. Start shell if authenticated
		ShelliftAPIBuild.Create()
			.SelectCommandShellLoad(ShellName)
			.WithTitle("Admin Shell", "Welcome Admin!")
			.Build();
	}
}
```

---

## Utility Helpers

### `AuthMode` Enum

Specifies authentication modes supported by authenticators.

```csharp
public enum AuthMode
{
	Local,      // Local authentication (Windows AD, etc.)
	Remote,     // Remote authentication (HTTP API, LDAP, etc.)
	OAuth2,     // OAuth2 authentication
	SAML        // SAML2 authentication
}
```

**Usage:**

```csharp
public class MyAuthenticator : IAuthenticator
{
	public AuthMode[] SupportedModes => new[] { AuthMode.Local, AuthMode.OAuth2 };
}
```

---

### `ConvertSymbolUniverse`

Provides utilities for converting and handling special symbols and Unicode strings.

```csharp
public static class ConvertSymbolUniverse
{
	// Converts symbols to safe strings for display
	public static string ToSafeString(string input) { }

	// Converts Unicode sequences to displayable format
	public static string EscapeUnicode(string input) { }
}
```

**Usage:**

```csharp
var boxDrawing = "╔═══╗";
var safe = ConvertSymbolUniverse.ToSafeString(boxDrawing);
// Converts box-drawing characters to ASCII-safe alternatives if needed
```

---

### `CallDll`

Manages library verification and dynamic DLL loading.

```csharp
public static class CallDll
{
	// Verify that the library is correctly loaded
	public static bool VerifyLibraryLoaded() { }

	// Get library version
	public static string GetLibraryVersion() { }
}
```

**Usage:**

```csharp
if (!CallDll.VerifyLibraryLoaded())
{
	Console.WriteLine("[X] BoxaraXLibrary.GenenicLib.LTS not properly loaded");
	return;
}

var version = CallDll.GetLibraryVersion();
Console.WriteLine($"[i] Library version: {version}");
```

---

## Logging System

### `LogConsole`

Provides colored console logging with timestamp support.

```csharp
public static class LogConsole
{
	public static ConsoleColor ForegroundColor { get; set; }

	public static void WriteLine(string message, string? time = null) { }
	public static void ResetColor() { }
}
```

**Example:**

```csharp
LogConsole.ForegroundColor = ConsoleColor.Green;
LogConsole.WriteLine("[✓] Operation successful", DateTime.Now.ToString("HH:mm:ss"));
LogConsole.ResetColor();
```

---

### `LogManager`

Real-time logging with non-blocking input capabilities.

```csharp
public static class LogManager
{
	public static void Log(string message) { }
	public static void LogError(string message) { }
	public static void LogWarning(string message) { }
	public static void LogInfo(string message) { }
}
```

---

## Real-Time Logging (LogManager)

### How It Works

`LogManager` provides non-blocking logging that doesn't interfere with user input:

```csharp
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
	Minimal,        // Single line header
	Simple,         // Two-line header
	Classic,        // Three-line header with borders
	Modern,         // Contemporary style with Unicode
	Compact,        // Minimal spacing
	Detailed,       // Multiple information lines
	Boxed,          // Full box border
	Gradient,       // ASCII gradient effect
	StarbustStyle,  // Star pattern border
	Plus,           // Plus symbols border
	Dollar,         // Dollar symbol border
	Dash,           // Dash line borders
	Equal,          // Equal sign border
	Hash,           // Hash symbol border
	Pipe,           // Pipe symbol border
	Asterisk,       // Asterisk symbol border
	Colon           // Colon symbol border
}
```

---

### `PromptStyle` Enum

Pre-built prompt styles for command input.

```csharp
public enum PromptStyle
{
	Simple,         // ">> "
	FullInfo,       // "[user@host time] >> "
	Classic,        // "$ "
	Root,           // "# "
	Arrow,          // "==> "
	Chevron,        // ">> "
	Question,       // "? "
	UserAtHost,     // "user@host >> "
	TimeFormat,     // "[HH:mm:ss] >> "
	Custom          // User-defined (via SelectCustomPrompt)
}
```

---

### `TableFormatterTemplate`

Formats data as structured ASCII tables.

```csharp
public static class TableFormatterTemplate
{
	public static void PrintTable(List<Dictionary<string, string>> rows, List<string> headers) { }
}
```

---

### `QuestionShellTemplate`

Provides user confirmation prompts and interactive questions.

```csharp
public static class QuestionShellTemplate
{
	public static bool AskYesNo(string question) { }
	public static string AskInput(string prompt) { }
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
	.WithCommandPreAction(cmd => LogManager.Log($"Executing: {cmd.Name}"))
	.WithCommandPostAction(cmd => LogManager.Log($"Completed: {cmd.Name}"))
	.WithTitlePreAction(() => Console.Clear())
	.WithTitlePostAction(() => Console.Beep())
	.WithInputProvider(() => Console.ReadLine() ?? "")
	.WithPreProcessor(input => LogManager.Log($"Input: {input}"))
	.WithPostProcessor(input => { })
	.WithExitCondition(input => input?.ToLower() == "exit")
	.Build();
```

---

## Shell Events

Shell execution can trigger 7 different events:

```csharp
public delegate void OnShellStartDelegate();
public delegate void OnShellEndDelegate();
public delegate void OnShellErrorDelegate(Exception ex);
public delegate void OnCommandsLoadedDelegate(List<ICommand> commands);
public delegate void OnCommandExecutedDelegate(ICommand command);
public delegate void OnCommandFailedDelegate(ICommand command, Exception ex);
public delegate void OnPromptRenderedDelegate();
```

**Usage:**

```csharp
ShellRegistry.OnShellStart += () => Console.WriteLine("[i] Shell started");
ShellRegistry.OnShellEnd += () => Console.WriteLine("[i] Shell ended");
ShellRegistry.OnCommandsLoaded += cmds => LogManager.Log($"Loaded {cmds.Count} commands");
ShellRegistry.OnCommandExecuted += cmd => LogManager.Log($"OK: {cmd.Name}");
ShellRegistry.OnCommandFailed += (cmd, ex) => LogManager.Log($"FAIL: {cmd.Name} - {ex.Message}");
```

---

## Delegate Hooks

### Command Processor Hooks

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

### Title Hooks

```csharp
var titleSet = new CommandPromptTitleSEt();
titleSet.WithTitlePreAction(() => Console.Clear());
titleSet.WithTitlePostAction(() => Console.Beep());
```

### Shell Loop Hooks

```csharp
ShellLoopTemplate.InitializeShellLoop(
	inputProvider: () => Console.ReadLine() ?? "",
	preProcessor: input => LogManager.Log($"Processing: {input}"),
	postProcessor: input => { },
	exitCondition: input => input?.ToLower() == "exit"
);
```

---

## Performance Considerations

### Command Discovery

- **Reflection-based loading** happens once during `ShellRegistry.Initialize()`
- Reflection scanning is cached; subsequent calls don't re-scan assemblies
- For performance-critical applications, pre-filter commands using `NonLoadableCommandAttribute`

### Logging Performance

- `LogManager` queues logs asynchronously - no blocking on I/O
- `LogConsole` is synchronous - avoid frequent calls in tight loops
- Use `LogManager` for background/async logging instead

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

- The main shell loop uses `Console.ReadLine()` which blocks on input
- For non-blocking input scenarios, implement custom `WithInputProvider`
- Real-time logging via `LogManager` doesn't impact shell responsiveness

---

## Thread Safety

### Thread-Safe Components

| Component | Thread-Safe | Notes |
|-----------|------------|-------|
| `ShellRegistry` | ✓ | Uses internal locks for concurrent access |
| `LogManager` | ✓ | Queue-based async logging |
| `ExternalCommandManager` | ✓ | Synchronized command registration |
| `LogConsole` | ⚠️ | Console output not thread-safe (use LogManager instead) |
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

### Extending `IAuthenticator`

Add custom authentication methods:

```csharp
public class LdapAuthenticator : IAuthenticator
{
	public string AuthenticatorName => "LDAP";
	public AuthMode[] SupportedModes => new[] { AuthMode.Remote };

	public bool Authenticate(string username, string password, AuthMode mode)
	{
		// LDAP authentication logic
		return ValidateLdapCredentials(username, password);
	}

	private bool ValidateLdapCredentials(string username, string password)
	{
		// Implementation
		return true;
	}
}
```

### Custom Shell Implementation

```csharp
public class AdvancedShell : IShellExecute
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
			.WithCommandPreAction(cmd => AuditLog(cmd))
			.Build();
	}

	private void InitializeEnvironment()
	{
		LogManager.Log("[i] Initializing advanced environment");
	}

	private void AuditLog(ICommand cmd)
	{
		LogManager.Log($"[AUDIT] User executed: {cmd.Name} at {DateTime.Now}");
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
		{
			ErrorShellTemplate.ShowCommandInvalidParameter(Name, "SQL query required");
			return;
		}

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
			ErrorShellTemplate.ShowCommandInvalidParameter(Name, ex.Message);
		}
	}
}
```

### Example 2: Multi-Level Shell Hierarchy

```csharp
public class MainShell : IShellExecute
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

public class AdminShell : IShellExecute
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
		{
			ErrorShellTemplate.ShowCommandInvalidParameter(Name);
			return;
		}

		ShellRegistry.ExecuteShell(args[0]);
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
				{
					ErrorShellTemplate.ShowCommandInvalidParameter(Name, "Expected: config get <key>");
					return;
				}
				DisplayConfig(args[1]);
				break;

			case "set":
				if (args.Length < 3)
				{
					ErrorShellTemplate.ShowCommandInvalidParameter(Name, "Expected: config set <key> <value>");
					return;
				}
				SetConfig(args[1], args[2]);
				break;

			case "list":
				ListAllConfig();
				break;

			default:
				ErrorShellTemplate.ShowCommandInvalidParameter(Name, $"Unknown action: {action}");
				break;
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

### 2. Error Handling

```csharp
public void ParameterExecute(string[] args)
{
	if (args == null || args.Length == 0)
	{
		ErrorShellTemplate.ShowCommandInvalidParameter(Name, "No arguments provided");
		return;
	}

	try
	{
		// Command logic
	}
	catch (Exception ex)
	{
		ErrorShellTemplate.ShowCommandInvalidParameter(Name, ex.Message);
	}
}
```

### 3. Logging

- Use `LogManager` for background operations
- Use `LogConsole` for immediate user feedback
- Always include timestamps for audit trails

### 4. Authentication

- Validate credentials before accessing sensitive features
- Support multiple authentication modes when possible
- Log authentication attempts

### 5. Performance

- Cache frequently accessed data
- Use `ExternalCommandManager` to avoid loading all plugins upfront
- Implement proper cleanup for `AssemblyLoadContext` instances

---

## FAQ

### Q: Why does `ShelliftAPIBuild.Create()` throw an exception?

**A:** It must be called from a class implementing `IShell`. This ensures the shell is properly registered.

### Q: How do I log while the shell is running?

**A:** Use `LogManager.Log()` for real-time logging. Do not use `Console.WriteLine()` directly.

### Q: How do I add external commands?

**A:** Use `ExternalCommandManager.RegisterExternalCommands()` to inject commands at runtime.

### Q: Why can't I use both `SelectShellPrompt` and `SelectCustomPrompt`?

**A:** They are mutually exclusive — choose either built-in or custom prompt.

### Q: Why can't I use both `SelectShellHeaderTemplate` and `SelectCustomHeader`?

**A:** They are mutually exclusive — choose either built-in or custom header.

### Q: Is the framework thread-safe?

**A:** Core components (`ExternalCommandManager`, `ShellRegistry`, `LogManager`) are thread-safe. Commands are not thread-safe by default.

### Q: What happens if a command throws an exception?

**A:** The exception is caught, logged, and the shell continues running. Use `OnShellError` to handle errors globally.

### Q: How do I extend the framework?

**A:** Implement custom versions of `ICommand`, `IShell`, or `IAuthenticator`, or create wrapper classes like `AsyncCommand` base class.

### Q: Can I load commands from external assemblies?

**A:** Yes, use `ExternalCommandManager.RegisterExternalCommands()` and `AssemblyLoadContext` to load from DLLs.

### Q: What's the difference between "External Command Loading" and "Plugin Architecture"?

**A:** External Command Loading is a mechanism to register additional commands at runtime. It is NOT a full plugin system with lifecycle management, versioning, or dependency resolution.

---

## Changelog

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
