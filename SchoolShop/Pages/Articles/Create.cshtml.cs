using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolShop.Data;
using SchoolShop.Helpers;
using SchoolShop.Models;

namespace SchoolShop.Pages.Articles
{
    public class CreateModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public Availability Availability { get; set; }

        [BindProperty]
        public Article Article { get; set; }
        public IFormFile PhotoUpload { get; set; }
        public CreateModel(SchoolShop.Data.SchoolShopContext context,
            IWebHostEnvironment webhostEnvironment)
        {
            _context = context;
            this.webhostEnvironment = webhostEnvironment;
        }

        public IActionResult OnGet()
        {
            Availability = new Availability(_context, HttpContext);
            if (!Availability.IsAdmin)
            {
                return RedirectToPage("../Articles/Index");
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategoryName");
            return Page();
        }



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
            if (PhotoUpload != null)
            {
                if (Article.ImagePath != null)
                {
                    string filePath = Path.Combine(webhostEnvironment.WebRootPath, "images", Article.ImagePath);
                    System.IO.File.Delete(filePath);
                }
                Article.ImagePath = ProcessUploadedFile();
            }
            _context.Article.Add(Article);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
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
