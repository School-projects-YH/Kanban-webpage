using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.API.Model;
using Microsoft.AspNetCore.Mvc;



namespace Frontend.ViewComponents
{
    public class CreateCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View();
        }
    }
}
