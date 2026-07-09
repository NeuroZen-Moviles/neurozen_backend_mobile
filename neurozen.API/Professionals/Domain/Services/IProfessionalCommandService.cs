using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Model.Commands;
using neurozen.API.Professionals.Domain.Model.Results;

namespace neurozen.API.Professionals.Domain.Services;

public interface IProfessionalCommandService
{
  Task<ProfessionalCreationResult> Handle(CreateProfessionalCommand command);
}