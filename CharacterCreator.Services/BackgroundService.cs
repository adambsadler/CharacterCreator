using CharacterCreator.Data;
using CharacterCreator.Models.BackgroundModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterCreator.Services
{
    public class BackgroundService
    {
        public bool CreateBackground(BackgroundCreate model)
        {
            var entity =
                new Background()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Feature = model.Feature
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Backgrounds.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<BackgroundListItem> GetBackgrounds()
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var query =
                    ctx
                        .Backgrounds
                        .Select(
                            e =>
                                new BackgroundListItem
                                {
                                    BackgroundId = e.BackgroundId,
                                    Name = e.Name,
                                    Description = e.Description,
                                    Feature = e.Feature
                                });

                    return query.ToArray();
                }
                catch
                {
                    return null;
                }
            }
        }

        public BackgroundDetail GetBackgroundById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Backgrounds
                        .Single(e => e.BackgroundId == id);

                    return new BackgroundDetail
                    {
                        BackgroundId = entity.BackgroundId,
                        Name = entity.Name,
                        Description = entity.Description,
                        Feature = entity.Feature
                    };
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool UpdateBackground(BackgroundEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Backgrounds
                        .Single(e => e.BackgroundId == model.BackgroundId);

                    entity.Name = model.Name;
                    entity.Description = model.Description;
                    entity.Feature = model.Feature;

                    return ctx.SaveChanges() > 0;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteBackground(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity =
                    ctx
                        .Backgrounds
                        .Single(e => e.BackgroundId == id);

                    ctx.Backgrounds.Remove(entity);

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
