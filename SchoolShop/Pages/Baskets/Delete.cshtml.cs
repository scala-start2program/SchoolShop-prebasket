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
    public class DeleteModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;

        public DeleteModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Basket Basket { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Basket = await _context.Basket
                .Include(b => b.Article)
                .Include(b => b.User).FirstOrDefaultAsync(m => m.Id == id);

            if (Basket == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Basket = await _context.Basket.FindAsync(id);

            if (Basket != null)
            {
                _context.Basket.Remove(Basket);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
