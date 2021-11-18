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
            Run_MainMenu();
        }

        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintLogo();

                Console.WriteLine("Welcome to the Character Creator for Dungeons & Dragons 5e! What would you like to do?");
                Console.WriteLine("1. Login to an existing Player account.\n" +
                        "2. Create a new Player account.\n" +
                        "3. Quit.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        //if(AskForLogin())
                            //GoToNextPage("Player");
                            //_consoleUI_Party.Menu_CreateParty();
                            //GoBack();
                        break;
                    case "2":
                        //GoToNextPage("View Existing Parties");
                        //_consoleUI_Party.Menu_ViewOrUpdate_All();
                        //GoBack();
                        break;
                    case "3":
                        // Quit
                        Environment.Exit(0);
                        return;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }

                Console.ReadLine();
            }
        }

        public bool AskForLogin(bool isExistingUser)
        {
            Console.WriteLine(isExistingUser ? "\n\nPlease enter your email:" : "\n\nPlease enter a new email address:");
            string email = Console.ReadLine();

            if (email is null || email.Trim() == "")
            {
                PrintErrorMessageForInput($"'{email}' is not a valid email. Press any key to return to the main menu.");
                return false;
            }

            Console.WriteLine(isExistingUser ? "\n\nPlease enter your password:" : "\n\nPlease enter a new password:");
            string password = Console.ReadLine();

            if (password is null || password == "")
            {
                PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                return false;
            }

            if(!isExistingUser)
            {
                Console.WriteLine("\n\nPlease confirm your password:");
                string passwordCheck = Console.ReadLine();

                if (passwordCheck is null || passwordCheck == "")
                {
                    PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                    return false;
                }
                else if(password != passwordCheck)
                {
                    PrintErrorMessageForInput($"These password do not match. Press any key to return to the main menu.");
                    return false;
                }
            }

            // Try to login to endpoint
            if (!AttemptLogin(email, password))
                return false;

            return true;
        }

        public bool AttemptLogin(string email, string password)
        {
            return true;
        }
    }
}
