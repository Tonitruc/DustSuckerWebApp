using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace DustSuckerWebApp.DataLayer
{
    public static class DbContextValidationHelper
    {
        public static async Task<ImmutableList<ValidationResult>> SaveChangeWithValidationAsync(this DbContext context)
        {
            var result = ExecuteValidation(context);
            if (!result.IsEmpty) return result;

            context.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            finally
            {
                context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
            return result;
        }

        public static ImmutableList<ValidationResult> SaveChangeWithValidation(this DbContext context)
        {
            var result = ExecuteValidation(context);
            if (!result.IsEmpty) return result;

            context.ChangeTracker.AutoDetectChangesEnabled = false; 
            try
            {
                context.SaveChanges(); 
            }
            finally
            {
                context.ChangeTracker.AutoDetectChangesEnabled = true;        
            }
            return result; 
        }

        public static ImmutableList<ValidationResult> ExecuteValidation(this DbContext context)
        {
            var result = new List<ValidationResult>();
            foreach(var entry 
                in context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
            {
                var entity = entry.Entity;
                var provider = new ValidationDbContextServiceProvider(context);
                var validationContext = new ValidationContext(entity, provider, null);
                var validationsResult = new List<ValidationResult>();
                if(!Validator.TryValidateObject(entity, validationContext, validationsResult, true))
                {
                    result.AddRange(validationsResult);
                }
            }

            return [.. result];
        }
    }
}
