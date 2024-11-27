using Nms.Db.Entities;
using Nms.Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class FoodMiddleware
{
    private readonly RequestDelegate _next;

    public FoodMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IFoodService foodService)
    {
        if (context.Request.Method == HttpMethods.Get && context.Request.Path.StartsWithSegments("/food/get/all"))
        {
            await GetAllUsersFood(context, foodService);
            return;
        }
        else if (context.Request.Method == HttpMethods.Get && context.Request.Path.StartsWithSegments("/food/get"))
        {
            await Get(context, foodService);
            return;
        }
        else if (context.Request.Method == HttpMethods.Post && context.Request.Path.StartsWithSegments("/food/create"))
        {
            await Add(context, foodService);
            return;
        }
        else if (context.Request.Method == HttpMethods.Put && context.Request.Path.StartsWithSegments("/food/update"))
        {
            await Update(context, foodService);
            return;
        }


        await _next(context);
    }

    private async Task GetAllUsersFood(HttpContext context, IFoodService foodService)
    {
        var idSegment = context.Request.Path.Value?.Split("/").LastOrDefault();
        if (!int.TryParse(idSegment, out int userId))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid user id.");
            return;
        }

        var foods = await foodService.GetAllByUserIdAsync(userId);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(foods));
    }

    private async Task Get(HttpContext context, IFoodService foodService)
    {
        var idSegment = context.Request.Path.Value?.Split("/").LastOrDefault();
        if (!int.TryParse(idSegment, out int id))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid ID.");
            return;
        }

        var food = foodService.GetByIdAsync(id);
        if (food == null)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("Food not found.");
            return;
        }

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(food));
    }

    private async Task Add(HttpContext context, IFoodService foodService)
    {
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        var food = JsonSerializer.Deserialize<Food>(requestBody);
        if (food == null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid data format.");
            return;
        }

        if (!ValidateModel(food, context)) 
            return;

        await foodService.CreateAsync(food);
        context.Response.StatusCode = StatusCodes.Status201Created;
        await context.Response.WriteAsync("Food added successfully.");
    }

    private async Task Update(HttpContext context, IFoodService foodService)
    {
        var idSegment = context.Request.Path.Value?.Split("/").LastOrDefault();
        if (!int.TryParse(idSegment, out int id))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid ID.");
            return;
        }

        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        var updatedFood = JsonSerializer.Deserialize<Food>(requestBody);
        if (updatedFood == null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid data format.");
            return;
        }

        if (!ValidateModel(updatedFood, context)) 
            return;

        var success = foodService.UpdateAsync(updatedFood);

        context.Response.StatusCode = StatusCodes.Status200OK;
        await context.Response.WriteAsync("Food updated successfully.");
    }

    private bool ValidateModel(Food food, HttpContext context)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(food);

        if (!Validator.TryValidateObject(food, validationContext, validationResults, true))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.WriteAsync(JsonSerializer.Serialize(validationResults)).Wait();
            return false;
        }

        return true;
    }
}
