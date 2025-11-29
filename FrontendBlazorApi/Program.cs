// Program.cs
// Archivo de arranque principal de la aplicación Blazor Web App (plantilla moderna unificada).
// Aquí se configuran los servicios y se define cómo se ejecuta la aplicación.


using FrontendBlazorApi.Components; 
using Microsoft.AspNetCore.Components; 
using Microsoft.AspNetCore.Components.Web; 
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using FrontendBlazorApi.Servicios; // Asegúrate de tener este 'using' si los servicios están en esa carpeta

var builder = WebApplication.CreateBuilder(args);

// =========================================================================
// 1. CONFIGURACIÓN DE BLALOR Y SERVICIOS CORE (DEL PROFESOR)
// =========================================================================

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configurar opciones del circuito de Blazor Server (CRUCIAL para rendimiento y reconexión)
builder.Services.AddServerSideBlazor(options =>
{
    // Desconectar el circuito después de 30 segundos de inactividad
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromSeconds(30);
    // Intentos de reconexión del cliente
    options.DisconnectedCircuitMaxRetained = 100;
    // Tamaño máximo del buffer de JavaScript interop
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
});

// Registrar ServicioAutenticacion y ServicioApiGenerico
builder.Services.AddScoped<ServicioAutenticacion>();

// NOTA: Si usas ServicioApiGenerico para todas las APIs, podrías eliminar
// las configuraciones de HttpClient duplicadas, pero las mantendré.
builder.Services.AddScoped<ServicioApiGenerico>(); 


// =========================================================================
// 2. CONFIGURACIÓN DE HTTP CLIENTS (TUS DEFINICIONES)
// =========================================================================

// Configuración original del profesor, renombrada para ser más específica si se usa para CRUD genérico
builder.Services.AddHttpClient("ApiGenerica", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});

// Tus configuraciones de HttpClient (Asegúrate de que 'http://localhost:5031/' sea la URL CORRECTA de tu API)
builder.Services.AddHttpClient("ApiUsuarios", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});

builder.Services.AddHttpClient("ApiEntregables", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});

builder.Services.AddHttpClient("ApiEstados", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});
builder.Services.AddHttpClient("ApiTipoProductos", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});

builder.Services.AddHttpClient("ApiTipoResponsables", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});

builder.Services.AddHttpClient("ApiTipoProyectos", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});

builder.Services.AddHttpClient("ApiVariableEstrategicas", cliente =>
{
    cliente.BaseAddress = new Uri("http://localhost:5031/");
});


// =========================================================================
// 3. CONSTRUIR Y CONFIGURAR MIDDLEWARE
// =========================================================================

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// ⬇️ MIDDLEWARE PARA EVITAR CACHÉ (DEL PROFESOR - MUY RECOMENDADO EN DESARROLLO) ⬇️
app.Use(async (context, next) =>
{
    // Agregar headers para evitar caché en TODAS las respuestas
    context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});
// ⬆️ FIN DEL MIDDLEWARE DE CACHÉ ⬆️

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
