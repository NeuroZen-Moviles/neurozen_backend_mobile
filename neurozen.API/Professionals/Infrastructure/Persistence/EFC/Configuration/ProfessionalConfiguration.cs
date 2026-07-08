using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neurozen.API.Professionals.Domain.Model.Aggregates;
using neurozen.API.UserManagement.Domain.Entities;

namespace neurozen.API.Professionals.Infrastructure.Persistence.EFC.Configuration;

public class ProfessionalConfiguration : IEntityTypeConfiguration<Professional>
{
    public void Configure(EntityTypeBuilder<Professional> builder)
    {
        builder.ToTable("Professionals");

        // Primary key
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .HasColumnType("char(36)")
            .ValueGeneratedNever();

        // User Id
        builder.Property(p => p.UserId)
            .IsRequired()
            .HasColumnName("UserId")
            .HasColumnType("char(36)");

        builder.HasIndex(p => p.UserId)
            .IsUnique();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);

        // Name
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Name");

        // Email
        builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Email");

        // Specialty
        builder.Property(p => p.Specialty)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Specialty");

        // Experience
        builder.Property(p => p.Experience)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Experience");

        // Rating
        builder.Property(p => p.Rating)
            .HasColumnName("Rating");

        // Reviews
        builder.Property(p => p.Reviews)
            .HasColumnName("Reviews");

        // Price
        builder.Property(p => p.Price)
            .HasColumnName("Price");

        // Availability
        builder.Property(p => p.Availability)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("Availability");

        // Bio
        builder.Property(p => p.Bio)
            .IsRequired()
            .HasColumnName("Bio");

        // Image
        builder.Property(p => p.Image)
            .IsRequired()
            .HasColumnName("Image");
    }
}