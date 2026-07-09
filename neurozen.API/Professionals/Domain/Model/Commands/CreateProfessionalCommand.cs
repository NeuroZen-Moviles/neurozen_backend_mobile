namespace neurozen.API.Professionals.Domain.Model.Commands;

public record CreateProfessionalCommand(
    //Guid UserId,
    string Name,
    string Email,
    string Specialty,
    string Experience,
    int Rating,
    int Reviews,
    int Price,
    string Availability,
    string Bio,
    string Image
);