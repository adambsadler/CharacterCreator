using CharacterCreator.Data;
using CharacterCreator.Models.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class SkillService
    {
        private readonly Guid _playerId;

        public SkillService(Guid playerId)
        {
            _playerId = playerId;
        }

        public bool CreateSkill(SkillCreate model)
        {
            var entity =
                new Skill()
                {
                    Name = model.Name,
                    Description = model.Description,
                    AbilityType = model.AbilityType
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Skills.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }
    }
}
