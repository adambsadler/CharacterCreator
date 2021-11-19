using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [MinLength(2, ErrorMessage = "Error: character's name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Error: character's name must be 50 characters or less.")]
        public string Name { get; set; }
        [Required, ForeignKey(nameof(Player))]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
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
        [Required, ForeignKey(nameof(Background))]
        public int BackgroundId { get; set; }
        public virtual Background Background { get; set; }
        [Required]
        public virtual List<Skill> SkillProficiencies { get; set; }
    }
}
