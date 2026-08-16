using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
using BoxaraXLibrary.GenenicLib.LTS.Commons.basicUtils;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.AuthHandle
{
    public static class ReflectionAuthenticatorTemplate
    {
        public static List<IAuthenticator> LoadAllAuthenticators()
        {
            var list = new List<IAuthenticator>();

            try
            {
                var types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s =>
                    {
                        try { return s.GetTypes(); }
                        catch { return Type.EmptyTypes; }
                    })
                    .Where(t => typeof(IAuthenticator).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var type in types)
                {
                    if (Activator.CreateInstance(type) is IAuthenticator instance)
                    {
                        list.Add(instance);
                    }
                }
            }
            catch (Exception ex)
            {
                LogConsole.WriteLine($"[X] Error loading authenticators: {ex.Message}");
            }

            return list;
        }

        public static IAuthenticator? GetAuthenticatorByMode(AuthMode mode)
        {
            try
            {
                return LoadAllAuthenticators().FirstOrDefault(a => a.Mode == mode);
            }
            catch
            {
                return null;
            }
        }
    }
}