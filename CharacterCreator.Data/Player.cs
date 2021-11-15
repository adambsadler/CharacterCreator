using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Data
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Error: skill name must be at least 2 characters long.")]
        [MaxLength(120, ErrorMessage = "Error: skill name must be 120 characters or less.")] 
        public string Name { get; set; }

        public virtual List<Character> Characters { get; set; } = new List<Character>();

        [Required]
        public Guid UserId { get; set; }
    }
}
