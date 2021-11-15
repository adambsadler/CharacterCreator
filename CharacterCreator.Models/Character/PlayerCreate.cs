using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.Character
{
    public class PlayerCreate
    {
        // Note: PlayerId will be taken from UserId login info

        [Required]
        [MinLength(2, ErrorMessage = "Error: skill name must be at least 2 characters long.")]
        [MaxLength(120, ErrorMessage = "Error: skill name must be 120 characters or less.")]
        public string Name { get; set; }
    }
}
