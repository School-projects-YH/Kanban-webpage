using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Frontend.API.Model;

namespace Frontend.ViewComponents
{
    public enum Movement
    {
        Left,
        Right
    }
    public class MovementButtonViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Movement movement)
        {
            if(movement == Movement.Right)
            {
                return View("Right");
            }

            if(movement == Movement.Left)
            {
                return View("Left");
            }

            return null;
        }
        
    }
}
