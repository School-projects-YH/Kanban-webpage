using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Frontend.DTO;
using System.Collections.Specialized;

namespace Frontend.API
{

    public interface IApiHandler
    {
    }


    public class ApiHandler : IApiHandler
    {
        HttpClient _client;

        public ApiHandler(HttpClient client)
        {
            _client = client;
        }

        public async Task<BoardDTO[]> GetBoardsAsync(string url = "https://localhost:9001/api/board")
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var board = await response.Content.ReadAsAsync<BoardDTO[]>();
                return board;
            }
            return null;
        }

        public async Task<CardDTO[]> GetCardsAsync(string url = "https://localhost:9001/api/dto")
        {
            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var card = await response.Content.ReadAsAsync<CardDTO[]>();
                return card;
            }
            return null;
        }
        public async Task<CardDTO[]> GetCardsByBoardIdAsync(int id)
        {
            string url = "https://localhost:9001/api/dto/" + id;
            //string url ="http://localhost:9001/api/dto/1";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var card = await response.Content.ReadAsAsync<CardDTO[]>();
                return card;
            }
            return null;
        }
       
        public async Task MoveLeftAsync(int id)
        {
            string url = "https://localhost:9001/api/cardmovement/left";
            
            await _client.PutAsJsonAsync(url, id);
        }

        public async Task MoveRightAsync(int id)
        {
            string url = "https://localhost:9001/api/cardmovement/right";
            
            await _client.PutAsJsonAsync(url, id);
        }

        public async Task<BoardDTO> CreateBoard(string title)
        {
            string url = "http://localhost:9000/api/board/";
        
            var board = new BoardDTO()
            {
                Title = title
            };
            
            var response = await _client.PostAsJsonAsync(url, board);

            if (response.IsSuccessStatusCode)
            {
                var uri = response.Headers.Location.ToString();
                string id = uri.Substring(uri.LastIndexOf('/') + 1);
                board.Id = Convert.ToInt32(id);

                return board;
            }
            else
            {
                return null; 
            }
            
        }


    }
}
