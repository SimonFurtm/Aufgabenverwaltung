using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

//Datenbankkontext, basierend auf der Konfiguration in appsettings.json, zu den Services hinzufügen
builder.Services.AddDbContext<WebAppContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppContext"));
});


//Controller zu den Services hinzufügen
builder.Services.AddControllersWithViews();

//Swagger konfigurieren
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Swagger verwenden, wenn in Entwicklungsumgebung
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Datenbank-Initialisierung
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

//Statische Files, wie CSS, liefern
app.UseStaticFiles();

app.UseRouting();

//Index von "Aufgaben" zu "Home" ändern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=Aufgaben}/{action=Index}/{id?}");

//Restliche Controller Routen verwenden
app.MapControllers();

app.Run();
