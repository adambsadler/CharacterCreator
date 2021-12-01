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
    public enum ListState
    {
        ViewOnly = 0,
        Delete = 1
    }

    public class ConsoleUI_CharacterList : FormattingHelpers
    {
        private ListState _state = ListState.ViewOnly;

        private string _baseURL;
        private TokenResponse _token;
        private readonly LocalCharacterController _controller;
        private List<Character> _characters = new List<Character>();

        public ConsoleUI_CharacterList(string baseURL, TokenResponse token, ListState state)
        {
            _baseURL = baseURL;
            _token = token;
            _controller = new LocalCharacterController(baseURL, token);

            // Get characters
            _characters = _controller.GetAllCharacters();

            _state = state;
        }

        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle($"List of characters:");

                PrintAllCharacters();

                if(_state == ListState.ViewOnly)
                {
                    Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a Character Id to view character information " +
                    "or press enter to return to the previous menu:\n");
                }
                else
                {
                    Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a Character Id to delete that character " +
                    "or press enter to return to the previous menu:\n");
                }
                string response = Console.ReadLine();

                // Return to previous screen if no response input
                if (response is null || response == "")
                    return;

                // Otherwise, try to view that character
                try
                {
                    int id = int.Parse(response);

                    if (_state == ListState.ViewOnly)
                    {
                        // Edit that character
                        Character specificCharacter = _characters.FirstOrDefault(c => c.CharacterId == id);

                        if (specificCharacter is null)
                        {
                            PrintErrorMessageForInput(response);
                        }
                        else
                        {
                            // Open character edit screen
                            ConsoleUI_CharacterInfo nextConsole_CharacterInfo = new ConsoleUI_CharacterInfo(_baseURL, _token, specificCharacter, specificCharacter.Player, false);
                            GoToNextPage("Character info");
                            nextConsole_CharacterInfo.Run_MainMenu();
                            _characters = _controller.GetAllCharacters();
                            GoBack();
                        }
                    }
                    else
                    {
                        // Delete that character
                        if (_controller.DeleteCharacter(id))
                        {
                            _characters = _controller.GetAllCharacters();
                            Console.WriteLine("Your character was successfully deleted. Press any key to continue.");
                            Console.ReadLine();
                        }else
                        {
                            PrintErrorMessage("Character could not be deleted. Press any key to continue.");
                        }
                    }
                }
                catch
                {
                    PrintErrorMessageForInput(response);
                }
            }
        }

        public void PrintAllCharacters()
        {
            if (_characters is null || _characters.Count == 0)
            {
                Console.WriteLine("There are no characters at this time. Please use Postman to enter new characters.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-25}{2,-15}{3,-30}\n",
                        "Id",
                        "Name",
                        "Race",
                        "Class");

                foreach (Character character in _characters)
                {
                    Console.WriteLine("{0,-10}{1,-50}{2,-15}{3,-30}",
                        character.CharacterId,
                        character.Name,
                        character.Race,
                        character.CharacterClass);
                }
            }
        }
    }
}
