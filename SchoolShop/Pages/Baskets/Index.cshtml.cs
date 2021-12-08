using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Models;

namespace SchoolShop.Pages.Baskets
{
    public class IndexModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;

        public IndexModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        public IList<Basket> Basket { get;set; }

        public async Task OnGetAsync()
        {
            Basket = await _context.Basket
                .Include(b => b.Article)
                .Include(b => b.User).ToListAsync();
        }
    }
}
