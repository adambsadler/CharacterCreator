using CharacterCreator.ConsoleProgram.ControllerClasses;
using CharacterCreator.ConsoleProgram.ModelClasses;
using CharacterCreator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.ConsoleProgram
{
    public class ConsoleUI_BackgroundList : FormattingHelpers
    {
        private string _baseURL;
        private TokenResponse _token;
        private readonly LocalBackgroundController _controller;
        private List<Background> _backgrounds = new List<Background>();

        public ConsoleUI_BackgroundList(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _token = token;
            _controller = new LocalBackgroundController(baseURL, token);

            // Get backgrounds
            _backgrounds = _controller.GetBackgrounds();
        }

        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle($"List of backgrounds:");

                PrintAllBackgrounds();

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a Background Id to view background information information " +
                    "or press enter to return to the previous menu:\n");
                string response = Console.ReadLine();

                // Return to previous screen if no response input
                if (response is null || response == "")
                    return;

                // Otherwise, try to view that background
                try
                {
                    int id = int.Parse(response);
                    Background specificBackground = _backgrounds.FirstOrDefault(b => b.BackgroundId == id);

                    if (specificBackground is null)
                    {
                        PrintErrorMessageForInput(response);
                    }
                    else
                    {
                        Console.WriteLine("\n" + CONST_DASHES + $"\n");
                        Console.WriteLine("{0,-15}{1,-50}", "Background Id:", specificBackground.BackgroundId);
                        Console.WriteLine("{0,-15}{1,-50}", "Name:", specificBackground.Name);
                        Console.WriteLine("{0,-15}{1,-30}", "Feature:", specificBackground.Feature);
                        Console.WriteLine("{0,-15}{1, -200}\n", "Description:", specificBackground.Description);
                        Console.ReadLine();
                    }
                }
                catch
                {
                    PrintErrorMessageForInput(response);
                }
            }
        }

        public void PrintAllBackgrounds()
        {
            if (_backgrounds is null || _backgrounds.Count == 0)
            {
                Console.WriteLine("There are no backgrounds at this time. Please use Postman to enter new backgrounds.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-50}\n",
                        "Id",
                        "Name");

                foreach (Background background in _backgrounds)
                {
                    Console.WriteLine("{0,-10}{1,-50}",
                        background.BackgroundId,
                        background.Name);
                }
            }
        }
    }
}
