using Microsoft.AspNetCore.Mvc.Filters;

namespace Api_Author.Filters;

public class MyFilterAction : IActionFilter
{

    private readonly ILogger<MyFilterAction> logger;

    public MyFilterAction(ILogger<MyFilterAction> logger){
        this.logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("Antes de ejecutar la accion");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.LogInformation("Despues de ejecutar la informacion");
    }

}