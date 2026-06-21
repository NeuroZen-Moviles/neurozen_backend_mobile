using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using neurozen.API.Payments.Domain.Entities;
using neurozen.API.Payments.Domain.Repositories;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using neurozen.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace neurozen.API.Payments.Infrastructure.Repositories
{
    public class PaymentRepository(AppDbContext context)
        : BaseRepository<Payment>(context), IPaymentRepository
    {
        public async Task<IEnumerable<Payment>> GetAllPaymentsForUserId(Guid userId)
        {
            return await Context.Set<Payment>()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}