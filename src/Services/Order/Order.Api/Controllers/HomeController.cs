using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Controllers
{
	public class HomeController : ControllerBase
	{
		public IActionResult Index()
		{
			return Redirect("~/swagger");
		}
	}
}
