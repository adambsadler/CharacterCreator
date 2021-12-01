using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.PlayerModels
{
    public class PlayerCreate
    {
        [Required]
        [MinLength(2, ErrorMessage = "Error: skill name must be at least 2 characters long.")]
        [MaxLength(120, ErrorMessage = "Error: skill name must be 120 characters or less.")]
        public string Name { get; set; }

        public Guid UserId { get; set; }
    }
}
