﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend
{
    public class Default1Model : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost(int Id)
        {
            Console.WriteLine("Hej Left id: " + Id );
        }
    }
}