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
    public class ConsoleUI_MainMenu : FormattingHelpers
    {
        private string _baseURL;
        private TokenResponse _token;
        private readonly LocalPlayerController _controller;
        private Player _player = new Player();

        public ConsoleUI_MainMenu(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _token = token;
            _controller = new LocalPlayerController(baseURL, token);

            // Get player object
            _player = _controller.GetFirstPlayerForToken();
        }

        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle($"Welcome back {_player.Name}! What would you like to do:");

                //Console.WriteLine("Welcome to the Character Creator for Dungeons & Dragons 5e! What would you like to do?");
                Console.WriteLine("1. Change player name.\n" +
                        "2. View all characters.\n" +
                        "3. Create a new characters.\n" +
                        "4. Delete a character.\n" +
                        "5. Logout.\n" +
                        "6. Quit.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Change player name
                        UpdatePlayerName();
                        break;

                    case "2":
                        // View all characters

                        break;

                    case "3":
                        // Create a new character

                        break;

                    case "4":
                        // Delete a character

                        break;

                    case "5":
                        // Logout and return to main menu
                        return;

                    case "6":
                        // Quit
                        Environment.Exit(0);
                        return;

                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }

        public void UpdatePlayerName()
        {
            Console.Write("Step 1 of 1: ");
            string name = AskUser_StringInput("Enter your new name:");
            if (name is null || name == "")
                return;

            Console.WriteLine("\nAttempting to update Player. Please wait.");

            // Update Player object
            bool success = _controller.UpdatePlayerName(_player, name);

            if(success)
            {
                Console.WriteLine("\nAttempting to retrieve updated Player. Please wait.");
                _player = _controller.GetFirstPlayerForToken();
            }else
            {
                PrintErrorMessageForInput($"There was an error updating the Player. Press any key to return to the menu.");
            }

            Console.WriteLine("\nUpdate successful. Press any key to continue.");
            Console.ReadLine();

            return;
        }
    }
}
