using CharacterCreator.Models.SkillModels;
using CharacterCreator.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CharacterCreator.WebAPI.Controllers
{
    public class SkillController : ApiController
    {
        private SkillService CreateSkillService()
        {
            var playerId = Guid.Parse(User.Identity.GetUserId());
            var skillService = new SkillService();
            return skillService;
        }

        [HttpPost]
        public IHttpActionResult Post(SkillCreate skill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSkillService();

            if (!service.CreateSkill(skill))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            SkillService skillService = CreateSkillService();
            var skills = skillService.GetSkills();

            if (skills is null)
                return InternalServerError();

            return Ok(skills);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            SkillService skillService = CreateSkillService();
            var skill = skillService.GetSkillById(id);

            if (skill is null)
                return NotFound();

            return Ok(skill);
        }

        [HttpPut]
        public IHttpActionResult Put(SkillEdit skill)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SkillService skillService = CreateSkillService();

            if (!skillService.UpdateSkill(skill))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSkillService();

            if (!service.DeleteSkill(id))
                return InternalServerError();

            return Ok();
        }
    }
}
