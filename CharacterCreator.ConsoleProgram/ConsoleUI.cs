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

        private string _baseURL;
        private TokenResponse _token = null;

        public ConsoleUI()
        {
            _baseURL = "https://localhost:" + _localURLCode;
        }

        public void Run()
        {
            Run_LoginMenu();
        }

        public void Run_LoginMenu()
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
                            
                            Console.WriteLine("\nLogin successful. Press any key to continue.");
                            Console.ReadLine();

                            ConsoleUI_MainMenu nextConsole = new ConsoleUI_MainMenu(_baseURL, _token);
                            GoToNextPage("Player menu");
                            nextConsole.Run_MainMenu();
                            GoBack();

                            // Logout
                            _token = null;

                            break;
                        }

                        // Failed login
                        Console.WriteLine("\nLogin unsuccessful. Press any key to continue.");
                        Console.ReadLine();
                        break;

                    case "2":
                        if (AskForLogin(false))
                        {
                            Console.WriteLine("\nRegistration successful. Press any key to continue.");
                            Console.ReadLine();

                            break;
                        }

                        // Failed regristration
                        Console.WriteLine("\nRegistration unsuccessful. Press any key to continue.");
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
            Console.WriteLine("\nPlease enter your 5 digit URL code:");
            string response = Console.ReadLine();

            if (response is null || response.Trim() == "" || response.Length != 5)
            {
                PrintErrorMessageForInput($"'{response}' is not a valid URL code. Press any key to return to the main menu.");
                return false;
            }

            _localURLCode = response;
            _baseURL = "https://localhost:" + _localURLCode;
            return true;
        }

        public bool AskForLogin(bool isExistingUser)
        {
            Console.WriteLine(isExistingUser ? "\nPlease enter your email:" : "\nPlease enter a new email address:");
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
                    confirmPassword = _defaultPassword;
                    //PrintErrorMessageForInput($"This is not a valid password. Press any key to return to the main menu.");
                    //return false;
                }
                else if(password != confirmPassword)
                {
                    PrintErrorMessageForInput($"These password do not match. Press any key to return to the main menu.");
                    return false;
                }

                Console.WriteLine("\nAttempting to register. Please wait.");

                // Try to register new profile
                return AttemptToRegisterUser(email, password, confirmPassword);
            }

            Console.WriteLine("\nAttempting to login. Please wait.");

            // Try to login to endpoint
            return (AttemptLogin(email, password));
        }

        public bool AttemptLogin(string email, string password)
        {
            LoginController controller = new LoginController();
            TokenResponse response = controller.GetToken(_baseURL, email, password);

            if (response is null || response.AccessToken is null)
                return false;

            _token = response;

            return true;
        }

        public bool AttemptToRegisterUser(string email, string password, string confirmPassword)
        {
            LoginController controller = new LoginController();
            bool success = controller.Register(_baseURL, email, password, confirmPassword);

            return success;
        }
    }
}
