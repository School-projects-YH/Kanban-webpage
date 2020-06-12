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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public int Id { get; set; }
        HttpClient client;

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
