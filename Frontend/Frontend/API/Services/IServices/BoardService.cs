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
        private string baseUrl { get; }
        private string uri = "api/board/";
        private string url
        { get { return baseUrl + uri; } }

        public BoardService(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        public async Task<IEnumerable<BoardDTO>> GetAll()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

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
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url + userId);

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
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url + boardId);

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
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(url, boardDTO);
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
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync(url + boardDTO.Id);
            }
        }

        // Update
        public async Task<BoardDTO> Update(BoardDTO boardDTO)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(url + boardDTO.Id, boardDTO);
                var card = await response.Content.ReadAsAsync<BoardDTO>();
                return card;
            }
        }

        // Find by id

        public async Task<BoardDTO> FindById(int id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url + id);
                var card = await response.Content.ReadAsAsync<BoardDTO>();
                return card;
            }
        }
    }
}
