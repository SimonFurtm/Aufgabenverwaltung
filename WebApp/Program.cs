using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

//Builder wird erstellt, um die WebApp zu erstellen (Bauen)
var builder = WebApplication.CreateBuilder(args);

//Datenbankkontext, basierend auf der Konfiguration in appsettings.json, zu den Services hinzufügen
builder.Services.AddDbContext<WebAppContext>((options) =>
{
    //SQL Server wird als Datenbankanbieter für den Datenbankkontext konfiguriert
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebAppContext"));
});

//Controller der Views wird zu den Services hinzufügen
builder.Services.AddControllersWithViews();

//Swagger konfigurieren
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//App wird erstellt, um die WebApp zu konfigurieren
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
    //Bereich, wo Ressourcen, Dienste und Datenbankverbindungen erstellt und verwaltet werden
    var services = scope.ServiceProvider;
    SeedData.Initialize(services); //* --> SeedData.cs
}

//Statische Files, wie CSS, liefern
app.UseStaticFiles();

//Routing Funktionen der WebApp werden aktiviert
app.UseRouting();

//Index von "Aufgaben" zu "Home" ändern
app.MapControllerRoute(
    //Index = default route (localhost/)
    name: "default", 
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Aufgaben}/{action=Index}/{id?}");

//Restliche Controller Routen verwenden
app.MapControllers();

app.Run();
//* --> WebAppContext.cs