using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartLibrary.RazorPagesAdmin.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SmartLibrary.RazorPagesAdmin.Pages.Admin
{
    public class UserManagementModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public string Token { get; private set; }
        public List<string> UserRoles { get; private set; }
        public string UserEmail { get; private set; }

        public UserManagementModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<UserResponse> Users { get; private set; } = new List<UserResponse>();

        [BindProperty]
        public RegisterRequest Input { get; set; }

        [BindProperty]
        public string SearchEmail { get; set; }

        [TempData]
        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login");
            }

            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            UserEmail = HttpContext.Session.GetString("UserEmail");
            var rolesJson = HttpContext.Session.GetString("UserRoles");
            UserRoles = rolesJson != null ? JsonConvert.DeserializeObject<List<string>>(rolesJson) : new List<string>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:7207/auth/all");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<UserResponse>>(responseString);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    ErrorMessage = "You don't have permissions to access this endpoint";
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                    ErrorMessage = errorResponse.Title;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRegisterUserAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(Input), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7207/auth/register", content);

            if (response.IsSuccessStatusCode)
            {
                await LoadUsersAsync();
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    ErrorMessage = "You don't have permissions to access this endpoint";
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                    ErrorMessage = errorResponse.Title;
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSearchUserByEmailAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"https://localhost:7207/auth/email/{SearchEmail}");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserResponse>(responseString);
                Users = [user];
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    ErrorMessage = "You don't have permissions to access this endpoint";
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                    ErrorMessage = errorResponse.Title;
                }
            }

            return Page();
        }

        private async Task LoadUsersAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:7207/auth/all");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<List<UserResponse>>(responseString);
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    ErrorMessage = "You don't have permissions to access this endpoint";
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                    ErrorMessage = errorResponse.Title;
                }
            }
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        public class RegisterRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UserResponse
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public List<Role> Roles { get; set; }
        }

        public class Role
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class ErrorResponse
        {
            public string Title { get; set; }
        }
    }
}
