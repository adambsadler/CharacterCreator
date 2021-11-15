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

        public IEnumerable<PlayerListItem> GetPlayers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var query =
                    ctx
                        .Players
                        .Select(
                            e =>
                                new PlayerListItem
                                {
                                    Name = e.Name,
                                    NumberOfCharacters = e.Characters.Count
                                });

                    return query.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        public PlayerDetail GetPlayerForCurrentUser()
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == _playerId);

                    return new PlayerDetail
                    {
                        Name = entity.Name,
                        NumberOfCharacters = entity.Characters.Count
                    };
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
