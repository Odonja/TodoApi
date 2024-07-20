namespace TodoApi.Services.interfaces
{
    public interface IConfigService
    {
        Task<string> ReceiveConfigurationFromServiceBus();
        Task SendConfigurationOverServiceBus(string configuration);
        string ReadConfigurationFromConfigFile();
    }
}