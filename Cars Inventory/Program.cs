
using Cars_Inventory.Logger;
using CarsInventory.BusinessLogicLayer.Services;
using CarsInventory.BusinessLogicLayer.Services.Contracts;
using CarsInventory.DataAccessLayer.Repositories;
using CarsInventory.DataAccessLayer.Repositories.Contracts;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using ILogger = Cars_Inventory.Logger.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
XmlConfigurator.Configure(new FileInfo("C:\\Users\\Ayush.g\\Desktop\\Project\\Cars Inventory\\Logger\\log4net.config"));
builder.Services.AddDbContext<DatabaseContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseManagement")));

builder.Services.AddLogging();
builder.Logging.ClearProviders(); 

JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.IgnoreCycles,
    WriteIndented = true
};
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cars Inventory", Version = "v1" });
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICarsRepository, CarsRepository>();
builder.Services.AddScoped<ICarService, CarsService>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILogger, LoggerLog4net>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
