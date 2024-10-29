using Common.Logging;
using Contract.Common.Interfaces;
using Customer.Api;
using Customer.Api.Controllers;
using Customer.Api.Data;
using Customer.Api.Repositories;
using Customer.Api.Repositories.Interfaces;
using Customer.Api.Services;
using Customer.Api.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;


Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName}");

try
{
	// Add services to the container.

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	builder.Services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));

	var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
	builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
	builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
		.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
		.AddScoped<ICustomerService, CustomerService>();


	var app = builder.Build();

	app.MapGet("/", () => "Welcome to Customer Api!");

	app.MapCustomerApi();

	app.UseSwagger();

	app.UseSwaggerUI(config =>
	{
		config.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1");
	});

	//app.UseHttpsRedirection(); //production

	app.UseAuthorization();

	app.MapControllers();

	app.SeedCustomer();

	app.Run();
}
catch (Exception ex)
{
	string type = ex.GetType().Name;
	if (type.Equals("StopTheHostException", StringComparison.Ordinal))
	{
		throw;
	}
	Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
	Log.Information("Shut down Customer Api");
	Log.CloseAndFlush();
}
