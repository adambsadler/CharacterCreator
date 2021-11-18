using CharacterCreator.Models.CharacterModels;
using CharacterCreator.Models.PlayerModels;
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
    public class PlayerController : ApiController
    {
        private PlayerService CreatePlayerService()
        {
            var playerId = Guid.Parse(User.Identity.GetUserId());
            var service = new PlayerService(playerId);
            return service;
        }

        [HttpPost]
        public IHttpActionResult Post(PlayerCreate player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.CreatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = CreatePlayerService();
            var players = service.GetPlayers();

            if (players is null)
                return InternalServerError();

            return Ok(players);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var service = CreatePlayerService();
            var player = service.GetPlayerForId(id);

            if (player is null)
                return NotFound();

            return Ok(player);
        }

        [HttpPut]
        public IHttpActionResult Put(PlayerEdit player)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreatePlayerService();

            if (!service.UpdatePlayer(player))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreatePlayerService();

            if (!service.DeletePlayer(id))
                return InternalServerError();

            return Ok();
        }
    }
}
