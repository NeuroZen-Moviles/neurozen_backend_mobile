namespace neurozen.API.Professionals.Interfaces.REST.Resources;

public record CreateProfessionalResource(Guid UserId, string Name, string Email, string Specialty, string Experience, int Rating, int Reviews, int Price, string Availability, string Bio, string Image);