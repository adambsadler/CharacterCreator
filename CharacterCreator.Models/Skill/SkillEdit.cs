using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.Skill
{
    public class SkillEdit
    {
        public int SkillId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AbilityType { get; set; }
    }
}
