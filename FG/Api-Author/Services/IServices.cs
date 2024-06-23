namespace Api_Author.Services;

public interface IService
{
    Guid GetTransient();

    Guid GetScope();

    Guid GetSingleton();

    void PerformTask();
}