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

        private string? _appName = null;
        private string? _appVersion = null;

        private ShelliftAPIBuild() { }

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

        public ShelliftAPIBuild SelectShellHeaderTemplate(HeaderStyle style, string? welcomeMessage = null)
        {
            _headerStyle = style;
            if (!string.IsNullOrEmpty(welcomeMessage))
                _headerWelcomeMessage = welcomeMessage;
            return this;
        }

        public ShelliftAPIBuild SelectShellPrompt(PromptStyle style, string customName = "BoxaraHS")
        {
            _promptStyle = style;
            _promptCustomName = customName;
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

        public ShelliftAPIBuild WithTitle(string title, params string[] reasons)
        {
            _consoleTitle = title;
            _titleReasons = reasons;
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

        public int Build()
        {
            try
            {
                LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (!string.IsNullOrEmpty(_consoleTitle))
                {
                    CommandPromptTitleSEt.SetTitle(_consoleTitle, _titleReasons, DateTime.Now);
                }
                ReflectionShellTemplate.SetCurrentShell(_shellName);
                _commands = ReflectionCommandShellTemplate.LoadCommandsForShell(_shellName);
                string fullWelcomeMessage = $"{_headerWelcomeMessage}{_headerExtraInfo}";
                ShellHeaderTemplate.Show(
    _commands,
    _headerWelcomeMessage,
    _headerStyle,
    _appName ?? "BoxaraHS",
    _appVersion ?? "1.0.0"
);
                var promptSegments = CommandPromptTemplate.GetPrompt(_promptStyle, _promptCustomName);
                ShellLoopTemplate.Run(promptSegments, _commands);
                return codeint.SUCESS;
            }
            catch
            {
                return codeint.FAILED;
            }
        }

        public async System.Threading.Tasks.Task<int> BuildAsync()
        {
            try
            {
                LogConsole.Clear(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (!string.IsNullOrEmpty(_consoleTitle))
                {
                    CommandPromptTitleSEt.SetTitle(_consoleTitle, _titleReasons, DateTime.Now);
                }

                _commands = ReflectionCommandShellTemplate.LoadCommandsForShell(_shellName);

                ShellHeaderTemplate.Show(
                    _commands,
                    _headerWelcomeMessage,
                    _headerStyle,
                    _appName ?? "BoxaraHS",      
                    _appVersion ?? "1.0.0"      
                );
                var promptSegments = CommandPromptTemplate.GetPrompt(_promptStyle, _promptCustomName);
                ShellLoopTemplate.Run(promptSegments, _commands);

                return codeint.SUCESS;
            }
            catch
            {
                return codeint.FAILED;
            }
        }
    }
}