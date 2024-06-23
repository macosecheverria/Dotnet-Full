
namespace Api_Author.Services;

public class ServiceB : IService
{
    private readonly ServiceTransient serviceTransient;

    private readonly ServiceScope serviceScope;

    private readonly ServiceSingleton serviceSingleton;

    public ServiceB(ServiceTransient serviceTransient, ServiceScope serviceScope, ServiceSingleton serviceSingleton)
    {
        this.serviceTransient = serviceTransient;
        this.serviceScope = serviceScope;
        this.serviceSingleton = serviceSingleton;
    }
    public Guid GetScope()
    {
        return serviceTransient.Guid;
    }

    public Guid GetSingleton()
    {
        return serviceScope.Guid; 
    }

    public Guid GetTransient()
    {
       return serviceSingleton.Guid;
    }

    public void PerformTask()
    {
    }
}