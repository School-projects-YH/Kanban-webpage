using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Frontend.API;
using Frontend.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Frontend.Pages
{
    public class BoardModel : PageModel
    {
        
        private readonly ILogger<BoardModel> _logger;
        public int Id { get; set; }
        HttpClient client;

        public BoardModel(ILogger<BoardModel> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

          public List<CardDTO> CardList = new List<CardDTO>();
          public List<CardDTO> columnOne = new List<CardDTO>();
          public List<CardDTO> columnTwo = new List<CardDTO>();
          public List<CardDTO> columnThree = new List<CardDTO>();
          public List<CardDTO> columnFour = new List<CardDTO>();

        public async Task OnGet(int Id)
        {
            Console.WriteLine(Id);
            await GetCardsByBoardIdAsync(Id);

        }
        public async Task Onpost()
        {
            var id = Request.Form["cardId"];
            var id2 = 999;
            Console.WriteLine(id+ id2);
        }

        public async Task GetCardsByBoardIdAsync(int Id)
        {
            ApiHandler api = new ApiHandler(client);

            var cards = await api.GetCardsByBoardIdAsync(Id);

            foreach (var item in cards)
            { 

                    switch(item.ColumnId)
                    {
                            case 1:
                                columnOne.Add(item);
                               break;
                            case 2:
                                 columnTwo.Add(item);
                               break;
                            case 3:
                                 columnThree.Add(item);
                               break;
                            case 4:
                                  columnFour.Add(item);
                               break;

                    }



                //CardList.Add(item);
            }
        }
    }
}
