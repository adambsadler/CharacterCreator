using CharacterCreator.Data;
using CharacterCreator.Models.Character;
using CharacterCreator.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class PlayerService
    {
        private readonly Guid _userId;

        public PlayerService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePlayer(PlayerCreate model)
        {
            var entity =
                new Player()
                {
                    Name = model.Name,
                    UserId = _userId
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
                                    PlayerId = e.PlayerId,
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

        public PlayerDetail GetPlayerForId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == id);

                    return new PlayerDetail
                    {
                        PlayerId = entity.PlayerId,
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

        public bool UpdatePlayer(PlayerEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == model.PlayerId);

                    entity.Name = model.Name;

                    return ctx.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeletePlayer(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Players
                        .Single(e => e.PlayerId == id);

                    ctx.Players.Remove(entity);

                    return ctx.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
