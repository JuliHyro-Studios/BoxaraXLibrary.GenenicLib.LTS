using System;
using System.Collections.Generic;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
using BoxaraXLibrary.GenenicLib.LTS.Commons.basicUtils;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.AuthHandle
{
    public static class AuthenticationHelper
    {
        private static List<IAuthenticator>? _cached = null;
        private static bool _initialized = false;
        private static readonly object _lock = new object();

        public static int Initialize()
        {
            lock (_lock)
            {
                if (_initialized) return codeint.SUCESS;

                try
                {
                    _cached = ReflectionAuthenticatorTemplate.LoadAllAuthenticators();
                    _initialized = true;
                    return codeint.SUCESS;
                }
                catch
                {
                    _cached = new List<IAuthenticator>();
                    return codeint.FAILED;
                }
            }
        }

        public static bool Authenticate(AuthMode mode, string prompt, int timeRedirect = 500)
        {
            if (!_initialized)
            {
                if (Initialize() == codeint.FAILED)
                {
                    LogConsole.WriteLine("[X] Authenticator system init failed.");
                    return false;
                }
            }

            try
            {
                var auth = _cached?.Find(a => a.Mode == mode);
                if (auth != null)
                {
                    return auth.Authenticate(prompt, timeRedirect);
                }

                LogConsole.ForegroundColor = ConsoleColor.Red;
                LogConsole.WriteLine($"[X] No authenticator found for mode: {mode}");
                LogConsole.ResetColor();
                return false;
            }
            catch (Exception ex)
            {
                LogConsole.ForegroundColor = ConsoleColor.Red;
                LogConsole.WriteLine($"[X] Auth error: {ex.Message}");
                LogConsole.ResetColor();
                return false;
            }
        }
    }
}