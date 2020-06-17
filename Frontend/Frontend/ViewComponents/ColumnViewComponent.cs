using Frontend.API;
using Frontend.API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Frontend.ViewComponents
{
    public class ColumnViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ColumnDTO column)
        {
           
                return View(column);
            
        }
    }
}