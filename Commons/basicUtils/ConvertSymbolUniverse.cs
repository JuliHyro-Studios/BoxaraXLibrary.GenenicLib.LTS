using BoxaraXLibrary.GenenicLib.LTS.Commons.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxaraXLibrary.GenenicLib.LTS.Commons.basicUtils
{
    public class ConvertSymbolUniverse
    {
        public class ConvertTextToAsterisk
        {
            public static (int code, string result) ReadMaskedInput()
            {
                string maskedInput = "";
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    if(key.Key == ConsoleKey.Backspace)
                    {
                        if(maskedInput.Length > 0) 
                        {
                             maskedInput = maskedInput.Substring(0, maskedInput.Length - 1);
                            LogConsole.Write("\b \b");

                        } 
                            
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    { }
                    else
                    {
                        maskedInput += key.KeyChar;
                        LogConsole.Write("*");

                    }
                }
                while (key.Key != ConsoleKey.Enter);
                LogConsole.WriteLine();
                if (!string.IsNullOrEmpty(maskedInput))
                    return (codeint.SUCESS, maskedInput);
                else
                    return (codeint.FAILED, "");
            }

        }
    }
}
 