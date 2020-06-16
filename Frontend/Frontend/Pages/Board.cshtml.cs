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

            var cardIdValue = Request.Form["cardId"];
            int cardId = Convert.ToInt32(cardIdValue);

            var button = Request.Form["button"];
            if (button == "left") {
                await api.MoveLeftAsync(cardId);
            } else {

                await api.MoveRightAsync(cardId);
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