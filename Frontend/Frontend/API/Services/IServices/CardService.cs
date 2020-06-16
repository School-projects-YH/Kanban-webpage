using Frontend.API.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.API.Services
{
    public class CardService : IService<CardDTO>
    {
        private string baseUrl { get; }
        private string uri = "api/card/";
        private string url
        { get { return baseUrl + uri; } }

        public CardService(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<IEnumerable<CardDTO>> GetAll()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);

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
            using (var httpClient = new HttpClient())
            {
                uri = "api/dto/";

                HttpResponseMessage response = await httpClient.GetAsync(url + boardId);
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
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(url, cardDTO);
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
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(url + cardDTO.Id);
            }
        }
        public async Task Delete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(url + id);
            }
        }

        // Update
        public async Task<CardDTO> Update(CardDTO cardDTO)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PutAsJsonAsync(url + cardDTO.Id, cardDTO);
                var card = await response.Content.ReadAsAsync<CardDTO>();
                return card;
            }
        }

        // Find by id

        public async Task<CardDTO> FindById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url + id);
                var card = await response.Content.ReadAsAsync<CardDTO>();
                return card;
            }
        }
    }
}