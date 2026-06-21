using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace neurozen.API.Payments.Interfaces.REST.Resources
{
    public record PaymentResource
    (
        Guid Id, Guid UserId, Guid? SubscriptionId, int PlanId, decimal Amount,
        string Currency, string Status, string? CardLast4, string? CardBrand, DateTimeOffset CreatedAt
    );
}