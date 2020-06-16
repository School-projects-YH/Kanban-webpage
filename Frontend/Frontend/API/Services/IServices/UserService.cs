using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.API.Model;

namespace Frontend.API.Services
{
    public class UserService : IService<UserLoginDTO>
    {

        private HttpClient _httpClient;
        private string baseUrl { get; }

        private string uri = "api/User/";
        private string url
        { get { return baseUrl + uri; } }

        public UserService(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<IEnumerable<UserLoginDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
        public async Task<int> UserLoginRequestAsync(UserLoginDTO user)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(url, user);
                if (response.IsSuccessStatusCode)
                {
                    var userId = await response.Content.ReadAsAsync<int>();
                    return userId;
                }
            }
            return 0;
        }

        // Create
        public async Task<int> Create(UserLoginDTO userLogin)
        {
            using (var _httpClient = new HttpClient())
            {
                uri = "api/User/newUser";

                var response = await _httpClient.PostAsJsonAsync(url, userLogin);
                uri = "api/User/";
                if (response.IsSuccessStatusCode)
                {
                    var uri = response.Headers.Location.ToString();
                    string stringId = uri.Substring(uri.LastIndexOf('/') + 1);
                    var id = Convert.ToInt32(stringId);
                    return id;
                }
                throw new Exception("Create userLogin not succesfull");
            }
        }


        // Delete
        public async Task Delete(UserLoginDTO userLogin)
        {
            using (_httpClient)
            {
                var response = await _httpClient.DeleteAsync(url + userLogin.Id);
            }
        }

        // Update
        public async Task<UserLoginDTO> Update(UserLoginDTO userLogin)
        {
            using (_httpClient)
            {
                var response = await _httpClient.PutAsJsonAsync(url + userLogin.Id, userLogin);
                var card = await response.Content.ReadAsAsync<UserLoginDTO>();
                return card;
            }
        }

        // Find by id

        public async Task<UserLoginDTO> FindById(int id)
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url + id);
                var card = await response.Content.ReadAsAsync<UserLoginDTO>();
                return card;
            }
        }
    }
}

