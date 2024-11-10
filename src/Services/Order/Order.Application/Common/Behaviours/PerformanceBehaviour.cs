using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Order.Application.Common.Behaviours
{
	public class PerformanceBehaviour<TRequest, TResponse> :
		IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{

		private readonly Stopwatch _stopwatch;
		private readonly ILogger<TRequest> _logger;

		public PerformanceBehaviour(ILogger<TRequest> logger)
		{
			_stopwatch = new Stopwatch();
			_logger = logger;
		}

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			_stopwatch.Start();
			var response = await next();
			_stopwatch.Stop();

			var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

			if (elapsedMilliseconds <= 500) return response;

			var requestName = typeof(TRequest).Name;

			_logger.LogWarning("Application Long Running Request: {Name} ({ElapsedMilliseconds}) {@Request}",
				requestName, elapsedMilliseconds, request);

			return response;
		}
	}
}
