using Microsoft.EntityFrameworkCore;
using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.Professionals.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace neurozen.API.Professionals.Infrastructure.Repositories;

public class ProfessionalRepository(AppDbContext context) : BaseRepository<Professional>(context), IProfessionalRepository
{
    public async Task<IEnumerable<Professional>> GetAllProfessionals()
    {
        return await context.Set<Professional>().ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await context.Set<Professional>().AnyAsync(p => p.Id == id);
    }
}