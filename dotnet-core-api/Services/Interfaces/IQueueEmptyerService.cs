namespace TodoApi.Services.Interfaces
{

    public interface IQueueEmptyerService
    {
        Task<string> PleaseDoEmptyQueue();
    }
}