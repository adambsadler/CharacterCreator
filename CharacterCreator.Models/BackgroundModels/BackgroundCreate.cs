using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.BackgroundModels
{
    public class BackgroundCreate
    {
        [Required]
        [Range(2, 50, ErrorMessage = "Background name must be between 2 and 50 characters long.")]
        public string Name { get; set; }
        [Required]
        [Range(2, 500, ErrorMessage = "Background description must be between 2 and 500 characters long.")]
        public string Description { get; set; }
        [Required]
        [Range(2, 100, ErrorMessage = "Background feature must be between 2 and 100 characters long.")]
        public string Feature { get; set; }
    }
}
