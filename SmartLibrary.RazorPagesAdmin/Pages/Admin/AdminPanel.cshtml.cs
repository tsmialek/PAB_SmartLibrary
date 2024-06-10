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
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminPanelModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Book> Books { get; private set; } = new List<Book>();

        [BindProperty]
        public Book Input { get; set; }

        [BindProperty]
        public string SearchTitle { get; set; }

        public bool IsLoggedIn { get; private set; }
        public string Token { get; private set; }
        public List<string> UserRoles { get; private set; }
        public string UserEmail { get; private set; }

        [TempData]
        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Account/Login");
            }

            IsLoggedIn = true;
            Token = HttpContext.Session.GetString("Token");
            UserEmail = HttpContext.Session.GetString("UserEmail");

            var rolesJson = HttpContext.Session.GetString("UserRoles");
            UserRoles = rolesJson != null ? JsonConvert.DeserializeObject<List<string>>(rolesJson) : new List<string>();

            await LoadBooksAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAddBookAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = new StringContent(JsonConvert.SerializeObject(Input), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:7207/books", content);

            if (response.IsSuccessStatusCode)
            {
                await LoadBooksAsync();
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

        public async Task<IActionResult> OnPostDeleteBookAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.DeleteAsync($"https://localhost:7207/books/{id}");

            if (response.IsSuccessStatusCode)
            {
                await LoadBooksAsync();
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

        public async Task<IActionResult> OnPostSearchBooksAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"https://localhost:7207/books/search?name={SearchTitle}");

            if (response is not null)
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Books = new List<Book>() { JsonConvert.DeserializeObject<Book>(responseString) };
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
                        if (errorResponse.Title is null)
                        {
                            ErrorMessage = "An error occurred while searching for books";
                        }
                        else
                        {
                            ErrorMessage = errorResponse.Title;
                        }
                    }
                }
            }
            return Page();
        }

        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }

        private async Task LoadBooksAsync()
        {
            var client = _httpClientFactory.CreateClient("SmartLibraryAPI");
            var accessToken = HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://localhost:7207/books");

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Books = JsonConvert.DeserializeObject<List<Book>>(responseString);
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
    }

    public class ErrorResponse
    {
        public string Title { get; set; }
    }
}
