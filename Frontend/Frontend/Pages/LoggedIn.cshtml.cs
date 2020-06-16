using Frontend.API;
using Frontend.API.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Pages
{
    public class LoggedInModel : PageModel
    {
        private readonly ILogger<LoggedInModel> _logger;
        private ApiHandler api;

        public LoggedInModel(ILogger<LoggedInModel> logger)
        {
            _logger = logger;
            HttpClient client = new HttpClient();
            api = new ApiHandler(client);
        }

        public int Id { get; set; }
        public List<BoardDTO> boardList = new List<BoardDTO>();

        public async Task OnGet(int id)
        {
            Id = id;
            await GetBoardsFromDBAsync(id);
        }

        public async Task GetBoardsFromDBAsync(int id)
        {
            var boards = await api.boardService.GetByUserIdAsync(id);

            foreach (var item in boards)
            {
                boardList.Add(item);
            }
        }

        public async Task Onpost(int id)
        {
            await GetBoardsFromDBAsync(id);

            var boardId = await CreateNewBoard();
            var link = String.Format("/Board?id={0}", boardId);
            Response.Redirect(link);
        }

        public async Task<int> CreateNewBoard()
        {
            var userIdrequest = Request.Form["userId"];
            var userId = Convert.ToInt32(userIdrequest);
            var boardTitle = Request.Form["btitle"];

            var createdBoard = new BoardDTO
            {
                Title = boardTitle,
                UserId = userId
            };

            var board = await api.boardService.Create(createdBoard);
            return board;
        }
    }
}