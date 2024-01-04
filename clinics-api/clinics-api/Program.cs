using clinics_api.Data;
using clinics_api.Models;
using clinics_api.Models.Interfaces;
using clinics_api.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using clinics_api.Controllers;

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
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            builder.Services.AddDbContext<ClinicsDbContext>(options => options.UseSqlServer(connString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ClinicsDbContext>();



            builder.Services.AddTransient<IUser, UserService>();
            builder.Services.AddScoped<JwtTokenService>();
            builder.Services.AddTransient<IDepartment, DepartmentService>();
            builder.Services.AddTransient<IDoctor, DoctorService>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationPerameters(builder.Configuration);
            });
            builder.Services.AddAuthorization();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseStaticFiles(); // This enables serving static files from the 'wwwroot' directory

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

                        if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}