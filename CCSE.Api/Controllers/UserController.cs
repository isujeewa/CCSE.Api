using Microsoft.AspNetCore.Mvc;

namespace CCSE.Api.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
