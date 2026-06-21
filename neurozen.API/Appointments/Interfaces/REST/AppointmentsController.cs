using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using neurozen.API.Appointments.Domain.Model.Commands;
using Swashbuckle.AspNetCore.Annotations;
using neurozen.API.Appointments.Domain.Model.Queries;
using neurozen.API.Appointments.Domain.Services;
using neurozen.API.IAM.Domain.Model.Aggregates;
using neurozen.API.Appointments.Interfaces.REST.Resources;
using neurozen.API.Appointments.Interfaces.REST.Transform;
using neurozen.API.Appointments.Domain.Model.ValueObjects;
using neurozen.API.Resources;
using System.Security.Claims;
using neurozen.API.IAM.Domain.Services;
using neurozen.API.IAM.Domain.Model.Queries;

namespace neurozen.API.Appointments.Interfaces.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Appointments")]
public class AppointmentsController(
    IAppointmentCommandService appointmentCommandService,
    IAppointmentQueryService appointmentQueryService,
    IUserQueryService userQueryService,
    IStringLocalizer<SharedResource> _localizer) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new appointment",
        Description = "Creates a new appointment with the provided details."
    )]
    [SwaggerResponse(201, "Appointment created successfully", typeof(AppointmentResource))]
    [SwaggerResponse(400, "Appointment creation failed")]
    public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentResource resource)
    {
        String msg = _localizer.GetString("CreateAppointmentError");

        // 1. Extraemos directamente el valor usando la URL larga exacta que vimos en tu consola
    var sidClaim = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")?.Value
                   ?? HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;

    // 2. Si no lo encuentra, validamos contra nulos
    if (string.IsNullOrEmpty(sidClaim) || !Guid.TryParse(sidClaim, out var patientId))
    {
        return Unauthorized(new { message = "No se pudo recuperar un GUID de paciente válido desde los reclamos de seguridad." });
    }

        if (resource.ProfessionalId == Guid.Empty)
            return BadRequest(new { message = "ProfessionalId must not be empty" });

        var createAppointmentCommand = CreateAppointmentCommandFromResourceAssembler.ToCommandFromResource(resource, patientId);
        var result = await appointmentCommandService.Handle(createAppointmentCommand);

        if (result is null) return BadRequest(new { message = msg });

        return CreatedAtAction(nameof(GetAppointmentById), new { id = result.Id }, AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("appointments/{patientId:guid}")]
    [SwaggerOperation(
        Summary = "Get all appointments by patient ID",
        Description = "Retrieves all appointments associated with a specific patient ID."
    )]
    [SwaggerResponse(200, "Appointments retrieved successfully", typeof(IEnumerable<AppointmentResource>))]
    [SwaggerResponse(400, "Appointments retrieval failed")]
    public async Task<ActionResult<IEnumerable<AppointmentResource>>> GetAllAppointmentsByPatientIdAsync(Guid patientId)
    {
        String msg = _localizer.GetString("GetAppointmentsbyPatientIdError");
        var getAllAppointmentsByPatientIdQuery = new GetAllAppointmentsQueryByPatientId(patientId);
        var result = await appointmentQueryService.Handle(getAllAppointmentsByPatientIdQuery);
        if (result == null || !result.Any()) return NotFound(new { message = msg });
        var appointments = result.Select(AppointmentResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(appointments);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get appointment by ID",
        Description = "Retrieves an appointment by its unique ID.")]
    [SwaggerResponse(200, "Appointment retrieved successfully", typeof(AppointmentResource))]
    [SwaggerResponse(404, "Appointment not found")]
    public async Task<ActionResult> GetAppointmentById(Guid id)
    {
        String msg = _localizer.GetString("GetAppointmentByIdError");
        var getAppointmentByIdQuery = new GetAppointmentByIdQuery(id);
        var result = await appointmentQueryService.Handle(getAppointmentByIdQuery);
        if (result is null) return NotFound(new { message = msg });
        return Ok(AppointmentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }

    [HttpGet("types")]
    [SwaggerOperation(
        Summary = "Get all appointment types",
        Description = "Retrieves all available appointment types with their details (name, description, estimated duration).")]
    [SwaggerResponse(200, "Appointment types retrieved successfully")]
    public ActionResult GetAppointmentTypes()
    {
        var types = AppointmentType.GetAll()
            .Select(t => new
            {
                value = (int)t.Type,
                name = t.Type.ToString(),
                displayName = t.DisplayName,
                description = t.Description,
                estimatedDurationMinutes = t.EstimatedDurationMinutes
            });

        return Ok(types);
    }

    private bool TryGetAuthenticatedUserId(out Guid userId)
    {
        userId = Guid.Empty;
        
        if (HttpContext.Items["User"] is User user && user.Id != Guid.Empty)
        {
            userId = user.Id;
            return true;
        }

        var sidClaim = HttpContext.User.FindFirst(ClaimTypes.Sid)
            //?? HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)
            ?? HttpContext.User.FindFirst("sid");
            //?? HttpContext.User.FindFirst("sub");

        return sidClaim != null && Guid.TryParse(sidClaim.Value, out userId);
    }
}