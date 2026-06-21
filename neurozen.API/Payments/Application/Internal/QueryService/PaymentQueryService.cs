using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neurozen.API.Payments.Domain.Entities;
using neurozen.API.Payments.Domain.Models.Queries;
using neurozen.API.Payments.Domain.Repositories;
using neurozen.API.Payments.Domain.Services;

namespace neurozen.API.Payments.Application.Internal.QueryService
{
    public class PaymentQueryService(IPaymentRepository paymentRepository) : IPaymentQueryService
    {
        public async Task<IEnumerable<Payment>> Handle(GetAllPaymentsForUserIdQuery query)
        {
            return await paymentRepository.GetAllPaymentsForUserId(query.UserId);
        }
    }
}