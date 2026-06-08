using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using neurozen.API.Shared.Application.Internal.QueryServices;
using neurozen.API.Shared.Interfaces.REST.Resources;
using neurozen.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace neurozen.API.Shared.Interfaces.REST;

[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("User Dashboard endpoints")]
public class DashboardController(DashboardQueryService dashboardQueryService) : ControllerBase
{
    /**
     * <summary>
     *     Get user dashboard
     * </summary>
     * <param name="userId">The user ID</param>
     * <returns>Dashboard data including health metrics, next appointment, and recommended sessions</returns>
     */
    [HttpGet("{userId:guid}/dashboard")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Get Dashboard",
        Description = "Get dashboard data for a user including health metrics and meditation recommendations",
        OperationId = "GetDashboard")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dashboard data retrieved successfully", typeof(DashboardResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetDashboard(Guid userId)
    {
        var dashboard = await dashboardQueryService.GetUserDashboard(userId);
        
        if (dashboard == null)
            return NotFound(new { message = "User not found" });

        return Ok(dashboard);
    }
}
