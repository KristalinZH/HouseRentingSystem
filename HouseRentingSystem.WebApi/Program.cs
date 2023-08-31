namespace HouseRentingSystem.WebApi
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Services.Data.Interfaces;
    using Web.Infrastructure.Extensions;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<HouseRentingDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddAplicationServices(typeof(IHouseService));
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("HouseRentingSystem", policyBuilder =>
                {
                    policyBuilder
                    .WithOrigins("https://localhost:7180")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("HouseRentingSystem");

            app.Run();
        }
    }
}