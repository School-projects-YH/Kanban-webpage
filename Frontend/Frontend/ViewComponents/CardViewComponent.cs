using Frontend.API.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Frontend.ViewComponents
{
    public class CardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CardDTO card)
        {
            return View(card);
        }
    }
}