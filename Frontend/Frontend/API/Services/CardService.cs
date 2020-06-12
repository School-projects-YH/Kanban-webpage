using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.API.DTO;
using Frontend.API;
using System.Net.Http;

namespace Frontend.API.Services
{

    public class CardService : IService<CardDTO>
    {
        HttpClient _httpClient;
        string baseUrl = "https://localhost:9001/";
        string uri = "api/card/";
        string url { get { return baseUrl + uri; } }
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
        public async Task Create(CardDTO cardDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url);
            }
        }

        // Deleate
        // Update
        // Find by id
    }
}

