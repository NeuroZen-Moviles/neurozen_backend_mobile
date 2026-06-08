using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;
using neurozen.API.Shared.Interfaces.REST.Resources;

namespace neurozen.API.Shared.Interfaces.REST.Resources;

/**
 * <summary>
 *     Dashboard Resource (DTO) - Contains all data for the home screen
 * </summary>
 */
public record DashboardResource(
    string UserName,
    HealthMetricsResource HealthMetrics,
    AppointmentSummaryResource? NextAppointment,
    List<MeditationSessionResource> RecommendedSessions
);

/**
 * <summary>
 *     Appointment Summary for Dashboard
 * </summary>
 */
public record AppointmentSummaryResource(
    int AppointmentId,
    DateTime AppointmentDate,
    string ProfessionalName,
    string ProfessionalSpecialty
);
