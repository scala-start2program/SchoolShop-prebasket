using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolShop.Data;
using SchoolShop.Models;

namespace SchoolShop.Pages.Baskets
{
    public class EditModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;

        public EditModel(SchoolShop.Data.SchoolShopContext context)
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
           ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "ArticleName");
           ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Basket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketExists(Basket.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BasketExists(int id)
        {
            return _context.Basket.Any(e => e.Id == id);
        }
    }
}
