namespace neurozen.API.Shared.Interfaces.REST.Resources;

/**
 * <summary>
 *     Health Metrics Resource (DTO)
 * </summary>
 */
public record HealthMetricsResource(
    int StressLevel,
    int HeartRate,
    double SleepHours,
    DateTime RecordedAt
);
