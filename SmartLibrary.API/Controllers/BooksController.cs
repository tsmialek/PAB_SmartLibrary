using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartLibrary.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult ListBooks()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
