using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Domain.Model.Commands;

public record CreateAppointmentCommand(
    Guid PatientId,
    Guid ProfessionalId,
    DateTime AppointmentDate,
    EAppointmentType AppointmentType,
    string? NotasAdicionales = null);
