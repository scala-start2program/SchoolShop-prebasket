using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolShop.Data;
using SchoolShop.Helpers;
using SchoolShop.Models;

namespace SchoolShop.Pages.Brands
{
    public class CreateModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        public Availability Availability { get; set; }

        public CreateModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            return Page();
        }

        [BindProperty]
        public Brand Brand { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Brand.Add(Brand);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
