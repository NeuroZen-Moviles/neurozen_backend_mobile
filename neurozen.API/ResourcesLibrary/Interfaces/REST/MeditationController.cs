using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using neurozen.API.ResourcesLibrary.Application.Internal.QueryServices;
using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;
using neurozen.API.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace neurozen.API.ResourcesLibrary.Interfaces.REST;

[ApiController]
[Route("api/v1/contents")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Meditation and Wellness Content endpoints")]
public class MeditationController(MeditationQueryService meditationQueryService) : ControllerBase
{
    /**
     * <summary>
     *     Get all meditation sessions
     * </summary>
     * <returns>List of all meditation sessions</returns>
     */
    [HttpGet("meditations")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get All Meditations",
        Description = "Get all available meditation sessions with descriptions and audio URLs",
        OperationId = "GetMeditations")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of meditation sessions retrieved successfully", typeof(List<MeditationSessionResource>))]
    public async Task<IActionResult> GetMeditations()
    {
        var sessions = await meditationQueryService.GetAllMeditationSessions();
        return Ok(sessions);
    }

    /**
     * <summary>
     *     Get meditation sessions by category
     * </summary>
     * <param name="category">The category (Beginner, Intermediate, Advanced)</param>
     * <returns>List of meditation sessions in the specified category</returns>
     */
    [HttpGet("meditations/category/{category}")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Get Meditations by Category",
        Description = "Get meditation sessions filtered by difficulty category",
        OperationId = "GetMeditationsByCategory")]
    [SwaggerResponse(StatusCodes.Status200OK, "Filtered meditation sessions retrieved successfully", typeof(List<MeditationSessionResource>))]
    public async Task<IActionResult> GetMeditationsByCategory(string category)
    {
        var sessions = await meditationQueryService.GetMeditationsByCategory(category);
        return Ok(sessions);
    }
}
