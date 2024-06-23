
namespace Api_Author.Services;

public class WriterOnFile : IHostedService
{

    private readonly IWebHostEnvironment env;

    private  readonly string fileName = "Archivo1.txt";

    private Timer? timer;

    public WriterOnFile(IWebHostEnvironment env){
        this.env = env;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(DoWork!, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        Writer("Proceso Iniciado");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer!.Dispose();
        Writer("Proceso Terminado");
        return Task.CompletedTask;
    }


    private void DoWork(object state){
        Writer("Proceso en ejecucion" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
    }

    private void Writer(string message){
        var route = $@"{env.ContentRootPath}\wwwroot\{fileName}";

        using(StreamWriter writer = new StreamWriter(route, append: true)){
            writer.WriteLine(message);
        }
    }
}