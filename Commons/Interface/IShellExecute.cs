namespace BoxaraXLibrary.GenenicLib.LTS.Commons.Interface
{
    public interface IShell
    {
        string ShellName { get; }
        string DisplayName { get; }
        string Description { get; }
        string Category { get; }
        string ShellVersion { get; }
        void Execute();
    }
}