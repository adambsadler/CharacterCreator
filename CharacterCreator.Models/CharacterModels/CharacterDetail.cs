using CharacterCreator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.CharacterModels
{
    public class CharacterDetail
    {
        public int CharacterId { get; set; }
        
        public string Name { get; set; }
        
        public int Strength { get; set; }
        
        public int Dexterity { get; set; }
        
        public int Constitution { get; set; }
        
        public int Intelligence { get; set; }
        
        public int Wisdom { get; set; }
        
        public int Charisma { get; set; }
        
        public string Race { get; set; }
        
        public string CharacterClass { get; set; }

        public virtual Background Background { get; set; }

        public virtual List<Skill> SkillProficiencies { get; set; }
    }
}
