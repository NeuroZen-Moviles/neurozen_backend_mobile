using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Professionals.Domain.Repositories;

public interface IProfessionalRepository : IBaseRepository<Professional>
{
    /// <summary>
    ///     Get all professionals
    /// </summary>
    /// <returns>An enumerable with all professionals</returns>
    Task<IEnumerable<Professional>> GetAllProfessionals();

    /// <summary>
    ///     Checks whether a professional profile already exists for the given user id.
    /// </summary>
    /// <param name="userId">The user id to check.</param>
    /// <returns>True if a profile exists, otherwise false.</returns>
    Task<bool> ExistsByUserIdAsync(Guid userId);
}