using Frontend.API.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.API.Services
{
    public class CardService : IService<CardDTO>
    {
        private HttpClient _httpClient;
        //private string baseUrl = "http://localhost:9000/";
        private string baseUrl = "https://localhost:9001/";
        private string uri = "api/card/";
        private string url
        { get { return baseUrl + uri; } }

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CardDTO>> GetAll()
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var cards = await response.Content.ReadAsAsync<IEnumerable<CardDTO>>();
                    return cards;
                }
                else
                {
                    throw new Exception("Hahah du krashade!");
                }
            }
        }

        public async Task<IEnumerable<CardDTO>> GetByBoardIdAsync(int boardId)
        {
            using (_httpClient)
            {
                uri = "api/dto/";

                HttpResponseMessage response = await _httpClient.GetAsync(url + boardId);
                uri = "api/card/";

                if (response.IsSuccessStatusCode)
                {
                    var cards = await response.Content.ReadAsAsync<IEnumerable<CardDTO>>();
                    return cards;
                }
                return null;
            }
        }

        // Create
        public async Task<int> Create(CardDTO cardDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.PostAsJsonAsync(url, cardDTO);
                if(response.IsSuccessStatusCode)
                {

                    var uri = response.Headers.Location.ToString();
                    string stringId = uri.Substring(uri.LastIndexOf('/') + 1);
                    var id = Convert.ToInt32(stringId);
                    return id;
                }
                throw new Exception("Create Card not succesfull");
            }
        }

        // Delete

        public async Task Delete(CardDTO cardDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.DeleteAsync(url + cardDTO.Id);
            }
        }

        // Update
        public async Task<CardDTO> Update(CardDTO cardDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.PutAsJsonAsync(url + cardDTO.Id, cardDTO);
                var card = await response.Content.ReadAsAsync<CardDTO>();
                return card;
            }
        }

        // Find by id

        public async Task<CardDTO> FindById(int id)
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url + id);
                var card = await response.Content.ReadAsAsync<CardDTO>();
                return card;
            }
        }
    }
}