using Frontend.API.Model;
using Frontend.API;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> OnPost()
        {
            var loginEmail = Request.Form["email"];
            var loginPassword = Request.Form["pass"];
            
            UserLoginDTO user = new UserLoginDTO();
            user.UserEmail = loginEmail;
            user.Password = loginPassword;
            
            ApiHandler api = new ApiHandler(client);

            int userId = await api.userService.UserLoginRequestAsync(user);

            if (userId != 0)
            {
                return RedirectToPage("LoggedIn", new { id = userId });
            }
            else
            {
                return new RedirectToPageResult("Index");
            }
        }
    }
}