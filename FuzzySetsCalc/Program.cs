using FuzzySetsCalc.Commands;
using FuzzySetsCalc.Data;
using FuzzySetsCalc.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<FuzzySetStorage>();
var jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Objects };
builder.Services.AddSingleton<JsonSerializerSettings>(jsonSettings);
builder.Services.AddSingleton<FuzzySetService>(isp => new FuzzySetService(isp.GetRequiredService<FuzzySetStorage>()));
builder.Services.AddSingleton<Invoker>();

var app = builder.Build();

var invoker = app.Services.GetService<Invoker>();

if (invoker != null && File.Exists("/data/commands.json"))
{
    try
    {
        invoker.Commands = JsonConvert.DeserializeObject<Invoker>(File.ReadAllText("/data/commands.json"), jsonSettings)?.Commands ?? new List<ICommand>();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

invoker?.InvokeAllNoThrow(app.Services);

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();