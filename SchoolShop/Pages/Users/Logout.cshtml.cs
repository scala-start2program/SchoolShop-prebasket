using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolShop.Pages.Users
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(-1));
            HttpContext.Response.Cookies.Append("UserId", "", cookieOptions);
            Response.Redirect("../Articles/Index");
        }

    }
}
