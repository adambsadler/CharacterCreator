using CharacterCreator.Data;
using CharacterCreator.Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class PlayerService
    {
        private readonly Guid _playerId;

        public PlayerService(Guid playerId)
        {
            _playerId = playerId;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var entity =
                new Player(_playerId)
                {
                    Name = model.Name
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Players.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }
    }
}
