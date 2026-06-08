using neurozen.API.Shared.Interfaces.REST.Resources;
using neurozen.API.IAM.Domain.Repositories;
using neurozen.API.Appointments.Domain.Repositories;
using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

namespace neurozen.API.Shared.Application.Internal.QueryServices;

/**
 * <summary>
 *     Dashboard Query Service
 * </summary>
 */
public class DashboardQueryService(
    IUserRepository userRepository,
    IAppointmentRepository appointmentRepository)
{
    /**
     * <summary>
     *     Get dashboard data for a user
     * </summary>
     */
    public async Task<DashboardResource?> GetUserDashboard(Guid userId)
    {
        // Get user
        var user = await userRepository.FindByIdAsync(userId);
        if (user == null) return null;

        // Get health metrics (for now, sample data)
        var healthMetrics = new HealthMetricsResource(
            StressLevel: 35,
            HeartRate: 72,
            SleepHours: 7.5,
            RecordedAt: DateTime.UtcNow
        );

        // Get next appointment
        AppointmentSummaryResource? nextAppointment = null;
        // TODO: Implement appointment fetching with professional data

        // Get recommended meditation sessions
        var recommendedSessions = new List<MeditationSessionResource>
        {
            new(
                Id: 1,
                Title: "Morning Meditation",
                Description: "Start your day with peace",
                DurationMinutes: 10,
                ImageUrl: "https://example.com/image1.jpg",
                AudioUrl: "https://example.com/audio1.mp3",
                Category: "Beginner",
                DifficultyLevel: 1.0,
                ViewCount: 1250
            ),
            new(
                Id: 2,
                Title: "Stress Relief",
                Description: "Release tension and anxiety",
                DurationMinutes: 15,
                ImageUrl: "https://example.com/image2.jpg",
                AudioUrl: "https://example.com/audio2.mp3",
                Category: "Intermediate",
                DifficultyLevel: 2.0,
                ViewCount: 850
            ),
            new(
                Id: 3,
                Title: "Sleep Meditation",
                Description: "Prepare for restful sleep",
                DurationMinutes: 20,
                ImageUrl: "https://example.com/image3.jpg",
                AudioUrl: "https://example.com/audio3.mp3",
                Category: "Advanced",
                DifficultyLevel: 3.0,
                ViewCount: 2100
            )
        };

        return new DashboardResource(
            UserName: user.Username,
            HealthMetrics: healthMetrics,
            NextAppointment: nextAppointment,
            RecommendedSessions: recommendedSessions
        );
    }
}
