using Common.Logging;
using Product.Api.Data;
using Product.Api.Extensions;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var builder = WebApplication.CreateBuilder(args);

Log.Information($"Start {builder.Environment.ApplicationName}");

try
{
	builder.Host.UseSerilog(Serilogger.Configure);
	builder.Host.AddAppConfigurations();

	// Add services to the container.
	builder.Services.AddInfrastructure(builder.Configuration);

	var app = builder.Build();

	app.UseInfrastructure();

	app.MigrationDatabase<ProductContext>((context, _) =>
	{
		ProductSeeder.SeedProductAsync(context, Log.Logger).Wait();
	}).Run();
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
	Log.Information("Shut down Product Api");
	Log.CloseAndFlush();
}
