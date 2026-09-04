using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle
{
    public sealed class ShelliftAPIBuild
    {
        private string _shellName = "MainShell";
        private HeaderStyle _headerStyle = HeaderStyle.Classic;
        private string _headerWelcomeMessage = "Type 'help' or 'cls' to start. Type 'exit' to quit the application!\n";
        private PromptStyle _promptStyle = PromptStyle.Default;
        private string _promptCustomName = "BoxaraHS";
        private List<ICommand> _commands = null!;
        private string? _consoleTitle = null;
        private string[] _titleReasons = Array.Empty<string>();
        private string _headerExtraInfo = "";
        private Func<string>? _customInputProvider;
        private Action<string>? _customPreProcessor;
        private Action<string, bool>? _customPostProcessor;
        private Func<bool>? _customExitCondition;
        private string? _appName = null;
        private string? _appVersion = null;
        private bool _isCustomPromptSet = false;
        private Func<string>? _customPromptGenerator;
        private ConsoleColor _customPromptColor = ConsoleColor.Cyan;
        private ShelliftAPIBuild() { }
        private Func<List<PromptSegment>, List<ICommand>, Action>? _customLoopBuilder;
        private bool _isCustomHeaderSet = false;
        private Action? _customHeaderRenderer;  
        private Action<string, string[], DateTime, string>? _titlePreAction;
        private Action<string, string[], DateTime, string>? _titlePostAction;
        private Action<string, string[]>? _commandPreAction;
        private Action<string, string[], bool>? _commandPostAction;
        private Action? _onShellStart;
        private Action? _onShellEnd;
        private Action<Exception>? _onShellError;
        private Action<List<ICommand>>? _onCommandsLoaded;
        private Action<string>? _onCommandExecuted;
        private Action<string>? _onCommandFailed;
        private Action<string>? _onPromptRendered;
        public static ShelliftAPIBuild Create()
        {
            var stackTrace = new StackTrace(true);
            var callerFrame = stackTrace.GetFrame(1);
            var callerMethod = callerFrame?.GetMethod();
            var callerType = callerMethod?.DeclaringType;
            var callerFile = callerFrame?.GetFileName();
            var callerLine = callerFrame?.GetFileLineNumber();

            if (callerType != null && !typeof(IShell).IsAssignableFrom(callerType))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("╔══════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║  [FRAMEWORK ERROR] ShelliftAPIBuild.Create()                   ║");
                Console.WriteLine("╠══════════════════════════════════════════════════════════════════╣");
                Console.WriteLine($"║  Caller   : {callerType?.FullName}.{callerMethod?.Name}()");
                Console.WriteLine($"║  File     : {callerFile ?? "Unknown"} (line {callerLine})");
                Console.WriteLine("║  Reason   : Caller must implement IShell interface.            ║");
                Console.WriteLine("╚══════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();

                throw new InvalidOperationException(
                    $"ShelliftAPIBuild.Create() must be called from a class implementing IShell. " +
                    $"Caller: {callerType?.FullName}.{callerMethod?.Name}()"
                );
            }

            return new ShelliftAPIBuild();
        }

        
        public static void OpenShell(string shellName)
        {
            LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ShellRegistry.OpenShell(shellName);
        }

        public static int OpenShellWithResult(string shellName)
        {
            LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return ShellRegistry.OpenShell(shellName);
        }

        public static void OpenShell(string shellName, Action<ShelliftAPIBuild>? config)
        {
            var api = Create().SelectCommandShellLoad(shellName);
            config?.Invoke(api);
            LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            api.Build();
        }

        public static (int Code, string Message) OpenShellWithResult(string shellName, Action<ShelliftAPIBuild>? config)
        {
            try
            {
                var api = Create().SelectCommandShellLoad(shellName);
                config?.Invoke(api);
                LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                api.Build();

                return (codeint.SUCESS, $"Open Shell '{shellName}' OK");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[CRITICAL LIBRARY EXCEPTION] Failed inside OpenShellWithResult for shell '{shellName}':");
                Console.WriteLine(ex.ToString());
                Console.ResetColor();

                return (codeint.FAILED, ex.ToString());
            }
        }

        
        public ShelliftAPIBuild SelectCommandShellLoad(string shellName)
        {
            _shellName = shellName;
            return this;
        }
        public ShelliftAPIBuild SelectCustomHeader(Action renderHeader)
        {
            if (_isCustomHeaderSet)
            {
                throw new InvalidOperationException(
                    "Cannot call SelectCustomHeader() after SelectShellHeaderTemplate() has been used. " +
                    "Choose one: either built-in header OR custom header generator."
                );
            }

            _isCustomHeaderSet = true;
            _customHeaderRenderer = renderHeader;
            return this;
        }
        public ShelliftAPIBuild SelectShellHeaderTemplate(HeaderStyle style, string? welcomeMessage = null)
        {
            if (_isCustomHeaderSet)
            {
                throw new InvalidOperationException(
                    "Cannot call SelectShellHeaderTemplate() after SelectCustomHeader() has been used. " +
                    "Choose one: either built-in header OR custom header generator."
                );
            }

            _headerStyle = style;
            if (!string.IsNullOrEmpty(welcomeMessage))
                _headerWelcomeMessage = welcomeMessage;
            return this;
        }

        public ShelliftAPIBuild SelectShellPrompt(PromptStyle style, string customName = "BoxaraHS")
        {
            if (_isCustomPromptSet)
            {
                throw new InvalidOperationException(
                    "Cannot call SelectShellPrompt() after SelectCustomPrompt() has been used. " +
                    "Choose one: either built-in prompt styles OR custom prompt generator."
                );
            }

            _promptStyle = style;
            _promptCustomName = customName;
            return this;
        }
        public ShelliftAPIBuild SelectCustomPrompt(Func<string> promptGenerator, ConsoleColor color = ConsoleColor.Cyan)
        {
            if (_promptStyle != PromptStyle.Default && _promptStyle != PromptStyle.Custom)
            {
                throw new InvalidOperationException(
                    "Cannot call SelectCustomPrompt() after SelectShellPrompt() has been used. " +
                    "Choose one: either built-in prompt styles OR custom prompt generator."
                );
            }

            _isCustomPromptSet = true;
            _customPromptGenerator = promptGenerator;
            _customPromptColor = color;
            return this;
        }
        public ShelliftAPIBuild WithInputProvider(Func<string> inputProvider)
        {
            _customInputProvider = inputProvider;
            return this;
        }

        public ShelliftAPIBuild WithPreProcessor(Action<string> preProcessor)
        {
            _customPreProcessor = preProcessor;
            return this;
        }

        public ShelliftAPIBuild WithPostProcessor(Action<string, bool> postProcessor)
        {
            _customPostProcessor = postProcessor;
            return this;
        }

        public ShelliftAPIBuild WithExitCondition(Func<bool> exitCondition)
        {
            _customExitCondition = exitCondition;
            return this;
        }
        public ShelliftAPIBuild WithCustomHeader(string customHeader)
        {
            _headerWelcomeMessage = customHeader;
            return this;
        }
        public ShelliftAPIBuild WithExtraHeaderInfo(string extraInfo)
        {
            _headerExtraInfo = extraInfo;
            return this;
        }
        public ShelliftAPIBuild WithCustomPromptText(string customText)
        {
            _promptCustomName = customText;
            return this;
        }
        public ShelliftAPIBuild WithCommandPreAction(Action<string, string[]> preAction)
        {
            _commandPreAction = preAction;
            return this;
        }

        public ShelliftAPIBuild WithCommandPostAction(Action<string, string[], bool> postAction)
        {
            _commandPostAction = postAction;
            return this;
        }

        public ShelliftAPIBuild OnShellStart(Action onStart)
        {
            _onShellStart = onStart;
            return this;
        }

        public ShelliftAPIBuild OnShellEnd(Action onEnd)
        {
            _onShellEnd = onEnd;
            return this;
        }

        public ShelliftAPIBuild OnShellError(Action<Exception> onError)
        {
            _onShellError = onError;
            return this;
        }

        public ShelliftAPIBuild OnCommandsLoaded(Action<List<ICommand>> onLoaded)
        {
            _onCommandsLoaded = onLoaded;
            return this;
        }

        public ShelliftAPIBuild OnCommandExecuted(Action<string> onExecuted)
        {
            _onCommandExecuted = onExecuted;
            return this;
        }

        public ShelliftAPIBuild OnCommandFailed(Action<string> onFailed)
        {
            _onCommandFailed = onFailed;
            return this;
        }

        public ShelliftAPIBuild OnPromptRendered(Action<string> onRendered)
        {
            _onPromptRendered = onRendered;
            return this;
        }

        public ShelliftAPIBuild WithTitle(string title, params string[] reasons)
        {
            _consoleTitle = title;
            _titleReasons = reasons;
            return this;
        }
        public ShelliftAPIBuild WithTitlePreAction(Action<string, string[], DateTime, string> preAction)
        {
            _titlePreAction = preAction;
            return this;
        }

        public ShelliftAPIBuild WithTitlePostAction(Action<string, string[], DateTime, string> postAction)
        {
            _titlePostAction = postAction;
            return this;
        }
        public ShelliftAPIBuild WithAppName(string appName)
        {
            _appName = appName ?? "BoxaraHS";
            return this;
        }

        public ShelliftAPIBuild WithAppVersion(string appVersion)
        {
            _appVersion = appVersion ?? "1.0.0";
            return this;
        }
        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(_shellName))
            {
                throw new InvalidOperationException(
                    $"[VALIDATION ERROR] Shell name is required. Call SelectCommandShellLoad(shellName) before Build()."
                );
            }

            if (_isCustomHeaderSet && _customHeaderRenderer == null)
            {
                throw new InvalidOperationException(
                    "[VALIDATION ERROR] Custom header was configured but no renderer provided. " +
                    "Use SelectCustomHeader(renderAction) correctly."
                );
            }

            if (_isCustomPromptSet && _customPromptGenerator == null)
            {
                throw new InvalidOperationException(
                    "[VALIDATION ERROR] Custom prompt was configured but no generator provided. " +
                    "Use SelectCustomPrompt(generatorFunc) correctly."
                );
            }
        }

        private void ValidateCommandsLoaded(List<ICommand> commands)
        {
            if (commands == null)
            {
                throw new InvalidOperationException(
                    $"[SHELL ERROR] Failed to load commands for shell '{_shellName}'. " +
                    $"ReflectionCommandShellTemplate.LoadCommandsForShell() returned null. " +
                    $"Ensure the shell is registered in ShellRegistry."
                );
            }

        }

        public int Build()
        {
            try
            {
                ValidateConfiguration();

                _onShellStart?.Invoke();

                LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (!string.IsNullOrEmpty(_consoleTitle))
                {
                    CommandPromptTitleSEt.SetTitle(
                        _consoleTitle,
                        _titleReasons,
                        DateTime.Now,
                        preAction: _titlePreAction,
                        postAction: _titlePostAction
                    );
                }

                _commands = ReflectionCommandShellTemplate.LoadCommandsForShell(_shellName);

                // Validate commands were loaded
                ValidateCommandsLoaded(_commands);

                _onCommandsLoaded?.Invoke(_commands);

                string fullWelcomeMessage = $"{_headerWelcomeMessage}{_headerExtraInfo}";

                if (_isCustomHeaderSet && _customHeaderRenderer != null)
                {
                    ShellHeaderTemplate.ShowCustom(_customHeaderRenderer);
                }
                else
                {
                    ShellHeaderTemplate.Show(
                        _commands,
                        fullWelcomeMessage,
                        _headerStyle,
                        _appName ?? "BoxaraHS",
                        _appVersion ?? "1.0.0"
                    );
                }

                List<PromptSegment> promptSegments;
                if (_isCustomPromptSet && _customPromptGenerator != null)
                {
                    promptSegments = CommandPromptTemplate.GetCustomPrompt(_customPromptGenerator, _customPromptColor);
                }
                else
                {
                    promptSegments = CommandPromptTemplate.GetPrompt(_promptStyle, _promptCustomName);
                }

                foreach (var segment in promptSegments)
                {
                    _onPromptRendered?.Invoke(segment.Text);
                }

                var wrappedCommandPostAction = new Action<string, string[], bool>((cmd, args, success) =>
                {
                    _commandPostAction?.Invoke(cmd, args, success);

                    if (success)
                    {
                        _onCommandExecuted?.Invoke(cmd);
                    }
                    else
                    {
                        _onCommandFailed?.Invoke(cmd);
                    }
                });

                ShellLoopTemplate.Run(
            promptSegments,
            _commands,
            _shellName, 
            _customInputProvider,
            _customPreProcessor,
            _customPostProcessor,
            _customExitCondition,
            _commandPreAction,
            _commandPostAction
        );

                _onShellEnd?.Invoke();

                return codeint.SUCESS;
            }
            catch (Exception ex)
            {
                _onShellError?.Invoke(ex);
                return codeint.FAILED;
            }
        }


    }
}