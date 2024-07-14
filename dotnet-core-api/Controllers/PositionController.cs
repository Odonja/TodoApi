using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : Controller
    {
        private readonly PositionOptions options;
        public PositionController(IOptions<PositionOptions> options)
        {
            this.options = options.Value;
        }


        // GET: api/Position
        [HttpGet]
        public async Task<ContentResult> GetPosition()
        {
            return await Task.FromResult(Content($"Title: {options.Title} \n" +
                        $"Name: {options.Name}"));
        }

    }
}
