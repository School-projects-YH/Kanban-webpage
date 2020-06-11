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
           string url ="https://localhost:9001/api/dto/"+id;
            //string url ="http://localhost:9001/api/dto/1";
          
            HttpResponseMessage response = await _client.GetAsync(url);
                
            if (response.IsSuccessStatusCode)
            {
                var card = await response.Content.ReadAsAsync<CardDTO[]>();
                return card;
            }
            return null;
        }


    }
}