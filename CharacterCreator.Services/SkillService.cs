using CharacterCreator.Data;
using CharacterCreator.Models.SkillModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class SkillService
    {

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

        public IEnumerable<SkillListItem> GetSkills()
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var query =
                    ctx
                        .Skills
                        .Select(
                            e =>
                                new SkillListItem
                                {
                                    SkillId = e.SkillId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    AbilityType = e.AbilityType
                                });

                    return query.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        public SkillDetail GetSkillById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Skills
                        .Single(e => e.SkillId == id);

                    return new SkillDetail
                    {
                        SkillId = entity.SkillId,
                        Name = entity.Name,
                        Description = entity.Description,
                        AbilityType = entity.AbilityType
                    };
                }
                catch
                {
                    return null;
                }                
            }
        }

        public bool UpdateSkill(SkillEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Skills
                        .Single(e => e.SkillId == model.SkillId);

                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.AbilityType = model.AbilityType;

                    return ctx.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }                
            }
        }

        public bool DeleteSkill(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Skills
                        .Single(e => e.SkillId == id);

                    ctx.Skills.Remove(entity);

                    return ctx.SaveChanges() > 0;
                }catch
                {
                    return false;
                }                
            }
        }
    }
}
