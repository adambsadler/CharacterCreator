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
    public class ConsoleUI_SkillList : FormattingHelpers
    {
        private string _baseURL;
        private TokenResponse _token;
        private readonly LocalSkillController _controller;
        private List<Skill> _skills = new List<Skill>();

        public ConsoleUI_SkillList(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _token = token;
            _controller = new LocalSkillController(baseURL, token);

            // Get skills
            _skills = _controller.GetSkills();
        }

        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle($"List of skills:");

                PrintAllSkills();

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a Skill Id to view skill information information " +
                    "or press enter to return to the previous menu:\n");
                string response = Console.ReadLine();

                // Return to previous screen if no response input
                if (response is null || response == "")
                    return;

                // Otherwise, try to view that skill
                try
                {
                    int id = int.Parse(response);
                    Skill specificSkill = _skills.FirstOrDefault(s => s.SkillId == id);

                    if(specificSkill is null)
                    {
                        PrintErrorMessageForInput(response);
                    }else
                    {
                        Console.WriteLine("\n" + CONST_DASHES + $"\n");
                        Console.WriteLine("{0,-15}{1,-50}", "Skill Id:", specificSkill.SkillId);
                        Console.WriteLine("{0,-15}{1,-50}", "Ability:", specificSkill.AbilityType);
                        Console.WriteLine("{0,-15}{1, -600}\n", "Description:", specificSkill.Description);
                        Console.ReadLine();
                    }
                }
                catch
                {
                    PrintErrorMessageForInput(response);
                }
            }
        }

        public void PrintAllSkills()
        {
            if (_skills is null || _skills.Count == 0)
            {
                Console.WriteLine("There are no skills at this time. Please use Postman to enter new skills.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-50}\n",
                        "Id",
                        "Name");

                foreach (Skill skill in _skills)
                {
                    Console.WriteLine("{0,-10}{1,-50}",
                        skill.SkillId,
                        $"{skill.Name} ({skill.AbilityType})");
                }
            }
        }
    }
}
