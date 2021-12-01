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
    public class ConsoleUI_CharacterInfo : FormattingHelpers
    {        
        private string _baseURL;
        private TokenResponse _token;
        private readonly LocalCharacterController _controller;
        private Character _character;
        private Player _player;

        private List<string> _races = new List<string>();
        private List<string> _classes = new List<string>();
        private List<Background> _backgrounds = new List<Background>();
        private List<Skill> _skills = new List<Skill>();

        private bool _isNewCharacter = false;

        public ConsoleUI_CharacterInfo(string baseURL, TokenResponse token, Character character, Player player, bool isNewCharacter)
        {
            _baseURL = baseURL;
            _token = token;
            _controller = new LocalCharacterController(baseURL, token);
            _isNewCharacter = isNewCharacter;

            if (player != null)
            {
                _player = player;
            }
            else if(_character != null && _character.Player != null)
            {
                _player = _character.Player;
            }else
            {
                LocalPlayerController playerController = new LocalPlayerController(baseURL, token);
                _player = playerController.GetFirstPlayerForToken();
            }

            // Clone the character, in case we don't save
            _character = SetupNewCharacter(character);

            _races.Add("Dragonborn");
            _races.Add("Dwarf");
            _races.Add("Elf");
            _races.Add("Gnome");
            _races.Add("Half-Elf");
            _races.Add("Halfing");
            _races.Add("Half-Orc");
            _races.Add("Human");
            _races.Add("Tiefling");

            _classes.Add("Barbarian");
            _classes.Add("Bard");
            _classes.Add("Cleric");
            _classes.Add("Druid");
            _classes.Add("Fighter");
            _classes.Add("Monk");
            _classes.Add("Paladin");
            _classes.Add("Ranger");
            _classes.Add("Rogue");
            _classes.Add("Sorcerer");
            _classes.Add("Warlock");
            _classes.Add("Wizard");

        }

        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle($"Character information:");

                PrintCharacter();

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a trait to edit, " +
                    "type 'SAVE' to save changes, " +
                    "or press enter to return to the previous menu:\n");
                string response = Console.ReadLine();

                // Return to previous screen if no response input
                if (response is null || response == "")
                    return;

                // Try to edit that trait
                switch (response.ToLower().Trim())
                {
                    case "save":
                        // Save changes
                        SaveCharacter();
                        break;

                    case "1":
                        // Change character name
                        string name = AskUser_StringInput("Enter character name:");

                        if (name is null || name == "")
                            break;

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Name = name;
                        break;

                    case "2":
                        // Change race
                        PrintAllRaces();
                        int? raceId = AskUser_IntegerInput("Enter race ID:");

                        if (raceId is null || raceId < 0 || raceId >= _races.Count)
                            break;

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Race = _races[(int)raceId];
                        break;

                    case "3":
                        // Change class
                         PrintAllClasses();
                        int? classId = AskUser_IntegerInput("Enter class ID:");

                        if (classId is null || classId < 0 || classId >= _classes.Count) 
                            break;

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.CharacterClass = _classes[(int)classId];
                        break;

                    case "4":
                        // Change background
                        PrintAllBackgrounds();
                        int? backgroundId = AskUser_IntegerInput("Enter background ID:");

                        if (backgroundId is null)
                            break;

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.BackgroundId = (int)backgroundId;
                        _character.Background = GetBackgroundForId((int)backgroundId);
                        break;

                    case "5":
                        // Change skill proficiencies
                        PrintAllSkills();
                        string skillProficiencyIds = AskUser_StringInput("Enter skill proficiency IDs separated by commas:");

                        if (skillProficiencyIds is null || skillProficiencyIds == "")
                            break;

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.SkillProficiencies.Clear();
                        LocalSkillController tempSkillController = new LocalSkillController(_baseURL, _token);
                        foreach(int i in SplitStringIntoIDs(skillProficiencyIds))
                        {
                            foreach(Skill s in _skills)
                            {
                                if (s.SkillId == i)
                                    _character.SkillProficiencies.Add(s);
                            }
                        }                     

                        break;

                    // Change ability scores

                    case "6":
                        int strength = AskUserForAbilityScore("strength");
                        if(strength == 0)
                        {
                            PrintErrorMessage("Ability score is not valid");
                            break;
                        }

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Strength = strength;
                        break;

                    case "7":
                        int dex = AskUserForAbilityScore("dexterity");
                        if (dex == 0)
                        {
                            PrintErrorMessage("Ability score is not valid");
                            break;
                        }

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Dexterity = dex;
                        break;

                    case "8":
                        int con = AskUserForAbilityScore("constitution");
                        if (con == 0)
                        {
                            PrintErrorMessage("Ability score is not valid");
                            break;
                        }

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Constitution = con;
                        break;

                    case "9":
                        int intelligence = AskUserForAbilityScore("intelligence");
                        if (intelligence == 0)
                        {
                            PrintErrorMessage("Ability score is not valid");
                            break;
                        }

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Intelligence = intelligence;
                        break;

                    case "10":
                        int wisdom = AskUserForAbilityScore("wisdom");
                        if (wisdom == 0)
                        {
                            PrintErrorMessage("Ability score is not valid");
                            break;
                        }

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Wisdom = wisdom;
                        break;

                    case "11":
                        int charisma = AskUserForAbilityScore("charisma");
                        if (charisma == 0)
                        {
                            PrintErrorMessage("Ability score is not valid");
                            break;
                        }

                        if (_character is null)
                            if (!CreateCharacter())
                                break;

                        _character.Charisma = charisma;
                        break;

                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }

        public bool CreateCharacter()
        {
            _character = new Character();
            _character.Player = _player;
            _character.PlayerId = _player.PlayerId;
            bool success = _controller.CreateNewCharacter(_character, "");

            if (success)
            {
                Console.WriteLine("New character created. Press any key to continue.");
                //Console.ReadLine();
                return true;
            }
            else
            {
                _character = null;
                Console.WriteLine("New character could not be created. Press any key to continue.");
                Console.ReadLine();
                return false;
            }
        }

        public void SaveCharacter()
        {
            if (_character.Player is null)
                _character.Player = _player;
            _character.PlayerId = _character.Player.PlayerId;

            string skillProficienciesString = GetCommaDelimitedStringForListOfSkills(_character.SkillProficiencies.ToList());

            bool success;
            if(_isNewCharacter)
            {
                // Must create if it's the first time
                success = _controller.CreateNewCharacter(_character, skillProficienciesString);

                if (success)
                {
                    _isNewCharacter = false;
                    Console.WriteLine("Character saved. Press any key to continue.");
                }else
                {
                    Console.WriteLine("Character could not be saved. Press any key to continue.");
                }
                Console.ReadLine();
                return;

            }else
            {
                success = _controller.UpdateCharacter(_character.CharacterId, _character, skillProficienciesString);

                if (success)
                {
                    Console.WriteLine("Character saved. Press any key to continue.");
                }
                else
                {
                    Console.WriteLine("Character could not be saved. Press any key to continue.");
                }

                UpdateCharacter();
            }
        }

        public void UpdateCharacter()
        {
            Character c = _controller.GetCharacterById(_character.CharacterId);

            if (c != null)
            {
                _character = c;
            }
            else
            {
                Console.WriteLine("Character could not be updated. Press any key to continue.");
            }
            Console.ReadLine();
        }

        public void PrintCharacter()
        {
            // Name
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "1", "Name:",
                (_character is null || _character.Name is null) ? "None" : _character.Name);

            // Race
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "2", "Race:",
                (_character is null || _character.Race is null) ? "None" : _character.Race);

            // Class
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "3", "Class:",
                (_character is null || _character.CharacterClass is null) ? "None" : _character.CharacterClass);

            // Background
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "4", "Background:",
                (_character is null || _character.Background is null) ? "None" : _character.Background.Name);
            Console.WriteLine("{0,-5}{1,-20}{2,-100}", "", "",
                (_character is null || _character.Background is null) ? "" : _character.Background.Description);

            // Skill proficiencies
            Console.WriteLine("{0,-5}{1,-20}{2,-50}", "5", "Proficiencies:", "");
            if (_character is null || _character.SkillProficiencies is null || _character.SkillProficiencies.Count == 0)
            {
                Console.WriteLine("{0,-5}{1,-20}{2,-50}", "", "", "None");
            }
            else
            {
                foreach(Skill s in _character.SkillProficiencies)
                {
                    Console.WriteLine("{0,-5}{1,-20}{2,-50}", "", "", s.Name);
                }
            }

            // Ability scores
            Console.WriteLine("\n{0,-5}{1,-20}{2,-40}", "6", "STR", _character.Strength);
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "7", "DEX", _character.Dexterity);
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "8", "CON", _character.Constitution);
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "9", "INT", _character.Intelligence);
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "10", "WIS", _character.Wisdom);
            Console.WriteLine("{0,-5}{1,-20}{2,-40}", "11", "CHA", _character.Charisma);
        }   

        public void PrintAllRaces()
        {
            Console.WriteLine();
            for(int i = 0; i < _races.Count; i++)
            {
                Console.WriteLine("{0,-5}{1,-20}", i, _races[i]);
            }
            Console.WriteLine();

        }

        public void PrintAllClasses()
        {
            Console.WriteLine();
            for (int i = 0; i < _classes.Count; i++)
            {
                Console.WriteLine("{0,-5}{1,-20}", i, _classes[i]);
            }
            Console.WriteLine();

        }

        public void PrintAllBackgrounds()
        {
            LocalBackgroundController _controller = new LocalBackgroundController(_baseURL, _token);
            _backgrounds = _controller.GetBackgrounds();

            Console.WriteLine();

            foreach(Background b in _backgrounds)
            {
                Console.WriteLine("{0,-5}{1,-20}", b.BackgroundId, b.Name);
            }

            Console.WriteLine();
        }

        public Background GetBackgroundForId(int id)
        {
            foreach(Background b in _backgrounds)
            {
                if (b.BackgroundId == id)
                    return b;
            }

            return null;
        }

        public void PrintAllSkills()
        {
            LocalSkillController _controller = new LocalSkillController(_baseURL, _token);
            _skills = _controller.GetSkills();

            Console.WriteLine();

            foreach(Skill s in _skills)
            {
                Console.WriteLine("{0,-5}{1,-20}", s.SkillId, s.Name);
            }

            Console.WriteLine();
        }
        
        public int AskUserForAbilityScore(string ability)
        {
            int? score = AskUser_IntegerInput($"\nEnter {ability} score (8-15):");
            if (score is null || score < 8 || score > 15)
            {
                return 0;
            }

            return (int) score;
        }

        public string GetCommaDelimitedStringForListOfSkills(List<Skill> listOfSkills)
        {
            string output = "";

            if (listOfSkills.Count == 0)
                return output;

            foreach(Skill skill in listOfSkills)
            {
                output += skill.SkillId.ToString().Trim();
                output += ",";
            }

            output.Trim(',');   // remove the trailing comma
            return output;
        }

        public Character SetupNewCharacter(Character oldCharacter)
        {
            Character newCharacter = new Character();

            if (oldCharacter is null)
            {
                newCharacter = new Character
                {
                    Name = "<Insert name>",
                    Race = "<Insert race>",
                    CharacterClass = "<Insert class>",
                    BackgroundId = 1,
                    Background = (new LocalBackgroundController(_baseURL, _token)).GetBackgroundById(1),
                    Strength = 8,
                    Dexterity = 8,
                    Constitution = 8,
                    Intelligence = 8,
                    Wisdom = 8,
                    Charisma = 8
                };
                return newCharacter;
            }

            newCharacter.CharacterId = oldCharacter.CharacterId;

            newCharacter.PlayerId = oldCharacter.PlayerId;
            if (oldCharacter.Player != null)
                newCharacter.Player = oldCharacter.Player;

            
            newCharacter.BackgroundId = oldCharacter.BackgroundId;
            if (newCharacter.BackgroundId == 0)
                newCharacter.BackgroundId = 1;  // Minimum index
            if (oldCharacter.Background != null)
                newCharacter.Background = oldCharacter.Background;

            newCharacter.SkillProficiencies = new List<Skill>();
            if(oldCharacter.SkillProficiencies != null)
            {
                foreach(Skill s in oldCharacter.SkillProficiencies)
                {
                    newCharacter.SkillProficiencies.Add(s);
                }
            }

            if (oldCharacter.Name != null)
                newCharacter.Name = oldCharacter.Name;
            if (newCharacter.Name is null || newCharacter.Name == "")
                newCharacter.Name = "<Insert Name>";

            if (oldCharacter.Race != null)
                newCharacter.Race = oldCharacter.Race;
            if (newCharacter.Race is null || newCharacter.Race == "")
                newCharacter.Race = "<Insert Race>";

            if (oldCharacter.CharacterClass != null)
                newCharacter.CharacterClass = oldCharacter.CharacterClass;
            if (newCharacter.CharacterClass is null || newCharacter.CharacterClass == "")
                newCharacter.CharacterClass = "<Insert Class>";

            newCharacter.Strength = oldCharacter.Strength;
            if (newCharacter.Strength < 8 || newCharacter.Strength > 15)
                newCharacter.Strength = 8;

            newCharacter.Dexterity = oldCharacter.Dexterity;
            if (newCharacter.Dexterity < 8 || newCharacter.Dexterity > 15)
                newCharacter.Dexterity = 8;

            newCharacter.Constitution = oldCharacter.Constitution;
            if (newCharacter.Constitution < 8 || newCharacter.Constitution > 15)
                newCharacter.Constitution = 8;

            newCharacter.Intelligence = oldCharacter.Intelligence;
            if (newCharacter.Intelligence < 8 || newCharacter.Intelligence > 15)
                newCharacter.Intelligence = 8;

            newCharacter.Wisdom = oldCharacter.Wisdom;
            if (newCharacter.Wisdom < 8 || newCharacter.Wisdom > 15)
                newCharacter.Wisdom = 8;

            newCharacter.Charisma = oldCharacter.Charisma;
            if (newCharacter.Charisma < 8 || newCharacter.Charisma > 15)
                newCharacter.Charisma = 8;

            return newCharacter;
        }
    }
}
