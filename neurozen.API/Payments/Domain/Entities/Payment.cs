using System;
using neurozen.API.Sales.Domain.Entities;

namespace neurozen.API.Payments.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid? SubscriptionId { get; set; }

        public int PlanId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PEN";
        public string Status { get; set; } = "exitoso";
        public string? CardLast4 { get; set; }
        public string? CardBrand { get; set; }
        public string? Provider { get; set; }
        public string? ProviderRef { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
