using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neurozen.API.Payments.Domain.Entities;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Payments.Domain.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetAllPaymentsForUserId(Guid UserId);
    }
}