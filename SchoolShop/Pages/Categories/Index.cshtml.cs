using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Models;
using SchoolShop.Helpers;

namespace SchoolShop.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        public Availability Availability { get; set; }
        public IndexModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get; set; }

        public async Task OnGetAsync()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                RedirectToPage("../Articles/Index");
            }
            Category = await _context.Category.ToListAsync();
        }
    }
}
