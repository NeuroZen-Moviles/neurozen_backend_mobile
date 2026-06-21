﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Payments.Domain.Entities;

namespace neurozen.API.Payments.Infrastructure.Persistence.EFC.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("payments");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("char(36)").ValueGeneratedNever();
            builder.Property(p => p.UserId).IsRequired().HasColumnType("char(36)");
            builder.Property(p => p.SubscriptionId).HasColumnType("char(36)");
            builder.Property(p => p.PlanId).IsRequired();
            builder.Property(p => p.Amount).HasColumnType("numeric(12,2)");
            builder.Property(p => p.Currency).HasMaxLength(10);
            builder.Property(p => p.Status).HasMaxLength(50);
            builder.Property(p => p.Provider).HasMaxLength(100);
            builder.Property(p => p.ProviderRef).HasMaxLength(255);
            builder.Property(p => p.CardLast4).HasMaxLength(4);
            builder.Property(p => p.CardBrand).HasMaxLength(50);
            builder.Property(p => p.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasIndex(p => p.UserId).HasDatabaseName("IX_payments_UserId");
        }
    }
}
