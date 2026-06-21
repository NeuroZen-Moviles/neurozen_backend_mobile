namespace neurozen.API.ResourcesLibrary.Domain.Model.Aggregates;

/**
 * <summary>
 *     Meditation Session Model
 * </summary>
 */
public class MeditationSession
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int DurationMinutes { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public string AudioUrl { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public double DifficultyLevel { get; set; }

    public int ViewCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public MeditationSession() { }

    public MeditationSession(string title, string description, int durationMinutes, string category, double difficultyLevel)
    {
        Title = title;
        Description = description;
        DurationMinutes = durationMinutes;
        Category = category;
        DifficultyLevel = difficultyLevel;
        CreatedAt = DateTime.UtcNow;
        ViewCount = 0;
    }
}
