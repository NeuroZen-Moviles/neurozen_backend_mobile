using Microsoft.AspNetCore.Http;
using neurozen.API.Professionals.Domain.Model.Aggregates;

namespace neurozen.API.Professionals.Domain.Model.Results;

public record ProfessionalCreationResult(
  bool Succeeded,
  Professional? Professional,
  string Message,
  int StatusCode)
{
  public static ProfessionalCreationResult Success(Professional professional) =>
    new(true, professional, string.Empty, StatusCodes.Status201Created);

  public static ProfessionalCreationResult NotFound(string message) =>
    new(false, null, message, StatusCodes.Status404NotFound);

  public static ProfessionalCreationResult Conflict(string message) =>
    new(false, null, message, StatusCodes.Status409Conflict);

  public static ProfessionalCreationResult BadRequest(string message) =>
    new(false, null, message, StatusCodes.Status400BadRequest);
}