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
            Console.WriteLine("----------");
            Console.WriteLine("Card Id: " + card.Id);
            Console.WriteLine("Card Info: " + card.Info);
            Console.WriteLine("Card ColumnId: " + card.ColumnId);
            Console.WriteLine("Card Title: " + card.Title);
            Console.WriteLine("----------");

            return View(card);
        }
    }
}