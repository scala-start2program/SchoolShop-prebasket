using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Models;

namespace SchoolShop.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;

        public IndexModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }

        public async Task OnGetAsync()
        {
            Order = await _context.Order
                .Include(o => o.User).ToListAsync();
        }
    }
}
