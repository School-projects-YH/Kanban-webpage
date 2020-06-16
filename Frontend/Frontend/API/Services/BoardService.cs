using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.API.Model;

namespace Frontend.API.Services
{
    public class BoardService : IService<BoardDTO>
    {
        private HttpClient _httpClient;
        private string baseUrl = "http://localhost:9000/";
        private string uri = "api/board/";
        private string url
        { get { return baseUrl + uri; } }

        public BoardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BoardDTO>> GetAll()
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var cards = await response.Content.ReadAsAsync<IEnumerable<BoardDTO>>();
                    return cards;
                }
                else
                {
                    throw new Exception("Hahah du krashade!");
                }
            }
        }
        public async Task<IEnumerable<BoardDTO>> GetByUserIdAsync(int userId)
        {
            using (_httpClient)
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url + userId);

                if (response.IsSuccessStatusCode)
                {
                    var cards = await response.Content.ReadAsAsync<BoardDTO[]>();
                    return cards;
                }
                return null;
            }
        }

        public async Task<IEnumerable<BoardDTO>> GetByBoardIdAsync(int boardId)
        {
            using (_httpClient)
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url + boardId);

                if (response.IsSuccessStatusCode)
                {
                    var cards = await response.Content.ReadAsAsync<BoardDTO[]>();
                    return cards;
                }
                return null;
            }
        }

        // Create
        public async Task<int> Create(BoardDTO boardDTO)
        {
            using (var _httpClient = new HttpClient())
            {
                var response = await _httpClient.PostAsJsonAsync(url, boardDTO);
                if (response.IsSuccessStatusCode)
                {
                    var uri = response.Headers.Location.ToString();
                    string stringId = uri.Substring(uri.LastIndexOf('=') + 1);
                    var id = Convert.ToInt32(stringId);
                    return id;
                }
                throw new Exception("Create Card not succesfull");
            }
        }
      

        // Delete

        public async Task Delete(BoardDTO boardDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.DeleteAsync(url + boardDTO.Id);
            }
        }

        // Update
        public async Task<BoardDTO> Update(BoardDTO boardDTO)
        {
            using (_httpClient)
            {
                var response = await _httpClient.PutAsJsonAsync(url + boardDTO.Id, boardDTO);
                var card = await response.Content.ReadAsAsync<BoardDTO>();
                return card;
            }
        }

        // Find by id

        public async Task<BoardDTO> FindById(int id)
        {
            using (_httpClient)
            {
                var response = await _httpClient.GetAsync(url + id);
                var card = await response.Content.ReadAsAsync<BoardDTO>();
                return card;
            }
        }
    }
}
