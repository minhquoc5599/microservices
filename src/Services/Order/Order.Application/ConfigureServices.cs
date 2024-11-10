using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Common.Behaviours;
using System.Reflection;
using FluentValidation;

namespace Order.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly())
				.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
				.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
				.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			return services;
		}
	}
}
