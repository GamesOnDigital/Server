using BLL.Functions;
using BLL.Interfaces;
using DAL.Functions;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// הגדרת מפתח סודי ארוך יותר
string secretKey = "this_is_a_very_long_secret_key_for_jwt"; // לפחות 32 תווים

builder.Services.AddSingleton(new JwtTokenGenerator(
    key: secretKey,
    issuer: "yourdomain.com",
    audience: "yourdomain.com"));

// הגדרת אימות JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourdomain.com",
        ValidAudience = "yourdomain.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // הוספת הגדרת Authorization ל-Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

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
            new string[] { }
        }
    });
});

builder.Services.AddDbContext<CountdContext>(options => options.UseSqlServer("Server=.;Database=countd;TrustServerCertificate=True;Trusted_Connection=True;"));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped(typeof(IAudienceDal), typeof(AudienceDal));
builder.Services.AddScoped(typeof(IAudienceBll), typeof(AudienceBll));

builder.Services.AddScoped(typeof(IGameDal), typeof(GameDal));
builder.Services.AddScoped(typeof(IGameBll), typeof(GameBll));

builder.Services.AddScoped(typeof(IGenderDal), typeof(GenderDal));
builder.Services.AddScoped(typeof(IGenderBll), typeof(GenderBll));

builder.Services.AddScoped(typeof(IHowKnownDal), typeof(HowKnownDal));
builder.Services.AddScoped(typeof(IHowKnownBll), typeof(HowKnownBll));

builder.Services.AddScoped(typeof(ISettingDal), typeof(SettingDal));
builder.Services.AddScoped(typeof(ISettingsBll), typeof(SettingsBll));

builder.Services.AddScoped(typeof(ITypeGameDal), typeof(TypeGameDal));
builder.Services.AddScoped(typeof(ITypeGameBll), typeof(TypeGameBll));

builder.Services.AddScoped(typeof(IUserDal), typeof(UserDal));
builder.Services.AddScoped(typeof(IUserBll), typeof(UserBll));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
