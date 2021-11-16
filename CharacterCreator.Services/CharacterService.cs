using CharacterCreator.Data;
using CharacterCreator.Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class CharacterService
    {
        private readonly Guid _playerId;

        public CharacterService(Guid playerId)
        {
            _playerId = playerId;
        }

        public bool CreateCharacter(CharacterCreate model)
        {
            var entity =
                new Character()
                {
                    Name = model.Name,
                    Strength = model.Strength,
                    Dexterity = model.Dexterity,
                    Constitution = model.Constitution,
                    Intelligence = model.Intelligence,
                    Wisdom = model.Wisdom,
                    Charisma = model.Charisma,
                    Race = model.Race,
                    CharacterClass = model.CharacterClass
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<CharacterListItem> GetCharacters()
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var query =
                    ctx
                        .Characters
                        .Select(
                            e =>
                                new CharacterListItem
                                {
                                    CharacterId = e.CharacterId,
                                    Name = e.Name,
                                    Strength = e.Strength,
                                    Dexterity = e.Dexterity,
                                    Constitution = e.Constitution,
                                    Intelligence = e.Intelligence,
                                    Wisdom = e.Wisdom,
                                    Charisma = e.Charisma,
                                    Race = e.Race,
                                    CharacterClass = e.CharacterClass
                                });

                    return query.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        public CharacterDetail GetCharacterById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterId == id);

                    return new CharacterDetail
                    {
                        CharacterId = entity.CharacterId,
                        Name = entity.Name,
                        Strength = entity.Strength,
                        Dexterity = entity.Dexterity,
                        Constitution = entity.Constitution,
                        Intelligence = entity.Intelligence,
                        Wisdom = entity.Wisdom,
                        Charisma = entity.Charisma,
                        Race = entity.Race,
                        CharacterClass = entity.CharacterClass
                    };
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool UpdateCharacter(CharacterEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterId == model.CharacterId);

                    entity.Name = model.Name;
                    entity.Strength = model.Strength;
                    entity.Dexterity = model.Dexterity;
                    entity.Constitution = model.Constitution;
                    entity.Intelligence = model.Intelligence;
                    entity.Wisdom = model.Wisdom;
                    entity.Charisma = model.Charisma;
                    entity.Race = model.Race;
                    entity.CharacterClass = model.CharacterClass;

                    return ctx.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteCharacter(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharacterId == id);

                    ctx.Characters.Remove(entity);

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
