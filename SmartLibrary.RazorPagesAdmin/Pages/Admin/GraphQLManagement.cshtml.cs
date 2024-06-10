using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartLibrary.RazorPagesAdmin.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SmartLibrary.RazorPagesAdmin.Pages.Admin
{
    public class GraphQLManagementModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GraphQLManagementModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public List<string> UserRoles { get; private set; }
        public string UserEmail { get; private set; }

        [BindProperty]
        public string Query { get; set; }
        public string Result { get; set; }

        [TempData]
        public string ErrorMessage { get; set; } = "";

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Login");
            }

            UserEmail = HttpContext.Session.GetString("UserEmail");

            var rolesJson = HttpContext.Session.GetString("UserRoles");
            UserRoles = rolesJson != null ? JsonConvert.DeserializeObject<List<string>>(rolesJson) : new List<string>();

            return Page();
        }

        public async Task<IActionResult> OnPostExecuteQueryAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var content = new StringContent(JsonConvert.SerializeObject(new { query = Query }), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7207/graphql", content);

            if (response.IsSuccessStatusCode)
            {
                Result = await response.Content.ReadAsStringAsync();
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

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        public class ErrorResponse
        {
            public string Title { get; set; }
        }
    }
}
