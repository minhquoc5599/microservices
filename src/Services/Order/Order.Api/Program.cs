using Common.Logging;
using Order.Application;
using Order.Infrastructure.Data;
using Order.Infrastructure.Services;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);


Log.Information($"Start {builder.Environment.ApplicationName}");

try
{
	// Add services to the container.
	builder.Services.AddApplicationServices();
	builder.Services.AddInfrastructure(builder.Configuration);
	builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

	builder.Services.AddControllers();
	// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	using (var scope = app.Services.CreateScope())
	{
		var orderSeeder = scope.ServiceProvider.GetRequiredService<OrderSeeder>();
		await orderSeeder.InitializeAsync();
		await orderSeeder.SeedOrderAsync();
	}

	//app.UseHttpsRedirection();

	app.UseAuthorization();

	app.MapDefaultControllerRoute();

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
	Log.Information("Shut down Order Api");
	Log.CloseAndFlush();
}
