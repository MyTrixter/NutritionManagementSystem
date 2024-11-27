using Nms.Db.Entities;
using Nms.Services.Services;
using Nms.Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

public class UserMiddleware
{
    private readonly RequestDelegate _next;

    public UserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserService userService)
    {
        if (context.Request.Method == HttpMethods.Post && context.Request.Path.StartsWithSegments("/user/create"))
        {
            await CreateUser(context, userService);
            return;
        }

        await _next(context);
    }

    private async Task CreateUser(HttpContext context, IUserService userService)
    {
        context.Request.EnableBuffering();
        var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        var user = JsonSerializer.Deserialize<User>(requestBody);
        if (user == null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Invalid data format.");
            return;
        }

        if (!ValidateModel(user, context)) return;

        var response = await userService.CreateAsync(user);
        context.Response.StatusCode = StatusCodes.Status201Created;
        await context.Response.WriteAsync($"User created successfully. User id = {response.Id}.");
    }

    private bool ValidateModel(User user, HttpContext context)
    {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(user);

        if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.WriteAsync(JsonSerializer.Serialize(validationResults)).Wait();
            return false;
        }

        return true;
    }
}
