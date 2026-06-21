using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using neurozen.API.Payments.Domain.Entities;
using neurozen.API.Payments.Interfaces.REST.Resources;

namespace neurozen.API.Payments.Interfaces.REST.Transform
{
    public class PaymentResourceFromEntityAssembler
    {
        public static PaymentResource ToResourceFromEntity(Payment e) => new PaymentResource(
            e.Id, e.UserId, e.SubscriptionId, e.PlanId, e.Amount,
        e.Currency, e.Status, e.CardLast4, e.CardBrand, e.CreatedAt);
    }
}