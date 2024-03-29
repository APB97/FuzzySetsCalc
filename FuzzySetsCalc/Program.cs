using FuzzySetsCalc.Commands;
using FuzzySetsCalc.Data;
using FuzzySetsCalc.Models;
using FuzzySetsCalc.Services;
using MediatR;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(typeof(Program));

builder.Services.AddSingleton<FuzzySetStorage>();
builder.Services.AddSingleton<ChartDisplaySettings>();
var jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };
builder.Services.AddSingleton<JsonSerializerSettings>(jsonSettings);
builder.Services.AddSingleton<FuzzySetService>();
builder.Services.AddSingleton<Invoker>();
builder.Services.AddSingleton<JsonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FuzzySet}/{action=Index}/{id?}");

app.Run();
