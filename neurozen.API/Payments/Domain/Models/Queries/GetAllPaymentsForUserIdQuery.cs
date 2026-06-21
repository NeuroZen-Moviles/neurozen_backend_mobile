using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neurozen.API.Payments.Domain.Models.Queries
{
    public record GetAllPaymentsForUserIdQuery(Guid UserId);
}