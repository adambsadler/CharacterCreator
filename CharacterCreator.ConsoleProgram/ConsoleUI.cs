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
                        if (AskForLogin(true))
                            break;
                            //GoToNextPage("Player");
                            //_consoleUI_Party.Menu_CreateParty();
                            //GoBack();
                        break;
                    case "2":
                        if (AskForLogin(false))
                            break;
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

            Console.WriteLine(isExistingUser ? "\nPlease enter your password:" : "\nPlease enter a new password:");
            string password = Console.ReadLine();

            if (password is null || password == "")
            {
                PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                return false;
            }

            if(!isExistingUser)
            {
                Console.WriteLine("\nPlease confirm your password:");
                string confirmPassword = Console.ReadLine();

                if (confirmPassword is null || confirmPassword == "")
                {
                    PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                    return false;
                }
                else if(password != confirmPassword)
                {
                    PrintErrorMessageForInput($"These password do not match. Press any key to return to the main menu.");
                    return false;
                }

                // Try to register new profile
                return AttemptToRegisterUser(email, password, confirmPassword);
            }

            // Try to login to endpoint
            return (!AttemptLogin(email, password));
        }

        public bool AttemptLogin(string email, string password)
        {
            Console.WriteLine("\nAttempting to login to existing user profile.");
            Console.ReadLine();
            return true;
        }

        public bool AttemptToRegisterUser(string email, string password, string confirmPassword)
        {
            Console.WriteLine("\nAttempting to create new user profile.");
            Console.ReadLine();
            return true;
        }
    }
}
