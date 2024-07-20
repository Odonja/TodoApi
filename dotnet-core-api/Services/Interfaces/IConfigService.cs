using TodoApi.Models;

namespace TodoApi.Services.Interfaces
{
    public interface IConfigService
    {
        Task<string> ReceiveConfigurationFromServiceBus();
        Task SendConfigurationOverServiceBus(ConfigFields field);
        string ReadConfigurationFromConfigFile();
    }
}