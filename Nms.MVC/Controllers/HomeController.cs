using Microsoft.AspNetCore.Mvc;

namespace YourApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateUser()
        {
            return RedirectToAction("Create", "User");
        }

        public IActionResult CreateFood()
        {
            return RedirectToAction("CreateFood", "Food");
        }

        public IActionResult FoodList()
        {
            return RedirectToAction("FoodList", "Food");
        }
    }
}
