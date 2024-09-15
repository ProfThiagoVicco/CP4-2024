using System.Reflection;
using CP4.Services.CountryServices;
using Microsoft.OpenApi.Models;

namespace CP4.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Get Countries",
                Version = "v1",
                Description = "API for Get Countries",
                Contact = new OpenApiContact
                {
                    Name = "Thiago Keller",
                    Email = "profthiagovicco@fiap.com.br"
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            x.IncludeXmlComments(xmlPath);
        });

        builder.Services.AddScoped<ICountryService, CountryService>();
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