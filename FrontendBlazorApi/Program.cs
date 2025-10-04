// Program.cs
// Archivo de arranque principal de la aplicación Blazor Web App (plantilla moderna unificada).
// Aquí se configuran los servicios y se define cómo se ejecuta la aplicación.

using FrontendBlazorApi.Components;          // Importa el espacio de nombres donde está App.razor
using Microsoft.AspNetCore.Components;       // Librerías base de Blazor
using Microsoft.AspNetCore.Components.Web;   // Funcionalidades adicionales de renderizado

var builder = WebApplication.CreateBuilder(args);

// -------------------------------
// Registro de servicios en el contenedor de dependencias
// -------------------------------

// Se registran los servicios de Razor Components.
// "AddInteractiveServerComponents" habilita el modo interactivo tipo Blazor Server (SignalR).
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


 // Servicio HttpClient para consumir la API externa de productos.
 // Se activará más adelante cuando se implemente la conexión a la API.
 builder.Services.AddHttpClient("ApiUsuarios", cliente =>
 {
     // URL base de la API que expone /api/producto
     cliente.BaseAddress = new Uri("http://localhost:5031/");
     // Aquí se pueden agregar encabezados por defecto si se requiere.
 });

 builder.Services.AddHttpClient("ApiEntregables", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/"); // URL de tu API
});

builder.Services.AddHttpClient("ApiEstados", cliente =>
{
   cliente.BaseAddress = new Uri("http://localhost:5031/"); // URL de tu API
});
builder.Services.AddHttpClient("ApiTipoProductos", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/"); // URL de tu API
});

builder.Services.AddHttpClient("ApiTipoResponsables", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/"); // URL de tu API
});

builder.Services.AddHttpClient("ApiTipoProyectos", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/"); // URL de tu API
});

builder.Services.AddHttpClient("ApiVariableEstrategicas", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/"); // URL de tu API
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Error", createScopeForErrors: true);


    app.UseHsts();
}


app.UseHttpsRedirection();


app.UseStaticFiles();


app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
