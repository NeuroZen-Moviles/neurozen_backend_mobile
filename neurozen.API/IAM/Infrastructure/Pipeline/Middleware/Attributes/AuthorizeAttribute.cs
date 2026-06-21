using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using neurozen.API.IAM.Domain.Model.Aggregates;

namespace neurozen.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;

/**
 * This attribute is used to decorate controllers and actions that require authorization.
 * It checks if the user is logged in by checking if HttpContext.User is set.
 * If a user is not signed in, then it returns a 401-status code.
 */
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /**
     * <summary>
     *     This method is called when authorization is required.
     *     It checks if the user is logged in by checking if HttpContext.User is set.
     *     If a user is not signed in then it returns 401-status code.
     * </summary>
     * <param name="context">The authorization filter context</param>
     */
    public void OnAuthorization(AuthorizationFilterContext context)
{
    var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

    if (allowAnonymous)
    {
        Console.WriteLine(" Skipping authorization");
        return;
    }

    // 1. Prioridad al Middleware: Si UseRequestAuthorization ya validó al usuario y lo metió en Items
    var user = (User?)context.HttpContext.Items["User"];
    if (user != null) return; // Autorizado

    // 2. Respaldo: Si no está en Items pero la identidad nativa de .NET dice que está autenticado
    if (context.HttpContext.User?.Identity?.IsAuthenticated == true) return; // Autorizado

    // 3. Si no pasó ninguna de las dos barreras, se bloquea con 401
    context.Result = new UnauthorizedResult();
    }
}