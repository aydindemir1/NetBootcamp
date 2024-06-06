using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bootcamp.Web.Controllers
{
    [Authorize]
    public class YController : Controller
    {
        public IActionResult Index()
        {
            var userName = User.Identity.Name;

            var roles = User.FindAll(x => x.Type == ClaimTypes.Role);
            return View();
        }
    }
}
