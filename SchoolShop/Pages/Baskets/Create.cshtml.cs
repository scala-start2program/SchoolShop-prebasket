using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolShop.Data;
using SchoolShop.Models;

namespace SchoolShop.Pages.Baskets
{
    public class CreateModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;

        public CreateModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ArticleId"] = new SelectList(_context.Article, "Id", "ArticleName");
        ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Basket Basket { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Basket.Add(Basket);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
