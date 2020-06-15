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
        public static int BoardId { get; set; }
        ApiHandler api;

        public BoardModel(ILogger<BoardModel> logger)
        {
            _logger = logger;
            var client = new HttpClient();
            api = new ApiHandler(client);
        }

          public List<CardDTO> CardList = new List<CardDTO>();
          public List<CardDTO> columnOne = new List<CardDTO>();
          public List<CardDTO> columnTwo = new List<CardDTO>();
          public List<CardDTO> columnThree = new List<CardDTO>();
          public List<CardDTO> columnFour = new List<CardDTO>();

        public async Task OnGet(int Id)
        {
            BoardId = Id;
            await GetCardsByBoardIdAsync(Id);

        }
        public async Task OnPost()
        {
            var cardIdValue = Request.Form["cardId"];
            int cardId = Convert.ToInt32(cardIdValue);
            await api.MoveLeftAsync(cardId);
            var link = String.Format("/Board?id={0}", BoardId);
            Response.Redirect(link);


        }

        public async Task GetCardsByBoardIdAsync(int Id)
        {

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
