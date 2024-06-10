using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Newtonsoft.Json;

namespace SmartLibrary.RazorPagesAdmin.Pages
{ 
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var loginRequest = new
            {
                Email = Input.Email,
                Password = Input.Password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);

                HttpContext.Session.SetString("UserEmail", authResponse.Email);
                HttpContext.Session.SetString("Token", authResponse.Token);
                HttpContext.Session.SetString("UserRoles", JsonConvert.SerializeObject(authResponse.Roles));

                return RedirectToPage("/Admin/AdminPanel");
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                ErrorMessage = $"Login failed: {errorResponse.Title}";
                return Page();
            }
        }

        public class ErrorResponse
        {
            public string Title { get; set; }
        }

        public class AuthenticationResponse
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public List<string> Roles { get; set; }
            public string Token { get; set; }
        }
    }
}
