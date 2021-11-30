﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Data
{
    public class Background
    {
        [Key]
        public int BackgroundId { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Error: background name must be at least 2 characters long.")]
        [MaxLength(50, ErrorMessage = "Error: background name must be 50 characters or less.")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Error: background description must be at least 2 characters long.")]
        [MaxLength(3000, ErrorMessage = "Error: background description must be 500 characters or less.")]
        public string Description { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Error: background feature must be at least 2 characters long.")]
        [MaxLength(3000, ErrorMessage = "Error: background feature must be 100 characters or less.")]
        public string Feature { get; set; }

        public Background() { }

        public Background(string name, string description, string feature)
        {
            Name = name;
            Description = description;
            Feature = feature;
        }
    }
}
