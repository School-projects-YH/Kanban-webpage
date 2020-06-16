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
        public Board board { get; set; }
        public ICollection<CardDTO> cardDTOs;

        public BoardModel(ILogger<BoardModel> logger)
        {
            
            _logger = logger;
        }

        public async Task OnGet(int Id)
        {
            BoardId = Id;
            board = new Board(Id);
            await GetColumnsByBoardIdAsync(Id);
            await GetCardsByBoardIdAsync(Id);
        }

        public async Task OnPost(int Id)
        {
            board = new Board(Id);
            await GetCardsByBoardIdAsync(Id);


            int cardIdValue = Convert.ToInt32(Request.Form["cardId"]);
            if (cardIdValue != 0) 
            // Annoying to develop more POST forms with this solution
            {
                int cardId = Convert.ToInt32(cardIdValue);

                
                var button = Request.Form["button"];
                if (button == "left")
                {
                    using (var api = new ApiHandler())
                    {
                        await api.moveLogicService.MoveLeftAsync(cardId);
                    }
                }
                else if (button == "right")
                {
                    using (var api = new ApiHandler())
                    {
                        await api.moveLogicService.MoveRightAsync(cardId);
                    }
                }
                else if (button == "delete")
                {

                    using (var api = new ApiHandler())
                    {
                        await api.cardService.Delete(cardId);
                    }
                }
            }
            else
            {
                // Collect data
                string info = Request.Form["card-info"];
                // Create card object
                
                var newCard = new CardDTO
                {
                    BoardId = Id,
                    ColumnId = 1,
                    Info = info
                };
                // Send the created card to the server
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
            using(var api = new ApiHandler())
            {
                var cards = await api.cardService.GetByBoardIdAsync(Id);
                await SortCardsIntoColumns(cards.AsEnumerable());
            }
        }
        public async Task GetColumnsByBoardIdAsync(int Id)
        {
            using(var api = new ApiHandler())
            {
                board.columns = await api.columnService.GetColumnsByBoardIdAsync(Id);
            }
        }

        private async Task SortCardsIntoColumns(IEnumerable<CardDTO> cards)
        {
            // TODO
            // Right now need to manually fill the column list in the board
            var cardArray = cards.ToArray();
            var columns = board.columns.ToList();
            for (int i = 0; i < columns.Count; i++)
            {
                columns[i].Cards.AddRange(
                    (from array in cardArray
                                  where array.ColumnId == (i + 1)
                                  select array));
            }
        }
    }
}
