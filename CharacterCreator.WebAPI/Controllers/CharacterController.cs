using CharacterCreator.Models.CharacterModels;
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
    public class CharacterController : ApiController
    {
        private CharacterService CreateCharacterService()
        {
            var characterService = new CharacterService();
            return characterService;
        }

        [HttpPost]
        public IHttpActionResult Post(CharacterCreate character)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCharacterService();

            if (!service.CreateCharacter(character))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            CharacterService characterService = CreateCharacterService();
            var characters = characterService.GetCharacters();

            if (characters is null)
                return InternalServerError();

            return Ok(characters);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            CharacterService characterService = CreateCharacterService();
            var character = characterService.GetCharacterById(id);

            if (character is null)
                return NotFound();

            return Ok(character);
        }

        [HttpPut]
        public IHttpActionResult Put(CharacterEdit character)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CharacterService characterService = CreateCharacterService();

            if (!characterService.UpdateCharacter(character))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCharacterService();

            if (!service.DeleteCharacter(id))
                return InternalServerError();

            return Ok();
        }
    }
}
