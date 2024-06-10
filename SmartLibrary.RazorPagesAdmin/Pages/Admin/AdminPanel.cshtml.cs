using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartLibrary.RazorPagesAdmin.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SmartLibrary.RazorPagesAdmin.Pages.Admin
{
    public class AdminPanelModel : PageModel
    {
        public string Token { get; private set; }
        public List<string> UserRoles { get; private set; }
        public string UserEmail { get; private set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login");
            }

            Token = HttpContext.Session.GetString("Token");
            UserEmail = HttpContext.Session.GetString("UserEmail");

            var rolesJson = HttpContext.Session.GetString("UserRoles");
            UserRoles = rolesJson != null ? JsonConvert.DeserializeObject<List<string>>(rolesJson) : new List<string>();

            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}

