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
    class LocalSkillController : ApiController
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string _baseURL;

        public LocalSkillController(string baseURL, TokenResponse token)
        {
            _baseURL = baseURL;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        public List<Skill> GetSkills()
        {
            string url = _baseURL + "/api/Skill";

            var response = _httpClient.GetAsync(url).Result;

            if (!response.IsSuccessStatusCode)
                return null;

            return response.Content.ReadAsAsync<List<Skill>>().Result;
        }
    }
}
