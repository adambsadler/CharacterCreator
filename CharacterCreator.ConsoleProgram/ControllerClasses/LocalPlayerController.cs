using CharacterCreator.ConsoleProgram.ModelClasses;
using CharacterCreator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CharacterCreator.ConsoleProgram.ControllerClasses
{
    public class LocalPlayerController : ApiController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _baseURL;

        public LocalPlayerController(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        public Player GetFirstPlayerForToken()
        {
            string url = _baseURL + "/api/Player";

            
            var response = _httpClient.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
                return null;

            List<Player> players = response.Content.ReadAsAsync<List<Player>>().Result;

            return players.First<Player>();
            
        }

        public bool UpdatePlayerName(Player p, string newName)
        {
            string url = _baseURL + "/api/Player";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "Name", newName ),
                            new KeyValuePair<string, string>( "PlayerId", p.PlayerId.ToString() ),
                        };
            var content = new FormUrlEncodedContent(pairs);

            var response = _httpClient.PutAsync(url, content).Result;

            return response.IsSuccessStatusCode;
        }
    }
}
