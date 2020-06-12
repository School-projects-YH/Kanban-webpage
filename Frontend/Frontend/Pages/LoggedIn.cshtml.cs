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
    public class LoggedInModel : PageModel
    {
        private readonly ILogger<LoggedInModel> _logger;
        ApiHandler api;
        public LoggedInModel(ILogger<LoggedInModel> logger)
        {
            _logger = logger;
            HttpClient client = new HttpClient();
            api = new ApiHandler(client);
        }

        public int Id { get; set; }
        public List<BoardDTO> boardList = new List<BoardDTO>();
        public async Task OnGet()
        {
            await GetBoardsFromDBAsync();
        }

        public async Task GetBoardsFromDBAsync()
        {
            var boards = await api.GetBoardsAsync();

            foreach (var item in boards)
            {

                boardList.Add(item);
            }
        }

        public async Task Onpost()
        {
            var board = await CreateNewBoard();
            var link = String.Format("/Board?id={0}", board.Id);
            Response.Redirect(link);
        }

        public async Task<BoardDTO> CreateNewBoard()
        {
            var boardTitle = Request.Form["btitle"];
            var board = await api.CreateBoard(boardTitle);
            return board;
        }
    }
}
