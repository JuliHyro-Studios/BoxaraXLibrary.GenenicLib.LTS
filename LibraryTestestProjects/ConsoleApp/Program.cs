using System;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting BoxaraXLibrary.GenenicLib.LTS test...");

            ShelliftAPIBuild.OpenShell("TestShell");
        }
    }
}
