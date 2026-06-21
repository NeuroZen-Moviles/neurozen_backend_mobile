using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Interfaces.REST.Resources;

public record AppointmentResource(
    Guid Id, Guid PatientId, Guid ProfessionalId, DateTime AppointmentDateTime
    , EAppointmentType AppointmentType, string notasAdicionales);
