using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Megarender.DataAccess.Extensions
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static async Task RetryOnExceptionAsync(this IAPIContext context, int times, Func<Task> operation)
        {
            await context.RetryOnExceptionAsync<Exception>(times, operation);
        }

        public static async Task RetryOnExceptionAsync<TException>(this IAPIContext context, int times, Func<Task> operation) where TException : Exception
        {
            if (times <= 0) 
                throw new ArgumentOutOfRangeException(nameof(times));


            var DelayPerAttemptInSeconds = new int[]
            {
                (int) TimeSpan.FromSeconds(2).TotalSeconds,
                (int) TimeSpan.FromSeconds(30).TotalSeconds,
                (int) TimeSpan.FromMinutes(2).TotalSeconds,
                (int) TimeSpan.FromMinutes(10).TotalSeconds,
                (int) TimeSpan.FromMinutes(30).TotalSeconds
            };

            var attempts = 0;
            do
            {
                try
                {
                    attempts++;
                    await operation();
                    break;
                }
                catch (TException ex)
                {
                    if (attempts == times)
                        throw;
                    
                    var delay = attempts > DelayPerAttemptInSeconds.Length ? DelayPerAttemptInSeconds.Last() : DelayPerAttemptInSeconds[attempts];
                    await Task.Delay(delay);
                }
            } while (true);
        }
    }
}