namespace neurozen.API.ResourcesLibrary.Interfaces.REST.Resources;

/**
 * <summary>
 *     Meditation Session Resource (DTO)
 * </summary>
 */
public record MeditationSessionResource(
    int Id,
    string Title,
    string Description,
    int DurationMinutes,
    string ImageUrl,
    string AudioUrl,
    string Category,
    double DifficultyLevel,
    int ViewCount
);
