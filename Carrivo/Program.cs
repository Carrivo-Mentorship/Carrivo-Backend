using SwaggerThemes;
using System.Reflection;
using TaskManagement.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ﬁ—«¡… „·› «·≈⁄œ«œ«  «·„Õ·Ì ≈–« ÊıÃœ
builder.Configuration.AddJsonFile("appsettings.Development.Local.json", optional: true, reloadOnChange: true);

//  ”ÃÌ· «·Œœ„«  «·„Œ’’… (Configuration)
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

//  ”ÃÌ· Repositories Ê Services (Dependency Injection)
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddControllers();

// Swagger/OpenAPI Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Carrivo API",
        Version = "v1",
        Description = "Educational platform API with authentication"
    });

    // ≈÷«›… ≈⁄œ«œ«  JWT Authentication ≈·Ï Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // «·Õ’Ê· ⁄·Ï „”«— „·› «· ÊÀÌﬁ XML
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// **********************************
// ≈⁄œ«œ CORS ··”„«Õ ·√Ì „’œ— (€Ì— ¬„‰ ··‹ Production)
// **********************************
builder.Services.AddCors(options =>
{
    options.AddPolicy("CarrivoCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin() // <--- Ì”„Õ ·√Ì ‰ÿ«ﬁ »«·Ê’Ê·
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// **********************************


var app = builder.Build();

// **********************************
//  ›⁄Ì· Swagger Ê SwaggerUI ·Ã„Ì⁄ «·»Ì∆«  (»„« ›Ì –·ﬂ Production)
// «· ⁄œÌ·:  ⁄ÌÌ‰ RoutePrefix ›«—€« · ‘€Ì· Swagger ⁄·Ï «·„”«— «·Ã–— (/)
// **********************************
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Carrivo API V1");
    options.RoutePrefix = string.Empty; // <--- Â–« ÂÊ «· ⁄œÌ· «·Â«„! Ì› Õ Swagger ⁄·Ï /
  
});
// **********************************


app.UseHttpsRedirection();

// CORS
app.UseCors("CarrivoCorsPolicy");

// Authentication & Authorization («· — Ì» „Â„!)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();