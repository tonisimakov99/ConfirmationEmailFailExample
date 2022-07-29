using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace TestConsole
{
    public class TestIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Создавет контекст
        /// </summary>
        /// <param name="options"></param>
        public TestIdentityDbContext(DbContextOptions<TestIdentityDbContext> options)
          : base(options)
        {

        }
    }
    public class Program
    {
        public static void Main()
        {
            Start().Wait();
            Console.WriteLine();
        }
        private static async Task Start()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
               {
                   options.User.RequireUniqueEmail = true;
                   options.Password.RequireNonAlphanumeric = false;
               }).AddDefaultTokenProviders()
               .AddEntityFrameworkStores<TestIdentityDbContext>();
             services.AddScoped(t =>
                {
                    var connection = new SqliteConnection("Filename=:memory:");
                    connection.Open();
                    var contextOptions = new DbContextOptionsBuilder<TestIdentityDbContext>()
                        .UseSqlite(connection)
                        .Options;
                    var dbContext = new TestIdentityDbContext(contextOptions);
                    dbContext.Database.EnsureCreated();
                    return dbContext;
                });
            var provider = services.BuildServiceProvider();
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            await userManager.CreateAsync(new IdentityUser("userName")
            {
                Email = "userName@test.ru",
                PhoneNumberConfirmed = false,
                EmailConfirmed = false
            });
            var user = await userManager.FindByEmailAsync("userName@test.ru");

            var emailToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var phoneToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, "88888888888");
            var phoneChangeResult = await userManager.ChangePhoneNumberAsync(user, "88888888888", phoneToken);
            var emailConfirmResult = await userManager.ConfirmEmailAsync(user, emailToken);
            Console.WriteLine(phoneChangeResult);
            Console.WriteLine(emailConfirmResult);
        }
    }
}