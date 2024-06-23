namespace Api_Author.Services;

public class ServiceA : IService
{

    private readonly ServiceTransient serviceTransient;

    private readonly ServiceScope serviceScope;

    private readonly ServiceSingleton serviceSingleton;

    public ServiceA(ServiceTransient serviceTransient, ServiceScope serviceScope, ServiceSingleton serviceSingleton)
    {
        this.serviceTransient = serviceTransient;
        this.serviceScope = serviceScope;
        this.serviceSingleton = serviceSingleton;
    }


    public Guid GetTransient()
    {
        return serviceTransient.Guid;
    }
    public Guid GetScope()
    {
        return serviceScope.Guid;
    }
    public Guid GetSingleton()
    {
        return serviceSingleton.Guid;
    }
    public void PerformTask()
    {
    }
}