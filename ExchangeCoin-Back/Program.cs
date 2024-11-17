using ExchangeCoin_Back.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text;
using ExchangeCoin_Back.Data;

namespace ExchangeCoin_Back
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(setupAction =>
            {
                setupAction.AddSecurityDefinition("ExchangeCoinApiBearerAuth", new OpenApiSecurityScheme() 
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Description = "Ac� pegar el token generado al loguearse."
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ExchangeCoinApiBearerAuth" } 
                            }, new List<string>() }
                });
            });

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<CoinService>();
            builder.Services.AddDbContext<ExchangeContext>(dbContextOptions => dbContextOptions.UseSqlite(
                builder.Configuration["ConnectionStrings:Exchange-Coin-DBConnectionString"]));

            builder.Services.AddAuthentication("Bearer") 
             .AddJwtBearer(options => 
             {
                 options.TokenValidationParameters = new()
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Authentication:Issuer"],
                     ValidAudience = builder.Configuration["Authentication:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                 };
             }
 );


            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(
            options => options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
            );

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }

}