using neurozen.API.Appointments.Domain.Model.Commands;
using neurozen.API.Appointments.Domain.Model.ValueObjects;

namespace neurozen.API.Appointments.Domain.Model.Aggregates;

public partial class Appointment
{
    protected Appointment()
    {
        PatientId = Guid.Empty;
        ProfessionalId = Guid.Empty;
        AppointmentDateTime = DateTime.MinValue;
        AppointmentType = EAppointmentType.ConsultaInicial;
        Notas_Adicionales = string.Empty;
    }

    public Appointment(CreateAppointmentCommand command)
    {
        PatientId = command.PatientId;
        ProfessionalId = command.ProfessionalId;
        AppointmentDateTime = command.AppointmentDate;
        AppointmentType = command.AppointmentType;
        Notas_Adicionales = command.NotasAdicionales ?? string.Empty;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid PatientId { get; private set; }
    public Guid ProfessionalId { get; private set; }
    public DateTime AppointmentDateTime { get; private set; }
    public EAppointmentType AppointmentType { get; private set; }
    public string Notas_Adicionales { get; private set; }

    /// <summary>
    /// Obtiene el tipo de cita como Value Object con toda su información
    /// </summary>
    public AppointmentType GetAppointmentTypeInfo() => ValueObjects.AppointmentType.FromType(AppointmentType);

}