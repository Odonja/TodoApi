using Azure.Messaging.ServiceBus;
using TodoApi.Models;
using TodoApi.Services.Interfaces;
namespace TodoApi.Services.Implementations
{
    public class ConfigService(IConfiguration configuration, ServiceBusClient serviceBusClient) : IConfigService
    {
        private static readonly string queueName = "configurationqueue";

        private readonly IConfiguration configuration = configuration;
        private readonly ServiceBusClient serviceBusClient = serviceBusClient;

        public async Task<string> ReceiveConfigurationFromServiceBus()
        {
            var receiver = serviceBusClient.CreateReceiver(queueName);
            var receivedConfigurationMessage = await receiver.ReceiveMessageAsync();
            await receiver.CompleteMessageAsync(receivedConfigurationMessage);
            var requestedField = receivedConfigurationMessage.Body.ToString();
            var fieldValue = configuration[requestedField];

            return $"{requestedField}: {fieldValue}";
        }

        public async Task SendConfigurationOverServiceBus(ConfigFields field)
        {
            var sender = serviceBusClient.CreateSender(queueName);
            var configurationMessage = new ServiceBusMessage(field.GetValue());
            await sender.SendMessageAsync(configurationMessage);
        }

        public string ReadConfigurationFromConfigFile()
        {
            var myKeyValue = configuration[ConfigFields.MY_KEY.GetValue()];
            var title = configuration[ConfigFields.POSITION_TITLE.GetValue()];
            var name = configuration[ConfigFields.POSITION_NAME.GetValue()];
            var defaultLogLevel = configuration[ConfigFields.DEFAULT_LOGLEVEL.GetValue()];
            var moestuinGroente = configuration[ConfigFields.MOESTUIN_GROENTE_AARDAPPEL.GetValue()];
            var moestuinFruit = configuration[ConfigFields.MOESTUIN_FRUIT_FRAMBOOS.GetValue()];
            var horse = configuration[ConfigFields.HORSE.GetValue()];

            var content = $"{ConfigFields.MY_KEY.GetValue()}: {myKeyValue} \n" +
                          $"{ConfigFields.POSITION_TITLE.GetValue()}: {title} \n" +
                          $"{ConfigFields.POSITION_NAME.GetValue()}: {name} \n" +
                          $"{ConfigFields.MOESTUIN_GROENTE_AARDAPPEL.GetValue()}: {moestuinGroente} \n" +
                          $"{ConfigFields.MOESTUIN_FRUIT_FRAMBOOS.GetValue()}: {moestuinFruit} \n" +
                          $"{ConfigFields.HORSE.GetValue()}: {horse} \n" +
                          $"{ConfigFields.DEFAULT_LOGLEVEL.GetValue()}: {defaultLogLevel}";
            return content;
        }



    }
}
