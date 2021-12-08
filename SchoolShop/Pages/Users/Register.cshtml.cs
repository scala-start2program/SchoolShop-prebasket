using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolShop.Helpers;
using SchoolShop.Models;

namespace SchoolShop.Pages.Users
{
    public class RegisterModel : PageModel
    {
        private readonly SchoolShop.Data.SchoolShopContext _context;
        [BindProperty]
        public User User { get; set; }
        public Availability Availability { get; set; }
        public RegisterModel(SchoolShop.Data.SchoolShopContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            Availability = new Availability(_context, HttpContext);
            return Page();
        }
        public IActionResult OnPost()
        {
            Availability = new Availability(_context, HttpContext);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            User.Password = Encoding.HashPassword(User.Password);
            _context.User.Add(User);
            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return Page();
            }

            string IdCookie = Encoding.EncryptString(User.Id.ToString(), "P@sw00rd");
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(365));
            HttpContext.Response.Cookies.Append("UserId", IdCookie, cookieOptions);
            return RedirectToPage("../Articles/Index");

        }
    }

}
