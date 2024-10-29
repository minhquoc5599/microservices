using Basket.Api.Extensions;
using Common.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information($"Start {builder.Environment.ApplicationName}");

try
{
	// Add services to the container.
	builder.Host.UseSerilog(Serilogger.Configure);
	builder.Host.AddAppConfigurations();

	builder.Services.ConfigureServices();
	builder.Services.ConfigureRedis(builder.Configuration);
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
		app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json",
			$"{builder.Environment.ApplicationName} v1"));
	}

	//app.UseHttpsRedirection(); //production

	app.UseAuthorization();

	app.MapDefaultControllerRoute();

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Unhandled exception");
}
finally
{
	Log.Information("Shut down Basket Api");
	Log.CloseAndFlush();
}
