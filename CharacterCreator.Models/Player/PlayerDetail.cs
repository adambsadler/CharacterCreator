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
        // Note: the User doesn't need to see their unique Guid since we'll only be showing this PlayerDetail
        // for Players.PlayerId == UserId

        public string Name { get; set; }
        public int NumberOfCharacters { get; set; }
    }
}
