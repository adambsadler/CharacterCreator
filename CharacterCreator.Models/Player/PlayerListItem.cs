using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Models.Character
{
    public class PlayerListItem
    {
        // We don't want to show the Guid here since anyone can see this endpoint. Just show Names and number of characters for testing.
        public string Name { get; set; }
        public int NumberOfCharacters { get; set; }
    }
}
