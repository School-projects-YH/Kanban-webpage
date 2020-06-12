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
        HttpClient client;

        public LoggedInModel(ILogger<LoggedInModel> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        public int Id { get; set; }
        public List<BoardDTO> boardList = new List<BoardDTO>();
        public async Task OnGet()
        {
            await GetBoardsFromDBAsync();
        }



        public async Task GetBoardsFromDBAsync()
        {
            ApiHandler api = new ApiHandler(client);

            var boards = await api.GetBoardsAsync();

            foreach (var item in boards)
            {

                boardList.Add(item);
            }
        }
    }
}
