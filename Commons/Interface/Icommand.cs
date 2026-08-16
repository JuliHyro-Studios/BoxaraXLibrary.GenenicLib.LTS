using System;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.Interface
{
    public interface ICommand
    {
        string DisplayName { get; }
        string[] Parameter { get; } 
        string Name { get; }
        string[] Aliases { get; }
        string Category { get; }
        string Shell { get; }
        string Description { get; }
        string CommandVersion { get; }

        void Execute();
        void ParameterExecute(string[] args); 
    }
}