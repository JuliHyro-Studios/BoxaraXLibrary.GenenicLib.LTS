using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

public sealed class Shell : IShell
{
    public string ShellName => "TestShell";

    public string DisplayName => "Test Shell";

    public string Description => "Basic ShelliftAPIBuild test shell.";

    public string Category => "Testing";

    public string ShellVersion => "1.0.0";

    public void Execute()
    {
        ShelliftAPIBuild
            .Create()
            .SelectCommandShellLoad(ShellName)
            .SelectShellHeaderTemplate(
                HeaderStyle.Classic,
                "BoxaraXLibrary.GenenicLib.LTS - Shell Test\n"
            )
            .SelectShellPrompt(PromptStyle.Default, "TestShell")
            .Build();
    }
}
