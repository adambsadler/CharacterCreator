using CharacterCreator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.Character
{
    public class PlayerDetail
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public int NumberOfCharacters { get; set; }
    }
}
