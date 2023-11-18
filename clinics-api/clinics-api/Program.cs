using clinics_api.Data;
using clinics_api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace clinics_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ClinicsDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ClinicsDbContext>();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}