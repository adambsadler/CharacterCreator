using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Data
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
        
        [Required] 
        [MinLength(5, ErrorMessage = "Error: skill name must be at least 5 characters long.")]
        [MaxLength(240, ErrorMessage ="Error: skill name must be 240 characters or less.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        public string AbilityType { get; set; }

        public Skill() { }

        public Skill(string name, string description, string abilityType)
        {
            Name = name;
            Description = description;
            AbilityType = abilityType;
        }
    }
}
