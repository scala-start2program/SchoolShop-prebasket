using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SchoolShop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        public Availability Availability { get; set; }

        public IndexModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Availability = new Availability(_context, HttpContext);
        }

    }
}
