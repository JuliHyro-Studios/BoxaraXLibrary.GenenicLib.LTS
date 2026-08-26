# 📘 BoxaraXLibrary.GenenicLib.LTS — Development Documentation

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
| **DevDoc update Date** | 2026-08-26 |

---

## 📑 Table of Contents

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
11. [Plugin & External Command Support](#plugin--external-command-support)
12. [Logging System](#logging-system)
13. [Real-Time Logging (LogManager)](#real-time-logging-logmanager)
14. [UI Components](#ui-components)
15. [Authentication System](#authentication-system)
16. [Fluent API Reference](#fluent-api-reference)
17. [Shell Events](#shell-events)
18. [Delegate Hooks](#delegate-hooks)
19. [Performance Considerations](#performance-considerations)
20. [Thread Safety](#thread-safety)
21. [Error Handling](#error-handling)
22. [Extensibility Points](#extensibility-points)
23. [Best Practices](#best-practices)
24. [FAQ](#faq)
25. [Changelog](#changelog)

---

## Overview

**BoxaraXLibrary.GenenicLib.LTS** is a lightweight, high-performance framework for building shell-based CLI applications in .NET. It provides:

- Shell engine with customizable prompts and headers
- Command registration and discovery via reflection
- External command injection (`ExternalCommandManager`)
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

## 🤝 Contributing

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
- `ExternalCommandManager` — External command injection
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
│ ├── AuthHandle/ # Authentication system
│ │ ├── AuthenticationHelper.cs
│ │ └── ReflectionAuthenticatorTemplate.cs
│ ├── basicUtils/ # Utility helpers
│ │ ├── AuthMode.cs
│ │ └── ConvertSymbolUniverse.cs
│ ├── Interface/ # Core contracts
│ │ ├── IAuthenticator.cs
│ │ ├── ICommand.cs
│ │ ├── IShellExecute.cs (IShell)
│ │ └── NonLoadableCommandAttribute.cs
│ ├── Log/ # Logging system
│ │ ├── LogConsole.cs
│ │ └── LogManager.cs
│ └── ShellHandle/ # Shell engine
│ ├── CommandProcessorTemplate.cs
│ ├── CommandPromptTemplate.cs
│ ├── CommandPromptTitleSEt.cs
│ ├── ErrorShellTemplate.cs
│ ├── ExternalCommandManager.cs
│ ├── QuestionShellTemplate.cs
│ ├── ReflectionCommandShellTemplate.cs
│ ├── ReflectionShellTemplate.cs
│ ├── ShellHeaderTemplate.cs
│ ├── ShelliftAPIBuild.cs
│ ├── ShellLoopTemplate.cs
│ ├── ShellRegistry.cs
│ └── TableFormatterTemplate.cs
├── CallDll.cs # Library verification
├── codeint.cs # Return codes (internal)
├── LICENSE.txt # Apache 2.0 license
├── README.md # User documentation
└── BoxaraXLibrary.GenenicLib.LTS.csproj

```

---

## Core Interfaces

### `ICommand`

Defines a command that can be executed in a shell.

```csharp

public interface ICommand
{
    string DisplayName { get; }
    string[] Parameter { get; }
    string Name { get; }
    string[] Aliases { get; }
    string Category { get; }
    string Shell { get; }
    string Description { get; }
    string CommandVersion { get; }

    void Execute();
    void ParameterExecute(string[] args);
}

```

**Implementation Notes:**

- `Name` must be unique within a shell
- `Shell` must match an existing shell name
- `Parameter` should list supported parameter names (optional)
- `Execute()` is called when no parameters are provided
- `ParameterExecute(string[] args)` is called when parameters are provided

### `IShell`

Defines a shell environment.

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

**Implementation Notes:**

- `ShellName` must be unique
- `Execute()` must build and run the shell using `ShelliftAPIBuild`

### `IAuthenticator`

Defines an authentication provider.

```csharp

public interface IAuthenticator
{
    AuthMode Mode { get; }
    string DisplayName { get; }
    string Description { get; }
    bool Authenticate(string prompt, int timeRedirect);
}

```

### `NonLoadableCommandAttribute`

Marks commands that should NOT be auto-loaded by reflection.

```csharp

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class NonLoadableCommandAttribute : Attribute
{
    public string Reason { get; }
    public NonLoadableCommandAttribute(string reason = "");
}

```

---

## Shell Engine

### `ShelliftAPIBuild` (Fluent API)

The main entry point for building shells.

```csharp

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

**Available Fluent Methods:**

| **Method** | **Description** |
| --- | --- |
| `SelectCommandShellLoad(string)` | Set shell name |
| `WithTitle(string, params string[])` | Set console title |
| `SelectShellHeaderTemplate(HeaderStyle, string?)` | Choose header style |
| `SelectCustomHeader(Action)` | Custom header renderer |
| `WithExtraHeaderInfo(string)` | Additional header info |
| `SelectShellPrompt(PromptStyle, string)` | Choose prompt style |
| `SelectCustomPrompt(Func<string>, ConsoleColor)` | Custom prompt generator |
| `WithInputProvider(Func<string>)` | Custom input provider |
| `WithPreProcessor(Action<string>)` | Pre-command hook |
| `WithPostProcessor(Action<string, bool>)` | Post-command hook |
| `WithExitCondition(Func<bool>)` | Exit condition checker |
| `WithCommandPreAction(Action<string, string[]>)` | Pre-command execution hook |
| `WithCommandPostAction(Action<string, string[], bool>)` | Post-command execution hook |
| `WithTitlePreAction(Action<string, string[], DateTime, string>)` | Pre-title hook |
| `WithTitlePostAction(Action<string, string[], DateTime, string>)` | Post-title hook |
| `OnShellStart(Action)` | Shell start event |
| `OnShellEnd(Action)` | Shell end event |
| `OnShellError(Action<Exception>)` | Shell error event |
| `OnCommandsLoaded(Action<List<ICommand>>)` | Commands loaded event |
| `OnCommandExecuted(Action<string>)` | Command executed event |
| `OnCommandFailed(Action<string>)` | Command failed event |
| `OnPromptRendered(Action<string>)` | Prompt rendered event |
| `WithAppName(string)` | Set app name |
| `WithAppVersion(string)` | Set app version |

### `ShellLoopTemplate`

Manages the main shell loop with non-blocking input support.

```csharp

public static void Run(
    List<PromptSegment> segments,
    List<ICommand> commands,
    string shellName,
    Func<string>? inputProvider = null,
    Action<string>? preProcessor = null,
    Action<string, bool>? postProcessor = null,
    Func<bool>? exitCondition = null,
    Action<string, string[]>? commandPreAction = null,
    Action<string, string[], bool>? commandPostAction = null)

```

**Key Features:**

- Non-blocking input loop using `Console.KeyAvailable` + `Console.ReadKey`
- Real-time log support via `LogManager`
- Full Backspace support
- Thread-safe prompt rendering

### `ShellRegistry`

Manages shell registration and discovery.

```csharp

// Initialize once at startup
ShellRegistry.Initialize();

// Open a shell
ShellRegistry.OpenShell("MainShell");

```

---

## Command System

### `CommandProcessorTemplate`

Processes user input and executes commands.

```csharp

public static bool Process(
    string input,
    List<ICommand> commands,
    Action<string, string[]>? preAction = null,
    Action<string, string[], bool>? postAction = null)

```

**Returns:**

- `true` — command found and executed (or handled)
- `false` — command not found

### `ReflectionCommandShellTemplate`

Automatically discovers commands via reflection.

```csharp

// Load commands for a specific shell
var commands = ReflectionCommandShellTemplate.LoadCommandsForShell("MainShell");

// Load all commands (merged with external)
var all = ReflectionCommandShellTemplate.GetCommandAllInterface();

```

### `ExternalCommandManager`

Injects external commands (e.g., from plugins).

```csharp

// Register external commands
ExternalCommandManager.RegisterExternalCommands(externalCommands);

// Merge core + external
var allCommands = ExternalCommandManager.MergeCommands(coreCommands);

```

---

## Plugin & External Command Support

### `ExternalCommandManager` API

| **Method** | **Description** |
| --- | --- |
| `RegisterExternalCommands(IEnumerable<ICommand>)` | Register external commands |
| `GetExternalCommands()` | Get registered external commands |
| `MergeCommands(List<ICommand>)` | Merge core + external |
| `GetTotalCommandCount(List<ICommand>)` | Get total command count |
| `GetCommandCountInfo(List<ICommand>)` | Get detailed count info |
| `GetLoadedMessage(List<ICommand>)` | Get formatted loaded message |

---

## Logging System

### `LogConsole`

Static console logging utilities.

```csharp

LogConsole.Clear(IsShowShell: true);
LogConsole.ForegroundColor = ConsoleColor.Green;
LogConsole.WriteLine("Hello, World!");
LogConsole.ResetColor();

```

### `LogManager`

Real-time logging with non-blocking support.

```csharp

// Log from anywhere
LogManager.Log("This log appears while user is typing");

// Background task example
Task.Run(async () =>
{
    int count = 0;
    while (true)
    {
        await Task.Delay(5000);
        count++;
        LogManager.Log($"Background log {count}");
    }
});

```

**Important:** `LogManager` is thread-safe and designed for real-time logging during shell execution.

---

## Real-Time Logging (LogManager)

### How It Works

1. `LogManager.Log()` queues log messages
2. `ShellLoopTemplate` flushes logs before each prompt render
3. Logs appear immediately without blocking user input
4. Prompt automatically re-renders after logs

### Thread Safety

```csharp

lock (LogManager.RenderLock)
{
    // Custom rendering with thread safety
    // Update prompt or input
}

```

### Best Practices

- ✅ Use `LogManager.Log()` for real-time logging
- ❌ Do not use `Console.WriteLine()` directly while shell is running
- ✅ Use `lock (LogManager.RenderLock)` for thread-safe custom rendering

---

## UI Components

### `HeaderStyle` Enum

Available header styles:

| **Style** | **Description** |
| --- | --- |
| `Classic`            | `===== App v1.0.0 =====` |
| `DoubleLine`         | `═══ App v1.0.0 ═══`     |
| `StarBorder`         | `*** App v1.0.0 ***`     |
| `Boxed`              | Box with borders         |
| `Minimal`            | Plain text               |
| `Clean`              | `[ App v1.0.0 ]`         |
| `Fancy`              | `✦ App v1.0.0 ✦`         |
| `Banner`             | Banner style             |
| `AsciiArt`           | ASCII art style          |
| `Cyber`              | Cyberpunk style          |
| `Neon`               | Neon glow style          |
| `Retro`              | Retro 80s style          |
| `Matrix`             | Matrix green style       |
| `Minimalist`         | Very simple              |
| `Modern`             | Modern flat style        |
| `Elegant`            | Elegant with symbols     |

### `PromptStyle` Enum

Available prompt styles:

| **Style** | **Example** |
| --- | --- |
| `Default`        | `BoxaraHS>`                             |
| `Linux`          | `[user@hostname]$`                      |
| `Powerline`      | ` BoxaraHS `                          |
| `Minimal`        | `$`                                     |
| `FullInfo`       | `[user@hostname BoxaraHS]>`             |
| `Dark`           | `█ BoxaraHS █`                          |
| `SimpleArrow`    | `➜ BoxaraHS $`                          |
| `Brackets`       | `[BoxaraHS]>`                           |
| `DoubleArrow`    | `>> BoxaraHS >>`                        |
| `Custom`         | User-defined (via `SelectCustomPrompt`) |

### `TableFormatterTemplate`

Structured tabular output.

```csharp

var table = new TableFormatterTemplate();
table.AddColumn("Name", ConsoleColor.Yellow, 15);
table.AddColumn("Value", ConsoleColor.Cyan, 20);
table.AddRow("Key1", "Value1");
table.AddRow("Key2", "Value2");
table.Render();

```

### `QuestionShellTemplate`

User interaction with timeout support.

```csharp

var confirmed = QuestionShellTemplate.ShowQuestion(
    message: "Do you want to proceed?",
    confirmText: "Y",
    cancelText: "N",
    timeoutSeconds: 10,
    timeoutMessage: "Operation timed out.",
    continueOnTimeout: false
);

```

---

## Authentication System

### `AuthenticationHelper`

Manages authenticator registration and execution.

```csharp

// Initialize
AuthenticationHelper.Initialize();

// Authenticate
bool success = AuthenticationHelper.Authenticate(
    AuthMode.Local,
    "Enter password: ",
    timeRedirect: 500
);

```

### `ReflectionAuthenticatorTemplate`

Auto-discovers authenticators via reflection.

```csharp

var authenticators = ReflectionAuthenticatorTemplate.LoadAllAuthenticators();
var auth = ReflectionAuthenticatorTemplate.GetAuthenticatorByMode(AuthMode.Local);

```

### Custom Authenticator

```csharp

public class MyAuthenticator : IAuthenticator
{
    public AuthMode Mode => AuthMode.Local;
    public string DisplayName => "My Authenticator";
    public string Description => "Custom authentication";

    public bool Authenticate(string prompt, int timeRedirect)
    {
        // Custom authentication logic
        return true;
    }
}

```

---

## Fluent API Reference

### Header Configuration

| **Method** | **Parameters** | **Description** |
| --- | --- | --- |
| `SelectShellHeaderTemplate` | `HeaderStyle style, string? welcomeMessage` | Choose built-in header style |
| `SelectCustomHeader` | `Action renderHeader` | Custom header renderer |
| `WithExtraHeaderInfo` | `string extraInfo` | Add extra header info |

### Prompt Configuration

| **Method** | **Parameters** | **Description** |
| --- | --- | --- |
| `SelectShellPrompt` | `PromptStyle style, string customName` | Choose built-in prompt style |
| `SelectCustomPrompt` | `Func<string> generator, ConsoleColor color` | Custom prompt generator |

### Shell Loop Hooks

| **Method** | **Parameters** | **Description** |
| --- | --- | --- |
| `WithInputProvider` | `Func<string> inputProvider` | Custom input reading |
| `WithPreProcessor` | `Action<string> preProcessor` | Pre-command logic |
| `WithPostProcessor` | `Action<string, bool> postProcessor` | Post-command logic |
| `WithExitCondition` | `Func<bool> exitCondition` | Exit condition checker |
| `WithCommandPreAction` | `Action<string, string[]> preAction` | Pre-command execution |
| `WithCommandPostAction` | `Action<string, string[], bool> postAction` | Post-command execution |

### Title Configuration

| **Method** | **Parameters** | **Description** |
| --- | --- | --- |
| `WithTitle` | `string title, params string[] reasons` | Set console title |
| `WithTitlePreAction` | `Action<string, string[], DateTime, string> preAction` | Pre-title hook |
| `WithTitlePostAction` | `Action<string, string[], DateTime, string> postAction` | Post-title hook |

### Shell Events

| **Method** | **Parameters** | **Description** |
| --- | --- | --- |
| `OnShellStart` | `Action onStart` | Shell start event |
| `OnShellEnd` | `Action onEnd` | Shell end event |
| `OnShellError` | `Action<Exception> onError` | Shell error event |
| `OnCommandsLoaded` | `Action<List<ICommand>> onLoaded` | Commands loaded event |
| `OnCommandExecuted` | `Action<string> onExecuted` | Command executed event |
| `OnCommandFailed` | `Action<string> onFailed` | Command failed event |
| `OnPromptRendered` | `Action<string> onRendered` | Prompt rendered event |

### App Configuration

| **Method** | **Parameters** | **Description** |
| --- | --- | --- |
| `WithAppName` | `string appName` | Set app name |
| `WithAppVersion` | `string appVersion` | Set app version |

---

## Shell Events

### Available Events

| **Event** | **Triggered** | **Parameters** |
| --- | --- | --- |
| `OnShellStart` | Before shell begins | — |
| `OnShellEnd` | After shell ends | — |
| `OnShellError` | When shell encounters an error | `Exception` |
| `OnCommandsLoaded` | After commands are loaded | `List<ICommand>` |
| `OnCommandExecuted` | After command executes successfully | `string` (command name) |
| `OnCommandFailed` | When command fails | `string` (command name) |
| `OnPromptRendered` | After prompt is rendered | `string` (prompt text) |

### Example

```csharp

ShelliftAPIBuild.Create()
    .OnShellStart(() => Console.WriteLine("Shell started!"))
    .OnCommandExecuted((cmd) => Console.WriteLine($"Command '{cmd}' executed"))
    .OnShellError((ex) => Console.WriteLine($"Error: {ex.Message}"))
    .Build();

```

---

## Delegate Hooks

### Overview

All delegate hooks are **read-only** — they cannot modify the core flow or data.

| **Hook** | **Read-Only?** | **Purpose** |
| --- | --- | --- |
| `WithInputProvider` | ❌ No (returns input) | Custom input reading |
| `WithPreProcessor` | ✅ Yes | Pre-command logic |
| `WithPostProcessor` | ✅ Yes | Post-command logic |
| `WithExitCondition` | ❌ No (returns bool) | Exit condition |
| `WithCommandPreAction` | ✅ Yes | Pre-command execution |
| `WithCommandPostAction` | ✅ Yes | Post-command execution |
| `WithTitlePreAction` | ✅ Yes | Pre-title logic |
| `WithTitlePostAction` | ✅ Yes | Post-title logic |

### Design Principle

> **"Framework controls the flow — App hooks into it."**

---

## Performance Considerations

### Command Discovery

- `ShellRegistry.Initialize()` caches assemblies after first load
- Reflection is minimized by caching method handles

### External Commands

- Stored in a static `List<ICommand>` with thread-safe locking
- O(1) lookups

### Logging

- `LogConsole` writes directly to console — avoid excessive calls in tight loops
- `LogManager` uses a queue for non-blocking real-time logging

### TableFormatter

- Renders synchronously
- For large datasets, consider pagination

### Shell Loop

- Non-blocking input loop with `Thread.Sleep(20)` for CPU efficiency
- Log flushing happens only when logs exist

---

## Thread Safety

### Thread-Safe Components

| **Component** | **Thread-Safe?** | **Mechanism** |
| --- | --- | --- |
| `ExternalCommandManager` | ✅ Yes | `lock (_lock)` |
| `ShellRegistry` | ✅ Yes | `lock (_lock)` |
| `LogManager` | ✅ Yes | `lock (RenderLock)` |
| `LogConsole` | ⚠️ Not thread-safe | Direct console access |
| `Commands` | ❌ No (executed on caller thread) | Implement own locking |

### Custom Command Threading

```csharp

public class MyCommand : ICommand
{
    private readonly object _lock = new object();

    public void Execute()
    {
        lock (_lock)
        {
            // Thread-safe command logic
        }
    }
}

```

---

## Error Handling

### Framework Errors

- `ShelliftAPIBuild.Create()` throws `InvalidOperationException` if not called from `IShell`
- `LogManager.Log()` throws `InvalidOperationException` if not started

### User Errors

- `ErrorShellTemplate` provides user-friendly error messages
- Command not found suggestions
- Invalid parameter handling

### Exception Handling

- Shell loop catches and logs exceptions (does not crash)
- CommandProcessor catches command-specific exceptions
- Events can handle errors via `OnShellError`

---

## Extensibility Points

| **Extension Point** | **Interface / Method** | **Description** |
| --- | --- | --- |
| **Command** | `ICommand` | Implement custom commands |
| **Shell** | `IShell` | Implement custom shells |
| **Authenticator** | `IAuthenticator` | Implement custom authentication |
| **External Commands** | `ExternalCommandManager` | Inject external commands |
| **Custom Prompt** | `SelectCustomPrompt` | Define custom prompts |
| **Custom Header** | `SelectCustomHeader` | Define custom headers |
| **Delegate Hooks** | `WithPreProcessor`, `WithPostProcessor`, etc. | Hook into shell loop |
| **Shell Events** | `OnShellStart`, `OnShellEnd`, etc. | Subscribe to lifecycle events |

--- | --- |
| **Command** | `ICommand` |
| **Shell** | `IShell` |
| **Authenticator** | `IAuthenticator` |
| **External Commands** | `ExternalCommandManager` |
| **Custom Prompt** | `SelectCustomPrompt` |
| **Custom Header** | `SelectCustomHeader` |
| **Delegate Hooks** | `WithPreProcessor`, `WithPostProcessor`, etc. |
| **Shell Events** | `OnShellStart`, `OnShellEnd`, etc. |

---

## Best Practices

### Command Implementation

```csharp

public class MyCommand : ICommand
{
    public string Name => "mycommand";
    public string DisplayName => "My Command";
    public string[] Aliases => new[] { "mc" };
    public string Category => "Custom";
    public string Shell => "MainShell";
    public string Description => "My custom command";
    public string CommandVersion => "1.0.0";
    public string[] Parameter => new[] { "--arg1", "--arg2" };

    public void Execute()
    {
        // Logic without parameters
    }

    public void ParameterExecute(string[] args)
    {
        // Logic with parameters
        // Parse args here
    }
}

```

### Shell Implementation

```csharp

public class MyShell : IShell
{
    public string ShellName => "MyShell";
    public string DisplayName => "My Shell";
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

### Real-Time Logging

```csharp

// Background task for logging
Task.Run(async () =>
{
    int count = 0;
    while (true)
    {
        await Task.Delay(5000);
        count++;
        LogManager.Log($"Background log {count}");
    }
});

// Build shell
ShelliftAPIBuild.Create()
    .SelectCommandShellLoad("MainShell")
    .Build();

```

### Custom Prompt

```csharp

private string GetPrompt()
{
    return $"[{Environment.UserName}@{Environment.MachineName} {DateTime.Now:HH:mm:ss}] ";
}

ShelliftAPIBuild.Create()
    .SelectCustomPrompt(GetPrompt, ConsoleColor.Cyan)
    .Build();

```

### Custom Header

```csharp

private void RenderHeader()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔═══════════════════════════════════╗");
    Console.WriteLine($"║  MyApp v1.0.0 - {DateTime.Now}  ║");
    Console.WriteLine("╚═══════════════════════════════════╝");
    Console.ResetColor();
}

ShelliftAPIBuild.Create()
    .SelectCustomHeader(RenderHeader)
    .Build();

```

---

## FAQ

### Q: Why does `ShelliftAPIBuild.Create()` throw an exception?

**A:** It must be called from a class implementing `IShell`. This ensures the shell is properly registered.

### Q: How do I log while the shell is running?

**A:** Use `LogManager.Log()` for real-time logging. Do not use `Console.WriteLine()` directly.

### Q: How do I add external commands (plugins)?

**A:** Use `ExternalCommandManager.RegisterExternalCommands()`.

### Q: Why can't I use both `SelectShellPrompt` and `SelectCustomPrompt`?

**A:** They are mutually exclusive — choose either built-in or custom prompt.

### Q: Why can't I use both `SelectShellHeaderTemplate` and `SelectCustomHeader`?

**A:** They are mutually exclusive — choose either built-in or custom header.

### Q: Is the framework thread-safe?

**A:** Core components (`ExternalCommandManager`, `ShellRegistry`, `LogManager`) are thread-safe. Commands are not thread-safe by default.

### Q: What happens if a command throws an exception?

**A:** The exception is caught, logged, and the shell continues running. Use `OnShellError` to handle errors globally.

---

## Changelog

### v1.0.6 — Shell Events & Real-Time Logging

- Added 7 shell events (`OnShellStart`, `OnShellEnd`, `OnShellError`, `OnCommandsLoaded`, `OnCommandExecuted`, `OnCommandFailed`, `OnPromptRendered`)
- Added `LogManager` for real-time logging with non-blocking input
- Replaced `Console.ReadLine()` with non-blocking input loop

### v1.0.5 — CommandProcessor Hooks & Title Customization

- Added `WithCommandPreAction` and `WithCommandPostAction`
- Added `WithTitlePreAction` and `WithTitlePostAction`
- Added delegate hooks to `CommandProcessorTemplate` and `CommandPromptTitleSEt`

### v1.0.4 — Shell Loop Customization & Fluent API

- Added `WithInputProvider`, `WithPreProcessor`, `WithPostProcessor`, `WithExitCondition`
- Added shell loop delegate hooks

### v1.0.3 — Custom Header & Prompt

- Added `SelectCustomHeader`
- Added `SelectCustomPrompt`
- Added `WithExtraHeaderInfo`

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

This project is licensed under the **Apache License 2.0**.

[View full license](https://www.apache.org/licenses/LICENSE-2.0)

Copyright (c) 2026 JuliHyro Studios Workspace

---
