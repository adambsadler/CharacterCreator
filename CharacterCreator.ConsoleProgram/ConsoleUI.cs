using CharacterCreator.ConsoleProgram.ControllerClasses;
using CharacterCreator.ConsoleProgram.ModelClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace CharacterCreator.ConsoleProgram
{
    public class ConsoleUI : FormattingHelpers
    {
        LoginController _controller = new LoginController();
        private string _localURLCode = "44353";
        private string _defaultEmail = "bob@gmail.com";
        private string _defaultPassword = "Password1!";
        private TokenResponse _token = null;

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
                        $"3. Change local URL ({_localURLCode}).\n" +
                        "4. Quit.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        if (AskForLogin(true))
                        {
                            Console.WriteLine("\n\nLogin successful. Press any key to continue.");
                            Console.ReadLine();
                            //GoToNextPage("Player");
                            //_consoleUI_Party.Menu_CreateParty();
                            //GoBack();
                            break;
                        }

                        // Failed login
                        Console.WriteLine("\n\nLogin unsuccessful. Press any key to continue.");
                        Console.ReadLine();
                        break;
                    case "2":
                        if (AskForLogin(false))
                        {
                            Console.WriteLine("\n\nRegistration successful. Press any key to continue.");
                            Console.ReadLine();
                            //GoToNextPage("Player");
                            //_consoleUI_Party.Menu_CreateParty();
                            //GoBack();
                            break;
                        }

                        // Failed regristration
                        Console.WriteLine("\n\nRegistration unsuccessful. Press any key to continue.");
                        Console.ReadLine();
                        break;
                    case "3":
                        AskForLocalURLCode();
                        break;
                    case "4":
                        // Quit
                        Environment.Exit(0);
                        return;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }

        public bool AskForLocalURLCode()
        {
            Console.WriteLine("\n\nPlease enter your 5 digit URL code:");
            string response = Console.ReadLine();

            if (response is null || response.Trim() == "" || response.Length != 5)
            {
                PrintErrorMessageForInput($"'{response}' is not a valid URL code. Press any key to return to the main menu.");
                return false;
            }

            _localURLCode = response;
            return true;
        }

        public bool AskForLogin(bool isExistingUser)
        {
            Console.WriteLine(isExistingUser ? "\n\nPlease enter your email:" : "\n\nPlease enter a new email address:");
            string email = Console.ReadLine();

            if (email is null || email.Trim() == "")
            {
                email = _defaultEmail;
                //PrintErrorMessageForInput($"'{email}' is not a valid email. Press any key to return to the main menu.");
                //return false;
            }

            Console.WriteLine(isExistingUser ? "\nPlease enter your password:" : "\nPlease enter a new password:");
            string password = Console.ReadLine();

            if (password is null || password == "")
            {
                password = _defaultPassword;
                //PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                //return false;
            }

            if(!isExistingUser)
            {
                Console.WriteLine("\nPlease confirm your password:");
                string confirmPassword = Console.ReadLine();

                if (confirmPassword is null || confirmPassword == "")
                {
                    password = _defaultPassword;
                    //PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                    //return false;
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
            return (AttemptLogin(email, password));
        }

        public bool AttemptLogin(string email, string password)
        {
            string baseUrl = "https://localhost:" + _localURLCode;

            LoginController controller = new LoginController();
            TokenResponse response = controller.GetToken(baseUrl, email, password);

            if (response is null)
                return false;

            _token = response;

            return true;
        }

        public bool AttemptToRegisterUser(string email, string password, string confirmPassword)
        {
            string baseUrl = "https://localhost:" + _localURLCode;

            LoginController controller = new LoginController();
            TokenResponse response = controller.GetToken(baseUrl, email, password);

            if (response is null)
                return false;

            return true;
        }
    }
}
