using Microsoft.AspNetCore.Mvc;
using TodoApi.Services.interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : Controller
    {


        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfigService service;
        public ConfigController(IConfigService service)
        {
            this.service = service;
        }

        // GET: api/Config/no-service-bus
        [HttpGet("no-service-bus")]
        public async Task<ContentResult> GetConfig()
        {
            string configuration = service.ReadConfigurationFromConfigFile();

            return await Task.FromResult(Content(configuration));
        }

        // GET:  api/Config/use-service-bus
        [HttpGet("use-service-bus")]
        public async Task<ContentResult> GetConfigWithServiceBus()
        {
            // await using var client = new ServiceBusClient(connectionString);
            await service.SendConfigurationOverServiceBus(service.ReadConfigurationFromConfigFile());
            string configuration = await service.ReceiveConfigurationFromServiceBus();

            return await Task.FromResult(Content(configuration));
        }


    }
}