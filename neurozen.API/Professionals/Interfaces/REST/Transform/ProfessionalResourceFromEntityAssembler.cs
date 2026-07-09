using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Interfaces.REST.Resources;

namespace neurozen.API.Professionals.Interfaces.REST.Transform;

public class ProfessionalResourceFromEntityAssembler
{
  public static ProfessionalResource ToResourceFromEntity(Professional entity) => new(
    entity.Id,
    entity.Name,
    entity.Email,
    entity.Specialty,
    entity.Experience,
    (double)entity.Rating,
    5, // YearsOfExperience - default value for now
    entity.Reviews,
    entity.Price,
    entity.Availability,
    entity.Bio,
    entity.Image
  );
}