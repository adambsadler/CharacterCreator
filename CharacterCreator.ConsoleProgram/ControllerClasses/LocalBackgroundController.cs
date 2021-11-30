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
    public class LocalBackgroundController : ApiController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _baseURL;

        public LocalBackgroundController(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        public List<Background> GetBackgrounds()
        {
            string url = _baseURL + "/api/Background";

            var response = _httpClient.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<List<Background>>().Result;
        }
    }
}