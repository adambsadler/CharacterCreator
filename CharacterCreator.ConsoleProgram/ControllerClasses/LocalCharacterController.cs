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


        public bool CreateNewCharacter(Character character)
        {
            string url = _baseURL + "/api/Character";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "Name", character.Name ),
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

        public bool UpdateCharacter(int id, Character newCharacter)
        {
            string url = _baseURL + "/api/Character";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "Name", newCharacter.Name ),
                            new KeyValuePair<string, string>( "CharacterId", id.ToString() ),
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
