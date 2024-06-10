using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartLibrary.RazorPagesAdmin.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SmartLibrary.RazorPagesAdmin.Pages.Admin
{
    public class RoleManagementModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RoleManagementModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public List<string> UserRoles { get; private set; }
        public string UserEmail { get; private set; }

        public List<RoleResponse> Roles { get; private set; } = new List<RoleResponse>();

        [BindProperty]
        public string RoleName { get; set; }

        [BindProperty]
        public AddUserRoleByEmailAndNameRequest AddUserRoleByEmailRequest { get; set; }

        [BindProperty]
        public AddUserRoleByIdRequest AddUserRoleById { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login");
            }

            UserEmail = HttpContext.Session.GetString("UserEmail");

            var rolesJson = HttpContext.Session.GetString("UserRoles");
            UserRoles = rolesJson != null ? JsonConvert.DeserializeObject<List<string>>(rolesJson) : new List<string>();

            await LoadRolesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddUserRoleByEmailAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(AddUserRoleByEmailRequest), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7207/roles/addUserRoleByEmail", content);

            if (response.IsSuccessStatusCode)
            {
                await LoadRolesAsync();
            }
            else
            {
                await HandleErrorResponse(response);
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddUserRoleByIdAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(AddUserRoleById), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7207/roles/addUserRoleById", content);

            if (response.IsSuccessStatusCode)
            {
                await LoadRolesAsync();
            }
            else
            {
                await HandleErrorResponse(response);
            }

            return RedirectToPage();
        }

        private async Task LoadRolesAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:7207/roles");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Roles = JsonConvert.DeserializeObject<List<RoleResponse>>(responseString);
            }
            else
            {
                await HandleErrorResponse(response);
            }
        }

        private async Task HandleErrorResponse(HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(responseString);

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ErrorMessage = "You don't have permissions to access this endpoint";
            }
            else
            {
                ErrorMessage = errorResponse.Title;
            }
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        public class RoleResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public List<User> Users { get; set; }
        }

        public class User
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class AddUserRoleByEmailAndNameRequest
        {
            public string UserEmail { get; set; }
            public string NewUserRoleName { get; set; }
        }

        public class AddUserRoleByIdRequest
        {
            public Guid UserId { get; set; }
            public Guid NewUserRoleId { get; set; }
        }

        public class ErrorResponse
        {
            public string Title { get; set; }
        }
    }
}
