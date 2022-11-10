using AutoAuction.Service;
using CarAuction;
using CarAuction.Authorization;
using CarAuction.Authorization.Policy;
using CarAuction.Entites;
using CarAuction.Middleware;
using CarAuction.Models.DTO;
using CarAuction.Models.Validators;
using CarAuction.Seeder;
using CarAuction.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using UniAPI.Services;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder();

    
    builder.Services.AddControllersWithViews();
    builder.Logging.ClearProviders();

    //Nlog
    builder.Host.UseNLog();

    //services.AddTransient<>();           
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

    builder.Services.AddAuthorization(options => {
        options.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "German", "Polish"));
        options.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirment(20)));
        options.AddPolicy("CreatedAtleast2Univeristy", builder => builder.AddRequirements(new CreateMultipleUniversityRequirment(2)));

    });

    //RequirmentHandler
    builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirmentHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, CreateMultipleUniversityRequirmentHandler>();

    //Validator
    //Obsolete
    //builder.Services.AddControllers().AddFluentValidation();

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddFluentValidationClientsideAdapters();

    //Sedder
    builder.Services.AddScoped<AuctionSeeder>();

    //Interface
    builder.Services.AddScoped<IAccountServices, AccountServices>();
    builder.Services.AddScoped<IVehicleService, VehicleService>();
    builder.Services.AddScoped<IUserContextService, UserContextService>();

    //Middleware
    builder.Services.AddScoped<ErrorHandlingMiddleware>();
    builder.Services.AddScoped<RequestTimeMiddleware>();

    //Hasser
    builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    //Validetor
    builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

    //ContextAccessor
    builder.Services.AddHttpContextAccessor();

    //Swagger
    builder.Services.AddSwaggerGen();

    //DbContext
    builder.Services.AddDbContext<AuctionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    var app = builder.Build();

    var scope = app.Services.CreateScope();

    var seeder = scope.ServiceProvider.GetRequiredService<AuctionSeeder>();

    app.UseResponseCaching();

    app.UseStaticFiles();

    app.UseCors("FrontEndClient");

    seeder.Seed();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeMiddleware>();

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseSwagger();

    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0"));

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