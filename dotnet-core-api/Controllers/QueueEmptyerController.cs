using Microsoft.AspNetCore.Mvc;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueEmptyerController(IQueueEmptyerService service) : Controller
    {
        private readonly IQueueEmptyerService service = service;

        [HttpDelete]
        public async Task<string> PleaseDoEmptyQueue()
        {
            return await service.PleaseDoEmptyQueue();
        }
    }
}