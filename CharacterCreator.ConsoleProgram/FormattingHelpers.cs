using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.ConsoleProgram
{
    public abstract class FormattingHelpers
    {
        public string CONST_DASHES = "------------------------------";
        public string CONST_DATE_FORMAT = "MMM dd, yyyy";
        private static List<string> _navigationPages = new List<string>();   // I don't want a new instance with every ConsoleUI_ class


        // Helper methods (if any)
        public void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWNNNNNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMN0xolccllodONMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMN0OKXXNNNNNNNNNNNWWMMMMMMMMMMKo:;;ldxoc:;;oKWKO0XXNNNNNNNNNNNNWWMMMMMMMMMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMW0c:ccccllllllllllodxOKWMMMMNd::;oKMMWXx:;;dNXo::cccllllllllllodxk0NMMMMMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMXxc;;;;;;;;;;;;;;;;;:lkXWMKdkd:xKXXNWO:;;oXMNOl:;;;;;;;;;;;;;;;;:cx0WMMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMWXd;;;;;;;::::;;;;;;;;ckNNK0xkOdoxk0kl;:OWMMMNk:;;;;;;::::;;;;;;;;:dXMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMMO:;;;;;lOKKK0ko:;;;;;:dX0o:ldocclldxx0WMMMMMXkddxOOk0KKK0Odc;;;;;;lKMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;oNMMMMMWOc;;;:ldkx:;::cc:;:cll0WMMMMX00xddddodkKWMMWKo;;;;;;oXMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;oNMMMMMMWx;:okK0xc;ckOl::ccoo:c0WWW0dlloddoooxk0NMMMM0c;;;;;:OMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;oNMMMMMMMO::lxOk0OdOXOdooO00koclkNKdloxdoloodKMMMMMMMKl;;;;;:OMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;oNMMMMMMWk;;okcckxcldkNKoxK0xoxkK0l:d0o;;;;;c0MMMMMMM0c;;;;;:0MMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;oNMMMMMWOc;cOOc::;;;oXMXxkKNXocodddkNXdoo:::l0MMMMMWKo;;;;;;oXMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;lOKKK0ko:;;c0O:;;;;:OMMMWMMMMKdc;;cxKKkO0OKOx0WNK0Odc;;;;;;lKMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMM0:;;;;;;::::;;;;;;:xx:;;;;:kWMMMMMMWOdddl:;:odocx0Od:dko;;;;;;;;:d0K00KNMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMNx:;;;;;;;;;;;;;;;:o0O:;;;;;cxKNNXKOo:;:xKd;;;;;:dkdc;;xx:;;;;:lxKN0kxxk0WMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMWXOxlclclllccllllodxOKWMNd::;;;;;:cl::;;;:dKWk:::;;;:;;;;lOxlooxk0NMMMX0OO0XMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMWNNNNNNNNNNNNNNNNWMMMMMMMNK0xc;;;;;;;;;cd0WMMOlxd;;;;:ldOXWNNWWMMMMMMMMWNNWMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWX0kxdxxkk0XWMMMMWXXNOdllox0NMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\n" +
                "MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMWWNNWMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(CONST_DASHES + "\n");
        }

        public void PrintTitle(string title)
        {
            string navStr = GetNavigationString();
            if (!(navStr is null || navStr == "Home"))
            {
                Console.WriteLine(GetNavigationString() + "\n\n");
            }
            Console.WriteLine(title + "\n\n" + CONST_DASHES + "\n");
        }

        public void PrintErrorMessageForInput(string input)
        {
            Console.WriteLine($"\nWe're sorry, '{input}' is not a valid input. Please try again.");
            Console.ReadLine();
        }

        public void PrintErrorMessage(string error)
        {
            Console.WriteLine($"\n{error}");
            Console.ReadLine();
        }

        public bool InterpretYesNoInput(string input)
        {
            if (input is null || input == "")
            {
                return false;
            }

            if (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
            {
                return true;
            }

            return false;
        }

        public bool ValidateStringResponse(string response, bool required)
        {
            if (response is null)
            {
                return false;
            }

            if (response == "" && required)
            {
                return false;
            }

            return true;
        }

        public bool ValidateDateResponse(string response, bool required)
        {
            if (ValidateStringResponse(response, required))
            {
                try
                {
                    DateTime date = DateTime.Parse(response);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public List<int> SplitStringIntoIDs(string rawInput)
        {
            if (rawInput is null || rawInput == "")
            {
                return null;
            }

            List<string> listOfInputs = rawInput.Split(',').ToList();

            if (listOfInputs is null || listOfInputs.Count == 0)
            {
                return null;
            }

            List<int> listOfIDs = new List<int>();
            foreach (string input in listOfInputs)
            {
                try
                {
                    listOfIDs.Add(int.Parse(input));
                }
                catch { }
            }

            return listOfIDs;

        }

        public string AskUser_StringInput(string prompt)
        {
            if (prompt is null || prompt == "")
            {
                return null;
            }

            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if (!ValidateStringResponse(response, true))
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            return response;
        }

        public int? AskUser_IntegerInput(string prompt)
        {
            if (prompt is null || prompt == "")
            {
                return null;
            }

            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if (!ValidateStringResponse(response, true))
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            try
            {
                return int.Parse(response);
            }
            catch
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            return null;
        }

        public double? AskUser_DoubleInput(string prompt)
        {
            if (prompt is null || prompt == "")
            {
                return null;
            }

            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if (!ValidateStringResponse(response, true))
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            try
            {
                return double.Parse(response.Trim('$'));
            }
            catch
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            return null;
        }

        public DateTime? AskUser_DateInput(string prompt)
        {
            if (prompt is null || prompt == "")
            {
                return null;
            }

            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if (!ValidateDateResponse(response, true))
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            DateTime date;
            date = DateTime.Parse(response);
            return date;
        }


        // Navigation bar methods
        public void GoToNextPage(string newPageName)
        {
            if (newPageName is null)
            {
                return;
            }

            _navigationPages.Add(newPageName);
            return;
        }

        public void GoBack()
        {
            if (_navigationPages is null || _navigationPages.Count == 0)
            {
                return;
            }

            // Remove latest page
            _navigationPages.RemoveAt(_navigationPages.Count - 1);
            return;
        }

        public string GetNavigationString()
        {
            if (_navigationPages is null)
            {
                return null;
            }
            else if (_navigationPages.Count == 0)
            {
                _navigationPages.Add("Home");
                return _navigationPages[0];
            }

            string formattedOutput = _navigationPages[_navigationPages.Count - 1];

            if (_navigationPages.Count == 1)
            {
                return formattedOutput;
            }

            int maxLength = 75;
            for (int i = _navigationPages.Count - 2; i >= 0; i--)
            {
                try
                {
                    if (formattedOutput.Length + 3 + _navigationPages[i].Length <= maxLength)
                    {
                        formattedOutput = $"{_navigationPages[i]} > {formattedOutput}";
                    }
                    else
                    {
                        formattedOutput = $"... > {formattedOutput}";
                        return formattedOutput;
                    }
                }
                catch
                {
                    return formattedOutput;
                }
            }

            if (formattedOutput == "")
            {
                return null;
            }

            return formattedOutput;
        }
    }
}
