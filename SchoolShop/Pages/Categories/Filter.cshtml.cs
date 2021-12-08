using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolShop.Helpers;
using SchoolShop.Models;

namespace SchoolShop.Pages.Categories
{
    public class FilterModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        public Availability Availability { get; set; }
        public IList<Category> Categories { get; set; }

        public FilterModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            Availability = new Availability(_context, HttpContext);
            IQueryable<Category> catquery = _context.Category
                .OrderBy(a => a.CategoryName);
            Categories = catquery.ToList();
        }
    }

}
