
using AuthenticationApi.Infrastructure;
using AuthenticationClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            // User
            builder.Services.AddScoped<IUserServiceAsync, UserService>();

            // Customer
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToList();

                    var response = new
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            // Account
            builder.Services.AddScoped<IAccountsRepository<Account, long>, AccountsRepository>();

            // Transaction
            builder.Services.AddScoped<ITransactionRepository<Transaction, long>, TransactionRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMiddleware<JwtMiddleware>();
            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
