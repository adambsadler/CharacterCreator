using CharacterCreator.ConsoleProgram.ModelClasses;
using CharacterCreator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CharacterCreator.ConsoleProgram.ControllerClasses
{
    public class LocalCharacterController : ApiController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _baseURL;

        public LocalCharacterController(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
        }


        public bool CreateNewCharacter(Character newCharacter, string skillProficiencyIds)
        {
            string url = _baseURL + "/api/Character";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "PlayerId", newCharacter.PlayerId.ToString() ),///////////////
                            new KeyValuePair<string, string>( "Name", newCharacter.Name ),
                            new KeyValuePair<string, string>( "Strength", newCharacter.Strength.ToString() ),
                            new KeyValuePair<string, string>( "Dexterity", newCharacter.Dexterity.ToString() ),
                            new KeyValuePair<string, string>( "Constitution", newCharacter.Constitution.ToString() ),
                            new KeyValuePair<string, string>( "Intelligence", newCharacter.Intelligence.ToString() ),
                            new KeyValuePair<string, string>( "Wisdom", newCharacter.Wisdom.ToString() ),
                            new KeyValuePair<string, string>( "Charisma", newCharacter.Charisma.ToString() ),
                            new KeyValuePair<string, string>( "Race", newCharacter.Race ),
                            new KeyValuePair<string, string>( "CharacterClass", newCharacter.CharacterClass ),
                            new KeyValuePair<string, string>( "BackgroundId", newCharacter.BackgroundId.ToString() ),///////////////
                            new KeyValuePair<string, string>( "SkillProficiencyIds", skillProficiencyIds )///////////////
                        };
            var content = new FormUrlEncodedContent(pairs);

            var response = _httpClient.PostAsync(url, content).Result;

            return response.IsSuccessStatusCode;
        }
        public List<Character> GetAllCharacters()
        {
            string url = _baseURL + "/api/Character";

            var response = _httpClient.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<List<Character>>().Result;
        }

        public Character GetCharacterById(int id)
        {
            string url = _baseURL + "/api/Character/" + id.ToString();

            var response = _httpClient.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<Character>().Result;
        }

        public bool UpdateCharacter(int id, Character newCharacter, string skillProficiencyIds)
        {
            string url = _baseURL + "/api/Character";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "CharacterId", newCharacter.CharacterId.ToString() ),
                            new KeyValuePair<string, string>( "PlayerId", newCharacter.PlayerId.ToString() ),///////////////
                            new KeyValuePair<string, string>( "Name", newCharacter.Name ),
                            new KeyValuePair<string, string>( "Strength", newCharacter.Strength.ToString() ),
                            new KeyValuePair<string, string>( "Dexterity", newCharacter.Dexterity.ToString() ),
                            new KeyValuePair<string, string>( "Constitution", newCharacter.Constitution.ToString() ),
                            new KeyValuePair<string, string>( "Intelligence", newCharacter.Intelligence.ToString() ),
                            new KeyValuePair<string, string>( "Wisdom", newCharacter.Wisdom.ToString() ),
                            new KeyValuePair<string, string>( "Charisma", newCharacter.Charisma.ToString() ),
                            new KeyValuePair<string, string>( "Race", newCharacter.Race ),
                            new KeyValuePair<string, string>( "CharacterClass", newCharacter.CharacterClass ),
                            new KeyValuePair<string, string>( "BackgroundId", newCharacter.BackgroundId.ToString() ),///////////////
                            new KeyValuePair<string, string>( "SkillProficiencyIds", skillProficiencyIds )///////////////
                        };
            var content = new FormUrlEncodedContent(pairs);

            var response = _httpClient.PutAsync(url, content).Result;

            return response.IsSuccessStatusCode;
        }

        public bool DeleteCharacter(int id)
        {
            string url = _baseURL + "/api/Character/" + id.ToString();

            var response = _httpClient.DeleteAsync(url).Result;

            return response.IsSuccessStatusCode;
        }
    }
}
