using neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

namespace neurozen.API.ResourcesLibrary.Application.Internal.QueryServices;

/**
 * <summary>
 *     Meditation Query Service
 * </summary>
 */
public class MeditationQueryService
{
    /**
     * <summary>
     *     Get all meditation sessions
     * </summary>
     */
    public Task<List<MeditationSessionResource>> GetAllMeditationSessions()
    {
        var sessions = new List<MeditationSessionResource>
        {
            new(
                Id: 1,
                Title: "Morning Meditation",
                Description: "Start your day with peace and mindfulness. Perfect for beginners looking to establish a daily meditation practice.",
                DurationMinutes: 10,
                ImageUrl: "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500&h=500&fit=crop",
                AudioUrl: "https://example.com/audio/morning-meditation.mp3",
                Category: "Beginner",
                DifficultyLevel: 1.0,
                ViewCount: 1250
            ),
            new(
                Id: 2,
                Title: "Stress Relief",
                Description: "Release tension and anxiety with this guided meditation. Excellent for managing daily stress.",
                DurationMinutes: 15,
                ImageUrl: "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500&h=500&fit=crop",
                AudioUrl: "https://example.com/audio/stress-relief.mp3",
                Category: "Intermediate",
                DifficultyLevel: 2.0,
                ViewCount: 850
            ),
            new(
                Id: 3,
                Title: "Sleep Meditation",
                Description: "Prepare for restful sleep with this calming meditation. Helps with insomnia and sleep quality.",
                DurationMinutes: 20,
                ImageUrl: "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500&h=500&fit=crop",
                AudioUrl: "https://example.com/audio/sleep-meditation.mp3",
                Category: "Advanced",
                DifficultyLevel: 3.0,
                ViewCount: 2100
            ),
            new(
                Id: 4,
                Title: "Body Scan Meditation",
                Description: "Develop awareness of your body and release physical tension.",
                DurationMinutes: 25,
                ImageUrl: "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500&h=500&fit=crop",
                AudioUrl: "https://example.com/audio/body-scan.mp3",
                Category: "Intermediate",
                DifficultyLevel: 2.5,
                ViewCount: 650
            ),
            new(
                Id: 5,
                Title: "Deep Relaxation",
                Description: "A deeply relaxing session for complete restoration and rejuvenation.",
                DurationMinutes: 30,
                ImageUrl: "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500&h=500&fit=crop",
                AudioUrl: "https://example.com/audio/deep-relaxation.mp3",
                Category: "Advanced",
                DifficultyLevel: 3.5,
                ViewCount: 1800
            )
        };

        return Task.FromResult(sessions);
    }

    /**
     * <summary>
     *     Get meditation sessions by category
     * </summary>
     */
    public Task<List<MeditationSessionResource>> GetMeditationsByCategory(string category)
    {
        var allSessions = GetAllMeditationSessions().Result;
        var filtered = allSessions.Where(s => s.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        return Task.FromResult(filtered);
    }
}
