using FluentValidation;
using GoodCarRentals.Application;
using GoodCarRentals.Data;
using GoodCarRentals.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the iac container.
builder.Services.AddDbContext<CarRentalsContext>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<RentalsService>();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IValidator<RentCarViewModel>, RentCarValidator>();
builder.Services.AddTransient<IValidator<ReturnCarViewModel>, ReturnCarValidator>();

var app = builder.Build();
// From here, all is to Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CarRentalsContext>();
    context.Database.EnsureCreated();

    if (app.Environment.IsDevelopment())
    {
        // data will be always renewed in development
        context.SeedDevelopmentData();
    }
}
// Have error handler in production as well - avoid different behavior between environments as well
app.UseExceptionHandler("/Home/Error");
// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
