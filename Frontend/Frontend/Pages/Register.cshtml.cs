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
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        public int Id { get; set; }
        private HttpClient client;

        public RegisterModel(ILogger<RegisterModel> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }
        public async Task<IActionResult> OnPost()
        {
            var loginEmail = Request.Form["email"];
            var loginPassword = Request.Form["password"];
            var loginPasswordTwo = Request.Form["passwordVeri"];
            if(loginEmail!=""&&loginPassword!=""&&loginPasswordTwo!="")
            {

                if(loginPassword==loginPasswordTwo)
                {
                    UserLoginDTO user = new UserLoginDTO();
                    user.UserEmail = loginEmail;
                    user.Password = loginPassword;


                    using (var api = new ApiHandler())
                    {
                        await api.userService.Create(user);
                    }

            
           
                    return RedirectToPage("Index");
                }
                else
                {
                     return new RedirectToPageResult("Register");
                }
               
            }
            else
            {
                    return new RedirectToPageResult("Register");
            }
            
        
        }
    }
}