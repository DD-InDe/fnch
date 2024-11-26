using EAS_Hub.DbModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            In = ParameterLocation.Header,
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<EasFullDbContext>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Accessor", policy => policy.RequireRole("Accessor"));
    options.AddPolicy("Developer", policy => policy.RequireRole("Developer"));
    options.AddPolicy("Staff", policy => policy.RequireRole("Developer", "Accessor"));
});
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options => options.TokenValidationParameters = new()
        {
            ValidAudience = "user",
            ValidIssuer = "host",
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey("QWERTY-QWERTY-QWERTY-QWERTY-QWERTY"u8.ToArray())
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();


app.Run();