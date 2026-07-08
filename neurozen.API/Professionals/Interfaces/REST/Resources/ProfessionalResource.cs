namespace neurozen.API.Professionals.Interfaces.REST.Resources;

public record ProfessionalResource(
    Guid Id,
    Guid UserId,
    string Name,
    string Email,
    string Specialty,
    string Experience,
    double Rating,
    int YearsOfExperience,
    int Reviews,
    int Price,
    string Availability,
    string Bio,
    string Image
);