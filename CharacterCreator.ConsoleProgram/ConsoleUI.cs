using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.ConsoleProgram
{
    public class ConsoleUI : FormattingHelpers
    {
        public void Run()
        {
            PrintLogo();
            Console.WriteLine("Welcome to the Character Creator for Dungeons & Dragons 5e!\n\n" +
                "Press any key to continue.\n");
            Console.ReadLine();
        }
    }
}
