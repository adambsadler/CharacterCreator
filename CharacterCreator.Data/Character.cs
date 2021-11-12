using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Data
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(8,15, ErrorMessage ="Please enter a number between 8 and 15.")]
        public int Strength { get; set; }
        [Required]
        [Range(8, 15, ErrorMessage = "Please enter a number between 8 and 15.")]
        public int Dexterity { get; set; }
        [Required]
        [Range(8, 15, ErrorMessage = "Please enter a number between 8 and 15.")]
        public int Constitution { get; set; }
        [Required]
        [Range(8, 15, ErrorMessage = "Please enter a number between 8 and 15.")]
        public int Intelligence { get; set; }
        [Required]
        [Range(8, 15, ErrorMessage = "Please enter a number between 8 and 15.")]
        public int Wisdom { get; set; }
        [Required]
        [Range(8, 15, ErrorMessage = "Please enter a number between 8 and 15.")]
        public int Charisma { get; set; }
        [Required]
        public string Race { get; set; }
        [Required]
        public string CharacterClass { get; set; }

        // Add Background

        // Add Skill Proficiencies
    }
}
