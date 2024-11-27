using Microsoft.AspNetCore.Mvc;
using Nms.Db.Entities;
using Nms.MVC.Enums;
using Nms.Services.Services.Interfaces;

public class FoodController : Controller
{
    private readonly IFoodService _foodService;

    public FoodController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> FoodList(string searchName, SortOrder? sortOrder)
    {
        ViewData["CurrentSearch"] = searchName;
        ViewData["CurrentSort"] = sortOrder;

        var foods = await _foodService.GetAllByUserIdAsync(1);

        if (!string.IsNullOrEmpty(searchName))
        {
            foods = foods.Where(f => f.Name.Contains(searchName)).ToList(); ;
        }

        foods = sortOrder switch
        {
            SortOrder.NameAsc => foods.OrderBy(f => f.Name).ToList(),
            SortOrder.NameDesc => foods.OrderByDescending(f => f.Name).ToList(),
            SortOrder.CaloriesAsc => foods.OrderBy(f => f.Calories).ToList(),
            SortOrder.CaloriesDesc => foods.OrderByDescending(f => f.Calories).ToList(),
            _ => foods.OrderBy(f => f.Name).ToList()
        };

        return View(foods.ToList());
    }

    [HttpGet]
    public IActionResult CreateFood()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFood(Food food)
    {
        food.UserId = 1;

        var createdFood = await _foodService.CreateAsync(food);

        return View(createdFood); 
    }
}
