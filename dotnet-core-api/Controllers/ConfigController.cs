using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController(IConfigService service) : Controller
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfigService service = service;

        // GET: api/Config/no-service-bus
        [HttpGet("no-service-bus")]
        public async Task<ContentResult> GetConfig()
        {
            string configuration = service.ReadConfigurationFromConfigFile();

            return await Task.FromResult(Content(configuration));
        }

        // GET:  api/Config/use-service-bus
        [HttpGet("use-service-bus/{field}")]
        public async Task<ContentResult> GetConfigWithServiceBus(ConfigFields field)
        {
            // await using var client = new ServiceBusClient(connectionString);
            await service.SendConfigurationOverServiceBus(field);
            string configuration = await service.ReceiveConfigurationFromServiceBus();

            return await Task.FromResult(Content(configuration));
        }


    }
}