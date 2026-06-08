namespace neurozen.API.Shared.Domain.Model;

/**
 * <summary>
 *     Health Metrics for a user
 * </summary>
 */
public class HealthMetrics
{
    public int StressLevel { get; set; }
    
    public int HeartRate { get; set; }
    
    public double SleepHours { get; set; }
    
    public DateTime RecordedAt { get; set; }
    
    public HealthMetrics() { }
    
    public HealthMetrics(int stressLevel, int heartRate, double sleepHours)
    {
        StressLevel = stressLevel;
        HeartRate = heartRate;
        SleepHours = sleepHours;
        RecordedAt = DateTime.UtcNow;
    }
}
