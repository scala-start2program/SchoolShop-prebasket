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
using SchoolShop.Helpers;
using SchoolShop.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SchoolShop.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public Availability Availability { get; set; }
        public EditModel(SchoolShop.Data.SchoolShopContext context,
            IWebHostEnvironment webhostEnvironment)
        {
            _context = context;
            this.webhostEnvironment = webhostEnvironment;
        }

        [BindProperty]
        public Category Category { get; set; }
        [BindProperty]
        public IFormFile PhotoUpload { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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
            if (PhotoUpload != null)
            {
                if (Category.ImagePath != null)
                {
                    string filePath = Path.Combine(webhostEnvironment.WebRootPath, "images", Category.ImagePath);
                    System.IO.File.Delete(filePath);
                }
                Category.ImagePath = ProcessUploadedFile();
            }
            _context.Attach(Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.Id))
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

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (PhotoUpload != null)
            {
                string uploadFolder = Path.Combine(webhostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoUpload.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    PhotoUpload.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
