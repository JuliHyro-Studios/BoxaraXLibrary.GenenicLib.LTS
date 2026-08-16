using BoxaraXLibrary.GenenicLib.LTS.Commons.basicUtils;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.Interface
{
    public interface IAuthenticator
    {
        AuthMode Mode { get; }
        string DisplayName { get; }
        string Description { get; }
        bool Authenticate(string prompt, int timeRedirect);
    }
}