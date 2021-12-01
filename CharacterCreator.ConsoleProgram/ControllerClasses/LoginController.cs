using CharacterCreator.ConsoleProgram.ModelClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CharacterCreator.ConsoleProgram.ControllerClasses
{
    public class LoginController : ApiController
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public TokenResponse GetToken(string baseURL, string email, string password)
        {
            string url = baseURL + "/token";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "userName", email ),
                            new KeyValuePair<string, string> ( "password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;
                TokenResponse token = response.Content.ReadAsAsync<TokenResponse>().Result;

                return token;
            }
        }

        public bool Register(string baseURL, string email, string password, string confirmPassword)
        {
            string url = baseURL + "/api/account/register";

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "Email", email ),
                            new KeyValuePair<string, string>( "Password", password ),
                            new KeyValuePair<string, string> ( "ConfirmPassword", confirmPassword )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url, content).Result;

                return response.IsSuccessStatusCode;
            }
        }
    }
}
