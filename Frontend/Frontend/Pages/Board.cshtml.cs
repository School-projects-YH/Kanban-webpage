using Frontend.API;
using Frontend.API.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Pages
{
    public class BoardModel : PageModel
    {
        private readonly ILogger<BoardModel> _logger;
        public static int BoardId { get; set; }
        private ApiHandler api { get; }
        public Board board { get; set; }

        public BoardModel(ILogger<BoardModel> logger)
        {
            _logger = logger;
            api = new ApiHandler();
        }

        public async Task OnGet(int Id)
        {
            BoardId = Id;
            board = new Board(Id);

            await GetCardsByBoardIdAsync(Id);
        }

        public async Task OnPost(int Id)
        {
            board = new Board(Id);
            await GetCardsByBoardIdAsync(Id);


            int cardIdValue = Convert.ToInt32(Request.Form["cardId"]);
            if (cardIdValue != 0)
            {
                int cardId = Convert.ToInt32(cardIdValue);

                var button = Request.Form["button"];
                if (button == "left")
                {
                    await api.MoveLeftAsync(cardId);
                }
                else if (button == "right")
                {
                    await api.MoveRightAsync(cardId);
                }
                else if (button == "delete")
                {
                Console.WriteLine("tar bort kort");
                await api.DeleteCardAsync(cardId);
                }

            }
            else
            {
                // Samla data
                string info = Request.Form["card-info"];
                // Skapa card objekt
                var newCard = new CardDTO
                {
                    
                    ColumnId = 1,
                    Info = info
                };
                // Skicka till servern
                using (var api = new ApiHandler())
                {
                    await api.cardService.Create(newCard);
                }

            }
            var link = String.Format("/Board?id={0}", BoardId);
            Response.Redirect(link);

        }

        public async Task GetCardsByBoardIdAsync(int Id)
        {
            var cards = await api.cardService.GetByBoardIdAsync(Id);

            await SortCardsIntoColumns(cards.AsEnumerable());
        }

        private async Task SortCardsIntoColumns(IEnumerable<CardDTO> cards)
        {
            var cardArray = cards.ToArray();

            for (int i = 0; i < board.columns.Count; i++)
            {
                board.columns[i].Cards = (from array in cardArray
                                          where array.ColumnId == (i + 1)
                                          select array).ToList();
            }
        }
    }
}
