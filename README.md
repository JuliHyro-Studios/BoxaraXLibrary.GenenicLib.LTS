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

---

## ⚠️ 📚 **IMPORTANT — READ FULL DOCUMENTATION**

> ### **This README shows BASICS ONLY!**
>
> ✅ **README.md** = Quick Start + Simple Examples
> 📖 **[devopsdoc.md](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS/blob/master/devopsdoc.md)** = **COMPLETE DOCUMENTATION** (1600+ lines)
>
> **devopsdoc.md includes:**
> - 🎯 Advanced patterns & best practices
> - 🛡️ Error handling & validation
> - 🧩 Extensibility & customization
> - 📊 Performance optimization
> - 🔄 Threading & thread safety
> - 📝 Real-world examples
> - ❓ FAQ & troubleshooting
>
> **→ [👉 Read devopsdoc.md NOW →](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS/blob/master/devopsdoc.md)**

---

### ✨ Key Features

#### 🚀 Current Capabilities (Latest)
- ⚙️ **Advanced Shell Engine** — Build interactive shells with a clean **Fluent API**, reflection-based command discovery, and a high-performance **Key-based input loop** (since v1.0.6).
- 🛠️ **Smart Command Handling** — Features an intelligent **Duplicate Command Selection** system (v1.0.7.2) to resolve overlapping aliases and support for **External Command Loading** at runtime.
- 🌈 **Rich Console UI/UX** — Extensive visual customization with **16+ professional header styles**, **10+ prompt styles**, a structured **Table Formatter**, and interactive **Question/Confirmation** prompts.
- 📝 **Enterprise Logging** — Thread-safe, real-time logging via `LogManager` that synchronizes output with the active prompt without interrupting user input.
- 📦 **Modern Infrastructure** — Zero external dependencies, full cross-platform support (.NET 7/8/9/10), and a strictly **thread-safe architecture**.

#### 📜 Evolution & Deprecations
- **Authentication**: The legacy Auth system (`IAuthenticator`) was removed in v1.0.7.3 to simplify the core.
- **Internalized APIs**: `ErrorShellTemplate` (v1.0.7.1) and `LogConsole` (v1.0.7.3) are now internal framework components to ensure stability.
- **Refined Loop**: Transitioned from `Console.ReadLine` to a more responsive `Console.ReadKey` polling mechanism in v1.0.6.
- **Lean-up**: Removed redundant helpers like `CallDll` (v1.0.7.5) to keep the library lightweight.

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

---

> **Developer note:** Cloning the repository, building from source, packing the
> NuGet package, and running the internal verification project are maintainer
> workflows. See the complete [developer documentation](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS/blob/master/devopsdoc.md) for those
> instructions.

---

## 🚀 Quick Start

### 1. Create a Shell

```csharp
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

public sealed class MyShell : IShell
{
	public string ShellName => "MyShell";
	public string DisplayName => "My custom shell";
	public string Description => "My custom shell";
	public string Category => "Demo";
	public string ShellVersion => "1.0.0";

	public void Execute()
	{
		ShelliftAPIBuild.Create()
			.SelectCommandShellLoad(ShellName)
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

##  UI Customization

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

### LogManager (Real-Time)

```csharp
// Clear the screen safely
LogManager.Clear();

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
	.SelectShellHeaderTemplate(HeaderStyle.Modern, "Welcome!\n")
	.SelectShellPrompt(PromptStyle.FullInfo, "MyShell")
	.Build();
```

> ⚠️ **Important:** Use `LogManager.Log()` and `LogManager.Clear()` instead of direct console methods while the shell is running to avoid interfering with user input.

---

## 🔗 Continue Reading

This README covers the basics to get you started. For **comprehensive documentation**, advanced patterns, and detailed API reference, see:

### 📚 **[devopsdoc.md](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS/blob/master/devopsdoc.md)** — Complete Developer Guide

- **Core Concepts** —  Detailed interface documentation
- **Shell Engine** — ShelliftAPIBuild, ShellRegistry, events
- **Command System** — Command discovery, external loading, processors
- **Error Handling** — ErrorShellTemplate with examples
- **Logging System** — LogManager deep dive
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
| [devopsdoc.md](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS/blob/master/devopsdoc.md) | Full API reference & patterns |
| [AuthorInfo.md](https://github.com/JuliHyro-Studios/BoxaraXLibrary.GenenicLib.LTS/blob/master/AuthorInfo.md) | Author & contribution info |

---

## ⚖️ License

This project is licensed under **Apache License 2.0**.

[View full license](https://www.apache.org/licenses/LICENSE-2.0)

Copyright (c) 2026 JuliHyro Studios Workspace
