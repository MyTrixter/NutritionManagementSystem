using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nms.Core.Enums;
using Nms.Db.Entities;
using Nms.Services.Services.Interfaces;

public class FoodController : Controller
{
    private readonly IFoodService _foodService;

    public FoodController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> Main(string searchName, SortOrder? sortOrder, int page = 1, int pageSize = 5)
    {
        ViewData["CurrentSearch"] = searchName;
        ViewData["CurrentSort"] = sortOrder;

        var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        Guid userId;

        Guid.TryParse(userIdString, out userId);

        var foods = await _foodService.GetFilteredAndSortedFoods(userId, searchName, sortOrder);

        int totalItems = foods.Count();
        var paginatedFoods = foods
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = (int)Math.Ceiling((double)totalItems / pageSize);

        return View(paginatedFoods);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(Food food)
    {
        //if (!ModelState.IsValid)
        //{
        //    return View(food);
        //}    

        var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        Guid userId;

        Guid.TryParse(userIdString, out userId);
        food.UserId = userId;

        var createdFood = await _foodService.CreateAsync(food);

        return RedirectToAction("Main");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        var foodItem = await _foodService.GetByIdAsync(id);
        if (foodItem == null)
        {
            return NotFound(); 
        }

        await _foodService.DeleteAsync(id); 
        TempData["Message"] = "Food item deleted successfully."; 
        return RedirectToAction("Main"); 
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(Guid id)
    {
        var food = await _foodService.GetByIdAsync(id);
        if (food == null)
        {
            return NotFound();
        }

        return View(food);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(Guid id, Food food)
    {
        if (id != food.Id)
        {
            return BadRequest();
        }

        //if (!ModelState.IsValid)
        //{
            //return View(food);
        //}

        await _foodService.UpdateAsync(food);
        return RedirectToAction("Main");
    }
}
