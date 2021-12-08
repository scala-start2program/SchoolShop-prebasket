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
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace SchoolShop.Pages.Articles
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
        public Article Article { get; set; }
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

            Article = await _context.Article
                .Include(a => a.Brand)
                .Include(a => a.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName");
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
                if (Article.ImagePath != null)
                {
                    string filePath = Path.Combine(webhostEnvironment.WebRootPath, "images", Article.ImagePath);
                    System.IO.File.Delete(filePath);
                }
                Article.ImagePath = ProcessUploadedFile();
            }
            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
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

        private bool ArticleExists(int id)
        {
            return _context.Article.Any(e => e.Id == id);
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
