﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.UserManagement.Domain.Entities;

namespace neurozen.API.UserManagement.Infrastructure.Persistence.EFC.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", table =>
        {
            table.HasCheckConstraint("CK_users_role_allowed", "`Role` IN ('patient','professional')");
        });
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash).HasMaxLength(255);
        builder.Property(u => u.FullName).HasMaxLength(255);
        builder.Property(u => u.Role).IsRequired().HasMaxLength(20).HasDefaultValue("patient");
        builder.Property(u => u.AvatarUrl).HasColumnType("text");
        builder.Property(u => u.Meta).HasColumnType("json");
        builder.Property(u => u.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(u => u.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");
    }
}
