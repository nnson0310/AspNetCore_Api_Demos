using AspCoreWebAPIDemos.DBContexts;
using AspCoreWebAPIDemos.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(
    options => options.ReturnHttpNotAcceptable = true
)
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
builder.Services.AddTransient<IMailService, LocalMailService>();
builder.Services.AddDbContext<CityContext>(dbContextOptions => dbContextOptions.UseSqlite("Data Source=City.db"));
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = true;
});

// Add JWT authentication
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"]))
        };
    });

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeFromBangkok", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city", "Bangkok");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    context.Request.Headers["Accept"] = "application/json";
    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
