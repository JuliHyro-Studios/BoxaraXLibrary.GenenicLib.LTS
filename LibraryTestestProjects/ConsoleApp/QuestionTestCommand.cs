using System;
using BoxaraXLibrary.GenenicLib.LTS.Commons.Interface;
using BoxaraXLibrary.GenenicLib.LTS.Commons.ShellHandle;

namespace ConsoleApp
{
    public class QuestionTestCommand : ICommand
    {
        public string Name => "qtest";
        public string DisplayName => "Question System Test";
        public string[] Aliases => new[] { "qt" };
        public string Category => "Testing";
        public string Shell => "TestShell";
        public string Description => "Tests the QuestionShellTemplate functionality";
        public string CommandVersion => "1.0.0";
        public string[] Parameter => Array.Empty<string>();

        public void Execute()
        {
            Console.WriteLine("\n--- Testing Question System ---");

            // Test 1: Basic Y/N Question
            bool res1 = QuestionShellTemplate.ShowQuestion("Do you like this library?");
            Console.WriteLine($"Result 1 (Basic): {(res1 ? "Accepted" : "Declined")}");

            // Test 2: Custom Text Question
            bool res2 = QuestionShellTemplate.ShowQuestion(
                "Do you want to continue?",
                confirmText: "Yes",
                cancelText: "No"
            );
            Console.WriteLine($"Result 2 (Custom Text): {(res2 ? "Accepted" : "Declined")}");

            // Test 3: Timeout Question (wait 5 seconds)
            Console.WriteLine("\n[!] Next question will timeout in 5 seconds if no input...");
            bool res3 = QuestionShellTemplate.ShowQuestion(
                "Are you still there?",
                timeoutSeconds: 5,
                timeoutMessage: "Too slow! Defaulting to False.",
                continueOnTimeout: false
            );
            Console.WriteLine($"Result 3 (Timeout): {(res3 ? "Accepted" : "Declined")}");

            Console.WriteLine("--- Test Completed ---\n");
        }

        public void ParameterExecute(string[] args)
        {
            Execute();
        }
    }
}
