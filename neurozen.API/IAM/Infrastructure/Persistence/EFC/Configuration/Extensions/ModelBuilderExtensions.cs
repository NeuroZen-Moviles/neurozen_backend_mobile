using Microsoft.EntityFrameworkCore;
using neurozen.API.IAM.Domain.Model.Aggregates;

namespace neurozen.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context - User entity is configured in UserManagement bounded context
        // Ignore IAM User entity to avoid conflicts with UserManagement.User mapped to 'users' table
        builder.Ignore<User>();
    }
}