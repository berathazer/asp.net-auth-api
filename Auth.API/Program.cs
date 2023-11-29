using Auth.Business.Abstract;
using Auth.Business.Concrete;
using Auth.DataAccess.Abstract;
using Auth.DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IConfiguration ekleniyor
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



//builder.Services.AddSingleton<IConfiguration,builder.Configuration>();
builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();

//jwt 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var configurationSection = builder.Configuration.GetSection("Jwt");

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationSection["Secret"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(30) // 30 dakikalýk jwt
    };
});


//swaggerde jwt girilmesi için
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.EnableAnnotations();
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Sadece JWT tokeni baþýnda Bearer olmadan yazýn.",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    swagger.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
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
app.UseAuthorization();


app.MapControllers();

app.Run();
