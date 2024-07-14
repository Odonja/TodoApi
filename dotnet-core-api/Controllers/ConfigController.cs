using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : Controller
    {
        // set the environment variable with: setx AZURE_SERVICE_BUS_CONNECTION_STRING "<the connection string>"
        private static readonly string? connectionString = Environment.GetEnvironmentVariable("AZURE_SERVICE_BUS_CONNECTION_STRING");
        private static readonly string queueName = "configurationqueue";
        private const string MY_KEY = "MyKey";
        private const string POSITION_TITLE = "Position:Title";
        private const string POSITION_NAME = "Position:NamE";
        private const string DEFAULT_LOGLEVEL = "Logging:LogLevel:Default";
        private const string HORSE = "Horse";
        private const string MOESTUIN_GROENTE_AARDAPPEL = "Moestuin:Groente:Aardappel";
        private const string MOESTUIN_FRUIT_FRAMBOOS = "Moestuin:Fruit:Framboos";

        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration configuration;
        public ConfigController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: api/Config/no-service-bus
        [HttpGet("no-service-bus")]
        public async Task<ContentResult> GetConfig()
        {
            string configuration = ReadConfigurationFromConfigFile();

            return await Task.FromResult(Content(configuration));
        }

        // GET: api/Config/use-service-bus
        [HttpGet("use-service-bus")]
        public async Task<ContentResult> GetConfigWithServiceBus()
        {
            await using var client = new ServiceBusClient(connectionString);
            await SendConfigurationOverServiceBus(ReadConfigurationFromConfigFile(), client);
            string configuration = await ReceiveConfigurationFromServiceBus(client);

            return await Task.FromResult(Content(configuration));
        }

        private static async Task<string> ReceiveConfigurationFromServiceBus(ServiceBusClient client)
        {
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);
            ServiceBusReceivedMessage receivedConfigurationMessage = await receiver.ReceiveMessageAsync();
            return receivedConfigurationMessage.Body.ToString();
        }

        private static async Task SendConfigurationOverServiceBus(string configuration, ServiceBusClient client)
        {
            ServiceBusSender sender = client.CreateSender(queueName);
            ServiceBusMessage configurationMessage = new ServiceBusMessage(configuration);
            await sender.SendMessageAsync(configurationMessage);
        }

        private string ReadConfigurationFromConfigFile()
        {
            var myKeyValue = configuration[MY_KEY];
            var title = configuration[POSITION_TITLE];
            var name = configuration[POSITION_NAME];
            var defaultLogLevel = configuration[DEFAULT_LOGLEVEL];
            var moestuinGroente = configuration[MOESTUIN_GROENTE_AARDAPPEL];
            var moestuinFruit = configuration[MOESTUIN_FRUIT_FRAMBOOS];
            var horse = configuration[HORSE];

            var content = $"{MY_KEY}: {myKeyValue} \n" +
                          $"{POSITION_TITLE}: {title} \n" +
                          $"{POSITION_NAME}: {name} \n" +
                          $"{MOESTUIN_GROENTE_AARDAPPEL}: {moestuinGroente} \n" +
                          $"{MOESTUIN_FRUIT_FRAMBOOS}: {moestuinFruit} \n" +
                          $"{HORSE}: {horse} \n" +
                          $"{DEFAULT_LOGLEVEL}: {defaultLogLevel}";
            return content;
        }
    }
}