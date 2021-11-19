using CharacterCreator.Models.BackgroundModels;
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
    public class BackgroundController : ApiController
    {
        private BackgroundService CreateBackgroundService()
        {
            var playerId = Guid.Parse(User.Identity.GetUserId());
            var backgroundService = new BackgroundService();
            return backgroundService;
        }

        [HttpPost]
        public IHttpActionResult Post(BackgroundCreate background)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateBackgroundService();

            if (!service.CreateBackground(background))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            BackgroundService backgroundService = CreateBackgroundService();
            var backgrounds = backgroundService.GetBackgrounds();

            if (backgrounds is null)
                return InternalServerError();

            return Ok(backgrounds);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            BackgroundService backgroundService = CreateBackgroundService();
            var background = backgroundService.GetBackgroundById(id);

            if (background is null)
                return NotFound();

            return Ok(background);
        }

        [HttpPut]
        public IHttpActionResult Put(BackgroundEdit background)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            BackgroundService backgroundService = CreateBackgroundService();

            if (!backgroundService.UpdateBackground(background))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateBackgroundService();

            if (!service.DeleteBackground(id))
                return InternalServerError();

            return Ok();
        }
    }
}
