using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neurozen.API.Payments.Domain.Entities;
using neurozen.API.Payments.Domain.Models.Queries;
using neurozen.API.Payments.Domain.Repositories;
using neurozen.API.Shared.Domain.Repositories;

namespace neurozen.API.Payments.Domain.Services
{
    public interface IPaymentQueryService
    {   
        Task<IEnumerable<Payment>> Handle(GetAllPaymentsForUserIdQuery query);
    }
}