using Common.Logging;
using Contract.Common.Interfaces;
using Customer.Api.Controllers;
using Customer.Api.Data;
using Customer.Api.Repositories;
using Customer.Api.Repositories.Interfaces;
using Customer.Api.Services;
using Customer.Api.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Customer Api");

try
{
	// Add services to the container.

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
	builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(connectionString));
	builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
		.AddScoped(typeof(IRepositoryBase<,,>), typeof(RepositoryBase<,,>))
		.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
		.AddScoped<ICustomerService, CustomerService>();


	var app = builder.Build();

	app.MapGet("/", () => "Welcome to Customer Api!");

	app.MapCustomerApi();


	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

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
