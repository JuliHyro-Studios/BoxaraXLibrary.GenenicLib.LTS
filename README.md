# 📦 BoxaraXLibrary.GenenicLib.LTS

[![NuGet](https://img.shields.io/nuget/v/BoxaraXLibrary.GenenicLib.LTS?style=for-the-badge&logo=nuget&color=004880)](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS)
[![NuGet Downloads](https://img.shields.io/nuget/dt/BoxaraXLibrary.GenenicLib.LTS?style=for-the-badge&logo=nuget&color=004880)](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS)
[![GitHub Repo](https://img.shields.io/badge/GitHub-Repo-181717?style=for-the-badge&logo=github)](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS)
[![.NET Version](https://img.shields.io/badge/.NET-7.0%20%7C%208.0%20%7C%209.0%20%7C%2010.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg?style=for-the-badge)](https://www.apache.org/licenses/LICENSE-2.0)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20macOS%20%7C%20Linux-lightgrey?style=for-the-badge)](https://dotnet.microsoft.com/)

> **Generic Library LTS** — Core framework for building shell-based CLI applications with command handling, rich console UI, and fluent API. Designed for **DevOps & Engineers**.

---

## 📖 Overview

**BoxaraXLibrary.GenenicLib.LTS** is a lightweight, high-performance framework designed for building **shell-based CLI applications** in .NET. It provides a complete infrastructure for command registration, shell lifecycle management, and interactive console experiences.

### ✨ Key Features

- ⚙️ **Shell Engine** — Build and run interactive shells with customizable prompts and headers
- 🎯 **Command System** — Register, discover, and execute commands via reflection or manual registration
- 🔌 **External Command Support** — Load commands from external sources at runtime
- 🎨 **Fluent API** — Build shells with a clean, expressive fluent interface
- 🌈 **Rich Console UI** — 16+ header styles, 10+ prompt styles, table formatter, colored logs
- 📝 **Real-Time Logging** — Log messages while user is typing (via `LogManager`)
- 🔐 **Authentication** — Built-in authentication support with multiple auth modes
- 🔄 **Event System** — Subscribe to shell lifecycle events (start, end, error, command execution)
- 📦 **Cross-Platform** — Works on Windows, Linux, and macOS via .NET
- ⚡ **Lightweight** — Zero external dependencies, minimal footprint
- 🧩 **Extensible** — Easy to extend with custom commands, shells, and authenticators

### 📦 Use Cases

- **CLI Tools** — Build powerful command-line utilities
- **DevOps Tools** — Interactive automation shells
- **Game Consoles** — Admin panels or debug consoles
- **Custom Admin Panels** — Domain-specific shells with tailored UI
- **Educational Shells** — Learn command design patterns

### 🔧 Core Dependencies

- **.NET** 7.0+ (with .NET 8, 9, 10 support)
- **System.Reflection** — Command discovery
- **System.Text.Json** — Configuration serialization

---

## 📦 Installation

### Via NuGet Package

```bash
dotnet add package BoxaraXLibrary.GenenicLib.LTS
```

### Via Project Reference

```xml
<ProjectReference Include="..\BoxaraXLibrary.GenenicLib.LTS\BoxaraXLibrary.GenenicLib.LTS.csproj" />
```

---

## 🛠️ Build from Source

### 1. Clone the repository

```bash
git clone https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS
cd BoxaraXLibrary.GenenicLib.LTS
```

### 2. Build the project

Using **.NET CLI**:

```bash
dotnet build -c Release
```

Or using **Visual Studio**:

- Open `BoxaraXLibrary.GenenicLib.LTS.slnx`
- Set build configuration to **Release**
- Build the solution (Ctrl+Shift+B)

### 3. Locate the DLL

```
BoxaraXLibrary.GenenicLib.LTS/bin/Release/net7.0/BoxaraXLibrary.GenenicLib.LTS.dll
```

---

## 🚀 Quick Start

### 1. Create a Shell

```csharp
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

public class MyShell : IShellExecute
{
	public string ShellName => "MyShell";
	public string Description => "My custom shell";
	public string Category => "Demo";
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

### 2. Implement a Command

```csharp
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;

public class HelloCommand : ICommand
{
	public string Name => "hello";
	public string DisplayName => "Say Hello";
	public string[] Aliases => new[] { "hi" };
	public string Category => "Demo";
	public string Shell => "MyShell";
	public string Description => "Prints a greeting";
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

```csharp
ShellRegistry.Initialize();
ShelliftAPIBuild.OpenShellWithResult("MyShell");
```

---

## 🎯 Core Interfaces

### `IShellExecute`

Defines a shell contract.

| Property | Description |
|----------|-------------|
| `ShellName` | Unique identifier |
| `Description` | Brief description |
| `Category` | Grouping category |
| `ShellVersion` | Version string |

### `ICommand`

Defines a command contract.

| Property | Description |
|----------|-------------|
| `Name` | Command name (used in CLI) |
| `DisplayName` | User-friendly name |
| `Aliases` | Alternative names |
| `Category` | Grouping category |
| `Shell` | Target shell name |
| `Description` | Brief description |
| `CommandVersion` | Version string |
| `Parameter` | Supported parameters |

### `IAuthenticator`

Defines an authentication provider.

---

## 🎨 UI Customization

### Built-in Styles

The framework includes **16+ header styles** and **10+ prompt styles** for rich console experiences.

```csharp
// Use built-in styles
ShelliftAPIBuild.Create()
	.SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!")
	.SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
	.Build();

// Or use custom header/prompt
ShelliftAPIBuild.Create()
	.SelectCustomHeader(() => Console.WriteLine("=== Custom Header ==="))
	.SelectCustomPrompt(() => $"[{DateTime.Now:HH:mm:ss}] > ", ConsoleColor.Cyan)
	.Build();
```

---

## 📝 Logging

### LogConsole (Immediate)

```csharp
LogConsole.ForegroundColor = ConsoleColor.Green;
LogConsole.WriteLine("Success!", DateTime.Now.ToString("HH:mm:ss"));
LogConsole.ResetColor();
```

### LogManager (Real-Time)

```csharp
// Log while shell is running (non-blocking)
Task.Run(async () =>
{
	int count = 0;
	while (true)
	{
		await Task.Delay(5000);
		count++;
		LogManager.Log($"Background message {count}");
	}
});

ShelliftAPIBuild.Create()
	.SelectCommandShellLoad("MyShell")
	.Build();
```

> ⚠️ **Important:** Use `LogManager.Log()` instead of `Console.WriteLine()` while the shell is running to avoid interfering with user input.

---

## 🔗 Continue Reading

This README covers the basics to get you started. For **comprehensive documentation**, advanced patterns, and detailed API reference, see:

### 📚 **[devopsdoc.md](devopsdoc.md)** — Complete Developer Guide

- **Core Concepts** —  Detailed interface documentation
- **Shell Engine** — ShelliftAPIBuild, ShellRegistry, events
- **Command System** — Command discovery, external loading, processors
- **Error Handling** — ErrorShellTemplate with examples
- **Authentication** — IAuthenticator implementation patterns
- **Logging System** — LogConsole and LogManager deep dive
- **UI Components** — All header/prompt styles, custom templates
- **Fluent API** — Complete builder reference
- **Shell Events & Hooks** — Event subscription patterns
- **Performance** — Optimization tips
- **Thread Safety** — Thread-safety guarantees
- **Extensibility** — How to extend the framework
- **Advanced Examples** — Complex real-world scenarios
- **Best Practices** — Community recommendations
- **FAQ** — Answers to common questions

---

## 🔗 Additional Resources

| Resource | Purpose |
|----------|---------|
| [GitHub Repository](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS) | Source code & Issues |
| [NuGet Package](https://www.nuget.org/packages/BoxaraXLibrary.GenenicLib.LTS) | Package management |
| [devopsdoc.md](devopsdoc.md) | Full API reference & patterns |
| [AuthorInfo.md](AuthorInfo.md) | Author & contribution info |

---

## ⚖️ License

This project is licensed under **Apache License 2.0**.

[View full license](https://www.apache.org/licenses/LICENSE-2.0)

Copyright (c) 2026 JuliHyro Studios Workspace
