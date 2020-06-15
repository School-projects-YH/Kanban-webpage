using Frontend.API.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public int Id { get; set; }
        private HttpClient client;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        public List<BoardDTO> boardList = new List<BoardDTO>();

        public bool IsLoggedIn = false;
        public bool CreateNewAccount = false;

        public void OnGet()
        {
        }

        //public IActionResult Onpost()
        //{
        //  GetCardsByBoardIdAsync(1);
        //return RedirectToPage("/Board");
        //}
    }
}