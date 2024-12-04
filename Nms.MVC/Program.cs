using Microsoft.EntityFrameworkCore;
using Nms.Db;
using Nms.Db.Repositories;
using Nms.Db.Repositories.Interfaces;
using Nms.Services.Services;
using Nms.Services.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NmsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IFoodService, FoodService>();
builder.Services.AddTransient<IMealPlanService, MealPlanService>();
builder.Services.AddTransient<IMealPlanItemService, MealPlanItemService>();
builder.Services.AddTransient<IScheduledMealPlanService, ScheduledMealPlanService>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IFoodRepository, FoodRepository>();
builder.Services.AddTransient<IMealPlanRepository, MealPlanRepository>();
builder.Services.AddTransient<IMealPlanItemRepository, MealPlanItemRepository>();
builder.Services.AddTransient<IScheduledMealPlanRepository, ScheduledMealPlanRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
