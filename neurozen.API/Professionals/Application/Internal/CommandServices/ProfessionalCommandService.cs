using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Commands;
using neurozen.API.Professionals.Domain.Model.Results;
using neurozen.API.Professionals.Domain.Repositories;
using neurozen.API.Professionals.Domain.Services;
using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.UserManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace neurozen.API.Professionals.Application.Internal.CommandServices;

public class ProfessionalCommandService(
  IProfessionalRepository professionalRepository,
  IUnitOfWork unitOfWork,
  AppDbContext dbContext,
  ILogger<ProfessionalCommandService> logger
) : IProfessionalCommandService
{
  public async Task<ProfessionalCreationResult> Handle(CreateProfessionalCommand command)
  {
    logger.LogInformation(
      "Starting professional creation. Email: {Email}, Name: {Name}, Specialty: {Specialty}, Rating: {Rating}, Reviews: {Reviews}, Price: {Price}",
      command.Email,
      command.Name,
      command.Specialty,
      command.Rating,
      command.Reviews,
      command.Price);

    var user = await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == command.Email);
    if (user is null)
    {
      logger.LogWarning(
        "Cannot create professional profile because no user exists for email {Email}",
        command.Email);
      return ProfessionalCreationResult.NotFound(
        $"No existe un usuario registrado con el email '{command.Email}'. Primero debes crear la cuenta en IAM/sign-up.");
    }

    logger.LogInformation(
      "Found linked user for professional creation. UserId: {UserId}, Email: {Email}, CurrentRole: {Role}",
      user.Id,
      user.Email,
      user.Role);

    if (await professionalRepository.ExistsByIdAsync(user.Id))
    {
      logger.LogWarning(
        "Cannot create professional profile because one already exists for user {UserId} / email {Email}",
        user.Id,
        command.Email);
      return ProfessionalCreationResult.Conflict(
        $"Ya existe un perfil de profesional para el usuario '{command.Email}'.");
    }


    var professional = new Professional(command);
    professional.AssignUserIdAsProfessionalId(user.Id);
    user.Role = "professional";

    try
    {
      logger.LogInformation(
        "Persisting professional entity. ProfessionalId: {ProfessionalId}, LinkedUserId: {UserId}",
        professional.Id,
        user.Id);

      await professionalRepository.AddAsync(professional);
      await unitOfWork.CompleteAsync();

      logger.LogInformation(
        "Professional created successfully. ProfessionalId: {ProfessionalId}, LinkedUserId: {UserId}",
        professional.Id,
        user.Id);

      return ProfessionalCreationResult.Success(professional);
    }
    catch (Exception e)
    {
      logger.LogError(
        e,
        "Error creating professional. Email: {Email}, UserId: {UserId}, ProfessionalId: {ProfessionalId}",
        command.Email,
        user.Id,
        professional.Id);
      return ProfessionalCreationResult.BadRequest(
        "Ocurrió un error inesperado al crear el profesional. Revisa los logs del servidor.");
    }
  }
}