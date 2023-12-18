using Application;  // Importera nödvändig namespace för Application-projektet
using Infrastructure;  // Importera nödvändig namespace för Infrastructure-projektet
using Microsoft.AspNetCore.Authentication.JwtBearer;  // Importera nödvändig namespace för JWT-baserad autentisering
using Microsoft.IdentityModel.Tokens;  // Importera nödvändig namespace för hantering av JWT-token
using Microsoft.OpenApi.Models;  // Importera nödvändig namespace för Swagger/OpenAPI
using System.Text;  // Importera nödvändig namespace för hantering av text och teckenkoder

var builder = WebApplication.CreateBuilder(args);  // Skapa en ny instans av WebApplicationBuilder

ConfigureServices(builder.Services, builder.Configuration);  // Konfigurera tjänster med hjälp av metoden ConfigureServices
ConfigureAuthentication(builder.Services, builder.Configuration);  // Konfigurera autentisering med hjälp av metoden ConfigureAuthentication
ConfigureSwagger(builder.Services);  // Konfigurera Swagger med hjälp av metoden ConfigureSwagger

var app = builder.Build();  // Bygg en instans av WebApplication

app.UseHttpsRedirection();  // Omdirigera HTTP-förfrågningar till HTTPS
app.UseRouting();  // Aktivera routing
app.UseAuthentication();  // Aktivera autentisering
app.UseAuthorization();  // Aktivera behörighetshantering

app.MapControllers();  // Kartlägg kontroller för hantering av HTTP-förfrågningar

app.UseSwagger();  // Aktivera Swagger
app.UseSwaggerUI();  // Aktivera Swagger UI

app.Run();  // Starta applikationen


// Metoder för att strukturera konfigurationer
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();  // Lägg till tjänster för MVC-controllers
    services.AddApplication().AddInfrastructure();  // Lägg till applikationens och infrastrukturens tjänster
    services.AddEndpointsApiExplorer();  // Lägg till tjänster för API Explorer
}

void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("JwtSettings");  // Hämta JWT-konfigurationsinställningar från appsettings.json
    var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);  // Konvertera hemligt nyckel-strängen till byte-array

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  // Konfigurera autentisering med JWT-bärare
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters  // Konfigurera JWT-tokenvalideringsparametrar
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
}

void ConfigureSwagger(IServiceCollection services)
{
    services.AddSwaggerGen(c =>  // Konfigurera Swagger-generering
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean-Api", Version = "v1" });  // Ange API-dokumentinformation

        // Lägg till säkerhetsschemat för JWT
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });

        // Lägg till globalt säkerhetskrav för JWT
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}
