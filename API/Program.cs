using Application;  // Importera n�dv�ndig namespace f�r Application-projektet
using Infrastructure;  // Importera n�dv�ndig namespace f�r Infrastructure-projektet
using Microsoft.AspNetCore.Authentication.JwtBearer;  // Importera n�dv�ndig namespace f�r JWT-baserad autentisering
using Microsoft.IdentityModel.Tokens;  // Importera n�dv�ndig namespace f�r hantering av JWT-token
using Microsoft.OpenApi.Models;  // Importera n�dv�ndig namespace f�r Swagger/OpenAPI
using System.Text;  // Importera n�dv�ndig namespace f�r hantering av text och teckenkoder

var builder = WebApplication.CreateBuilder(args);  // Skapa en ny instans av WebApplicationBuilder

ConfigureServices(builder.Services, builder.Configuration);  // Konfigurera tj�nster med hj�lp av metoden ConfigureServices
ConfigureAuthentication(builder.Services, builder.Configuration);  // Konfigurera autentisering med hj�lp av metoden ConfigureAuthentication
ConfigureSwagger(builder.Services);  // Konfigurera Swagger med hj�lp av metoden ConfigureSwagger

var app = builder.Build();  // Bygg en instans av WebApplication

app.UseHttpsRedirection();  // Omdirigera HTTP-f�rfr�gningar till HTTPS
app.UseRouting();  // Aktivera routing
app.UseAuthentication();  // Aktivera autentisering
app.UseAuthorization();  // Aktivera beh�righetshantering

app.MapControllers();  // Kartl�gg kontroller f�r hantering av HTTP-f�rfr�gningar

app.UseSwagger();  // Aktivera Swagger
app.UseSwaggerUI();  // Aktivera Swagger UI

app.Run();  // Starta applikationen


// Metoder f�r att strukturera konfigurationer
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();  // L�gg till tj�nster f�r MVC-controllers
    services.AddApplication().AddInfrastructure();  // L�gg till applikationens och infrastrukturens tj�nster
    services.AddEndpointsApiExplorer();  // L�gg till tj�nster f�r API Explorer
}

void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
{
    var jwtSettings = configuration.GetSection("JwtSettings");  // H�mta JWT-konfigurationsinst�llningar fr�n appsettings.json
    var secretKey = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);  // Konvertera hemligt nyckel-str�ngen till byte-array

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  // Konfigurera autentisering med JWT-b�rare
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

        // L�gg till s�kerhetsschemat f�r JWT
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        });

        // L�gg till globalt s�kerhetskrav f�r JWT
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
