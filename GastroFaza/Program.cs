using FluentValidation;
using FluentValidation.AspNetCore;
using GastroFaza;
using GastroFaza.Middleware;
using GastroFaza.Models;
using GastroFaza.Models.DTO;
using GastroFaza.Models.Validators;
using GastroFaza.Seeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder();
    
    builder.Services.AddControllersWithViews();

    builder.Services.AddSession();

    builder.Logging.ClearProviders();

    //Nlog
    builder.Host.UseNLog();
        
    var authenticationSettings = new AuthenticationSettings();

    builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

    //Autoorization
    builder.Services.AddSingleton(authenticationSettings);

    builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
        };
    });


    //Validator
    //Obsolete
    //builder.Services.AddControllers().AddFluentValidation();

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    //Sedder
    builder.Services.AddScoped<RestaurantSeeder>();

    //Middleware
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<RequestTimeMiddleware>();

    //Hasser
    builder.Services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();
    builder.Services.AddScoped<IPasswordHasher<Worker>, PasswordHasher<Worker>>();

    //Validator
    builder.Services.AddScoped<IValidator<RegisterClientDto>, RegisterClientDtoValidator>();
    builder.Services.AddScoped<IValidator<RegisterWorkerDto>, RegisterWorkerDtoValidator>();
    builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
    builder.Services.AddScoped<IValidator<EditClientDto>, EditDtoValidator>();

    //ContextAccessor
    builder.Services.AddHttpContextAccessor();

    //Razor
    builder.Services.AddRazorPages();

    //DbContext
    builder.Services.AddDbContext<RestaurantDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();

    app.UseSession();

    var scope = app.Services.CreateScope();

    var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();

    app.UseResponseCaching();

    app.UseStaticFiles();

    app.UseCors("FrontEndClient");

    seeder.Seed();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeMiddleware>();

    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}