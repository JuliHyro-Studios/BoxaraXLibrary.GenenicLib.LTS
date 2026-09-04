# 📦 BoxaraXLibrary.GenenicLib.LTS

[![NuGet](https://img.shields.io/nuget/v/BoxaraXLibrary.GenenicLib.LTS?style=for-the-badge&logo=nuget&color=004880)](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS)
[![NuGet Downloads](https://img.shields.io/nuget/dt/BoxaraXLibrary.GenenicLib.LTS?style=for-the-badge&logo=nuget&color=004880)](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS)
[![GitHub Repo](https://img.shields.io/badge/GitHub-Repo-181717?style=for-the-badge&logo=github)](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS)
[![.NET Version](https://img.shields.io/badge/.NET-7.0%20%7C%208.0%20%7C%209.0%20%7C%2010.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=for-the-badge)](https://www.apache.org/licenses/LICENSE-2.0)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-lightgrey?style=for-the-badge)](https://dotnet.microsoft.com/)
> **Generic Library LTS** — Core framework for building shell-based CLI applications with command handling, rich console UI, and fluent API. Designed for **Lazy DevOps & Engineers**.
**Generic Library LTS** — Core framework for building shell-based CLI applications with command handling, rich console UI, and fluent API.
---
## 📖 Overview

**BoxaraXLibrary.GenenicLib.LTS** is a lightweight, high-performance framework designed for building **shell-based CLI applications** in .NET. It provides a complete infrastructure for command registration, shell lifecycle management, and interactive console experiences.

### ✨ Key Features

- **Shell Engine** — Build and run interactive shells with customizable prompts and headers
- **Command System** — Register, discover, and execute commands via reflection or manual registration
- **External Command Support** — Register commands from external sources via `ExternalCommandManager`
- **Fluent API** — Build shells with a clean, expressive fluent interface
- **Customizable Prompt & Header** — Fully customizable dynamic prompts and headers with delegate support
- **Shell Loop Hooks** — Inject custom logic (history, logging, delays) into the shell loop without breaking core
- **Rich Console UI** — 16+ header styles, 10+ prompt styles, table formatter, colored logs
- **Cross-Platform** — Works on Windows, Linux, and macOS via .NET
- **Lightweight** — Zero external dependencies, minimal footprint
- **Extensible** — Easy to extend with custom commands, shells, and authentication handlers
- **Shell Events** — Subscribe to shell lifecycle events (start, end, error, command execution, prompt render)
- **Real-Time Logging** — Log messages can appear while user is typing, without interrupting input (via `LogManager`)
- **Interface Layer** — `ICommand`, `IShell`, `IAuthenticator` define the core contracts
- **Shell Engine** — Registry, loop, templates, and API build system with delegate hooks for runtime customization
- **Command Discovery** — Automatic reflection-based loading, external command registration, and dynamic command injection
- **UI & Interaction Layer** — Colored console logging, table rendering, prompt/header templating, and user feedback system
- **Extensibility Pipeline** — Pluggable command handlers, custom prompt generators, and shell loop interceptors

### 📦 Use Cases

- **CLI Tools** — Build powerful command-line utilities with custom prompts and headers
- **DevOps Tools** — Interactive scripting and automation shells with runtime behavior injection (logging, history, delays)
- **Game Consoles** — Admin panels or debug consoles for game engines with dynamic UI
- **Educational Shells** — Learn command design patterns through hands-on shell building
- **Plugin-Based Applications** — Host external commands and extensions via `ExternalCommandManager`
- **Custom Admin Panels** — Create domain-specific shells with tailored user experience
- **Monitoring & Telemetry** — Track shell usage, command execution, and errors

### 🔧 Core Dependencies

- **.NET** — Minimum required runtime
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

git clone https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS
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

BoxaraXLibrary.GenenicLib.LTS/bin/Release/netxx.x/BoxaraXLibrary.GenenicLib.LTS.dll

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

### Or with a custom prompt:

```

ShelliftAPIBuild.Create()
.SelectCommandShellLoad("MyShell")
.WithTitle("My App", "Starting...")
.SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
.WithExtraHeaderInfo("External commands: 5")
.SelectCustomPrompt(() => $"{DateTime.Now:HH:mm:ss} > ", ConsoleColor.Cyan)
.WithAppName("MyApp")
.WithAppVersion("1.0.0")
.Build();

```

### Custom Header! (new):

```

ShelliftAPIBuild.Create()
.SelectCustomHeader(() =>
{
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("╔═══════════════════════════════════╗");
Console.WriteLine($"║  MyApp v1.0.0 - {DateTime.Now}  ║");
Console.WriteLine("╚═══════════════════════════════════╝");
Console.ResetColor();
})
.Build();

// Or use a method
private void RenderHeader()
{
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine($"=== MyApp v1.0.0 - {DateTime.Now:HH:mm:ss} ===");
Console.ResetColor();
}

ShelliftAPIBuild.Create()
.SelectCustomHeader(RenderHeader)
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
## Custom Header (New!):
`User-defined - full control over content, colors, borders, and layout`
### Shell Loop Customization (NEW)

You can hook into the shell loop using delegates without breaking the core loop.

```

ShelliftAPIBuild.Create()
.SelectCommandShellLoad("MyShell")
.WithTitle("My App", "Starting...")
.SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
.SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
.WithAppName("MyApp")
.WithAppVersion("1.0.0")
.WithInputProvider(() => Console.ReadLine()?.Trim() ?? string.Empty)
.WithPreProcessor((input) =>
{
// Save command history
if (!string.IsNullOrEmpty(input))
history.Add(input);
})
.WithPostProcessor((input, success) =>
{
Console.WriteLine($"[DEBUG] '{input}' => {success}");
Thread.Sleep(3000); // Wait 3 seconds after each command
})
.WithExitCondition(() => false) // Framework still controls exit
.Build();

```

### Shell Title Customization (NEW)
You can hook into the title setting process with pre/post actions.

```

ShelliftAPIBuild.Create()
.SelectCommandShellLoad("MyShell")
.WithTitle("My App - Main Shell", "Starting application")
.WithTitlePreAction((title, reasons, timestamp, file) =>
{
Console.WriteLine($"[DEBUG] About to set title: {title}");
})
.WithTitlePostAction((title, reasons, timestamp, file) =>
{
    Console.WriteLine($"[DEBUG] Title set: {title} at {timestamp:HH:mm:ss}");
})
.Build();

```

### Command Processor Hooks (NEW)

You can hook into command execution before and after processing.

```

ShelliftAPIBuild.Create()
.SelectCommandShellLoad("MyShell")
.WithTitle("My App", "Starting...")
.SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
.SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
.WithAppName("MyApp")
.WithAppVersion("1.0.0")
.WithCommandPreAction((command, args) =>
{
Console.WriteLine($"[DEBUG] Executing: {command} with args: {string.Join(", ", args)}");
})
.WithCommandPostAction((command, args, success) =>
{
    Console.WriteLine($"[DEBUG] '{command}' => {(success ? "OK" : "FAIL")}");
})
.Build();

```

## 💬 Prompt Styles

- `Default` — BoxaraHS>
- `Linux` — [user@hostname]$
- `FullInfo` — [user@hostname BoxaraHS]>
- `Minimal` — $
- `SimpleArrow` — ➜ BoxaraHS $
- `Brackets` — [BoxaraHS]>
- `DoubleArrow` — >> BoxaraHS >>
- `Custom`       -> User-defined (dynamic)

---

## 📝 Logging

```

LogConsole.WriteLine("Hello", DateTime.Now.ToString());
LogConsole.Clear(IsShowShell: true);
LogConsole.ForegroundColor = ConsoleColor.Green;

```

## 📝 Real-Time Logging (NEW)

The framework provides `LogManager` for real-time logging while the shell is running.

```csharp
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;

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

### ✨ Features

- **Real-Time Logging** — Logs appear immediately without blocking user input.
- **Automatic Prompt Re-Rendering** — The prompt automatically re-renders after a log message.
- **Continuous Input** — Users can continue typing while background logs arrive.
- **Full Backspace Support** — Backspace handling remains functional while logging.
- **Thread-Safe Rendering** — Use `lock (LogManager.RenderLock)` for custom rendering.

> ⚠️ **Important:** Do not use `Console.WriteLine()` directly while the shell is running. Use `LogManager.Log()` instead.

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

### 🎨 Custom Header

You can define custom headers with full control over rendering.

Using lambda:

```

.SelectCustomHeader(() =>
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"╔══ {appName} v{appVersion} - {DateTime.Now} ══╗");
    Console.ResetColor();
})

```

## **Using method**:

```

private void RenderCustomHeader()
{
    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"╔═══════════════════════════════════════╗");
    Console.WriteLine($"║  MyApp v1.0.0 - {time}               ║");
    Console.WriteLine($"╚═══════════════════════════════════════╝");
    Console.ResetColor();
}

.SelectCustomHeader(RenderCustomHeader)

```

⚠️ Cannot use both SelectShellHeaderTemplate and SelectCustomHeader together.
### 🎨 Custom Dynamic Prompt

You can define dynamic prompts that update every time the shell renders.

```

Using lambda:
.SelectCustomPrompt(() => $"{DateTime.Now:HH:mm:ss} > ", ConsoleColor.Cyan)

Using method:
private string GetPrompt()
{
    return $"[{Environment.UserName}@{Environment.MachineName}] ";
}
.SelectCustomPrompt(GetPrompt, ConsoleColor.Magenta)

```

⚠️ Cannot use both SelectShellPrompt and SelectCustomPrompt together.
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
- **Shell loop** uses delegate hooks (`WithInputProvider`, `WithPreProcessor`, `WithPostProcessor`, `WithExitCondition`) without breaking the core loop.
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
### Shell Events (NEW)

You can subscribe to shell lifecycle events without modifying the core flow.

```

ShelliftAPIBuild.Create()
    .SelectCommandShellLoad("MyShell")
    .WithTitle("My App", "Starting...")
    .SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
    .SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
    .WithAppName("MyApp")
    .WithAppVersion("1.0.0")
    .OnShellStart(() => Console.WriteLine("[EVENT] Shell started!"))
    .OnShellEnd(() => Console.WriteLine("[EVENT] Shell ended!"))
    .OnShellError((ex) => Console.WriteLine($"[EVENT] Error: {ex.Message}"))
    .OnCommandsLoaded((cmds) => Console.WriteLine($"[EVENT] Loaded {cmds.Count} commands"))
    .OnCommandExecuted((cmd) => Console.WriteLine($"[EVENT] Command '{cmd}' executed"))
    .OnCommandFailed((cmd) => Console.WriteLine($"[EVENT] Command '{cmd}' failed"))
    .OnPromptRendered((prompt) => Console.WriteLine($"[EVENT] Prompt: {prompt}"))
    .Build();

```

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

### 🧩 Prompt Selection

Cannot use both SelectShellPrompt and SelectCustomPrompt together.

## `✅ CORRECT: .SelectCustomPrompt(GetPrompt, ConsoleColor.Cyan)`
## `❌ WRONG: .SelectShellPrompt(PromptStyle.FullInfo).SelectCustomPrompt(GetPrompt)`
### 🔄 Shell Loop Customization

The shell loop is **framework-controlled**. App can only hook via delegates:

- `WithInputProvider` — Customize input reading
- `WithPreProcessor` — Run logic before command execution (e.g., history)
- `WithPostProcessor` — Run logic after command execution (e.g., logging, delay)
- `WithExitCondition` — Check exit condition (framework still controls exit)

⚠️ **App cannot render prompt, process command, or exit the loop directly.**
### 🧩 Header Selection

Cannot use both SelectShellHeaderTemplate and SelectCustomHeader together.

✅ CORRECT: .SelectCustomHeader(RenderCustomHeader)
❌ WRONG: .SelectShellHeaderTemplate(HeaderStyle.Modern).SelectCustomHeader(RenderCustomHeader)
### 🔄 Framework Lifecycle

| Phase | Action | Who |
| --- | --- | --- |
| **Initialization** | `ShellRegistry.Initialize()` discovers all `IShell` and `ICommand` types | Framework |
| **Shell Build** | `ShelliftAPIBuild.Build()` creates and runs the shell | Framework |
| **Command Processing** | `CommandProcessorTemplate.Process()` handles input, merges external commands | Framework |
| **External Commands** | `ExternalCommandManager` holds commands registered by the app | App (via framework API) |
### 🧩 Title Customization

Use `WithTitlePreAction` and `WithTitlePostAction` to hook into title setting:

- `WithTitlePreAction` — runs before the title is set (logging, debugging)
- `WithTitlePostAction` — runs after the title is set (logging, tracking)

⚠️ **Read-only**: You cannot modify the title, reasons, timestamp, or filename inside these actions.

### 🧩 Command Processor Hooks

Use `WithCommandPreAction` and `WithCommandPostAction` to hook into command execution:

- `WithCommandPreAction` — runs before command lookup (logging, tracking)
- `WithCommandPostAction` — runs after command execution with result (logging, metrics)

⚠️ **Read-only**: You cannot modify the command name, arguments, or execution flow inside these actions.
### 🧩 Shell Events

Use shell events to hook into the shell lifecycle:

- `OnShellStart` — runs when shell begins
- `OnShellEnd` — runs when shell ends
- `OnShellError` — runs when shell encounters an error
- `OnCommandsLoaded` — runs after commands are loaded
- `OnCommandExecuted` — runs after a command executes successfully
- `OnCommandFailed` — runs when a command fails
- `OnPromptRendered` — runs when the prompt is rendered

⚠️ **Read-only**: You cannot modify the shell flow or data inside these events.
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

1. **✅ ALWAYS** use `AssemblyLoadContext` for external assembly loading.
2. **✅ ALWAYS** call `Unload()` when done with external assemblies.
3. **✅ ALWAYS** use `ExternalCommandManager` to register external commands.
4. **✅ ALWAYS** check `ShellExists()` before registering commands for a shell.
5. **✅ ALWAYS** call `ShellRegistry.Initialize()` before opening a shell.
6. **✅ ALWAYS** use `WithTitlePreAction` / `WithTitlePostAction` to hook into title changes.
7. **✅ ALWAYS** use `WithCommandPreAction` / `WithCommandPostAction` to hook into command execution.
8. **✅ ALWAYS** use `OnShellStart`, `OnShellEnd`, `OnShellError` for shell lifecycle events.
9. **✅ ALWAYS** use `OnCommandsLoaded`, `OnCommandExecuted`, `OnCommandFailed`, `OnPromptRendered` for runtime events.
10. **❌ NEVER** modify shell data or flow inside event handlers.
11. **❌ NEVER** use `Assembly.LoadFrom` without a collectible context.
12. **❌ NEVER** hardcode shell names in framework extensions.
13. **❌ NEVER** hold references to types from unloaded assemblies.
14. **❌ NEVER** ignore thread safety when accessing shared state.
15. **❌ NEVER** call `Build()` from a non-`IShell` class.
16. **❌ NEVER** modify title, reasons, timestamp, or filename inside pre/post actions (read-only).
17. **❌ NEVER** modify command name, arguments, or execution flow inside pre/post actions.

## 📄 License

This project is licensed under the **Apache License 2.0**.

[View full license](https://www.apache.org/licenses/LICENSE-2.0)

---

Copyright (c) 2026 JuliHyro Studios Workspace

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at:

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

## **Author:** JuliHyro Studios Workspace
## **Project:** BoxaraXLibrary.GenenicLib.LTS
## **Version:** 1.0.6-LTS
