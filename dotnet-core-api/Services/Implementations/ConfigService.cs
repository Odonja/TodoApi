using Azure.Messaging.ServiceBus;
using TodoApi.Services.interfaces;

namespace TodoApi.Services.Implementations
{
    public class ConfigService : IConfigService
    {
        private static readonly string queueName = "configurationqueue";
        private const string MY_KEY = "MyKey";
        private const string POSITION_TITLE = "Position:Title";
        private const string POSITION_NAME = "Position:NamE";
        private const string DEFAULT_LOGLEVEL = "Logging:LogLevel:Default";
        private const string HORSE = "Horse";
        private const string MOESTUIN_GROENTE_AARDAPPEL = "Moestuin:Groente:Aardappel";
        private const string MOESTUIN_FRUIT_FRAMBOOS = "Moestuin:Fruit:Framboos";

        private readonly IConfiguration configuration;
        private readonly ServiceBusClient serviceBusClient;

        public ConfigService(IConfiguration configuration, ServiceBusClient serviceBusClient)
        {
            this.configuration = configuration;
            this.serviceBusClient = serviceBusClient;
        }

        public async Task<string> ReceiveConfigurationFromServiceBus()
        {
            ServiceBusReceiver receiver = serviceBusClient.CreateReceiver(queueName);
            ServiceBusReceivedMessage receivedConfigurationMessage = await receiver.ReceiveMessageAsync();
            return receivedConfigurationMessage.Body.ToString();
        }

        public async Task SendConfigurationOverServiceBus(string configuration)
        {
            ServiceBusSender sender = serviceBusClient.CreateSender(queueName);
            ServiceBusMessage configurationMessage = new ServiceBusMessage(configuration);
            await sender.SendMessageAsync(configurationMessage);
        }

        public string ReadConfigurationFromConfigFile()
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
