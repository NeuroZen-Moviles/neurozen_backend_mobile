using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Commands;
using neurozen.API.Professionals.Domain.Repositories;
using neurozen.API.Professionals.Domain.Services;
using neurozen.API.Shared.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.UserManagement.Domain.Entities;

namespace neurozen.API.Professionals.Application.Internal.CommandServices;

public class ProfessionalCommandService(
  IProfessionalRepository professionalRepository,
  IUnitOfWork unitOfWork,
  AppDbContext dbContext,
  ILogger<ProfessionalCommandService> logger
) : IProfessionalCommandService
{
  public async Task<Professional?> Handle(CreateProfessionalCommand command)
  {
    var user = await dbContext.Set<User>().FindAsync(command.Email);
    if (user is null)
    {
      logger.LogWarning("Cannot create professional profile because email does not exist");
      return null;
    }


    var professional = new Professional(command);
    user.Role = "professional";

    try
    {
      await professionalRepository.AddAsync(professional);
      await unitOfWork.CompleteAsync();
    }
    catch (Exception e)
    {
      logger.LogError(e, "Error creating professional");
      return null;
    }
    return professional;
  }
}