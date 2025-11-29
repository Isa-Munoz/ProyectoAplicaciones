using System.Net.Http.Json;       // Extensiones para trabajar con JSON (PostAsJsonAsync, GetFromJsonAsync, etc.)
using Microsoft.JSInterop;        // Permite ejecutar código JavaScript desde C# (para acceder a localStorage)
using System.Text.Json;           // Librería moderna de .NET para serialización/deserialización JSON
using System.Net;                 // Contiene la enumeración HttpStatusCode (200, 404, 500, etc.)

namespace FrontendBlazorApi.Servicios
{
    public class ServicioApiGenerico
    {
        private readonly IHttpClientFactory _fabricaHttp;
        private readonly IJSRuntime _js;
        private const string NombreCliente = "ApiGenerica";
        public ServicioApiGenerico(IHttpClientFactory fabricaHttp, IJSRuntime js)
        {
            // Asignación de parámetros a campos privados para usarlos en otros métodos
            _fabricaHttp = fabricaHttp;
            _js = js;
        }
        private async Task<HttpClient> CrearClienteConTokenAsync()
        {
            var cliente = _fabricaHttp.CreateClient(NombreCliente);

            var token = await _js.InvokeAsync<string>("sessionStorage.getItem", "token");

            // Si hay un token válido, agregarlo como header de autenticación
            if (!string.IsNullOrWhiteSpace(token))
            {
                // Crear el header Authorization con el esquema Bearer
                // Formato: "Authorization: Bearer {token}"
                cliente.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Devolver el cliente HTTP configurado y autenticado
            return cliente;
        }

        private async Task LanzarSiError(HttpResponseMessage respuesta)
        {
            // Si la respuesta es exitosa (código 2xx), no hacer nada
            if (respuesta.IsSuccessStatusCode)
                return;

            // Variable para almacenar detalles del error devueltos por el backend
            string detalle = "";

            try
            {
                // Intentar leer el error en formato JSON estructurado
                // El backend puede devolver: { "Estado": 400, "Mensaje": "Error..." }
                var error = await respuesta.Content.ReadFromJsonAsync<ApiError>();
                detalle = error?.Mensaje ?? "";
            }
            catch
            {
                // Si falla la deserialización JSON, leer como texto plano
                detalle = await respuesta.Content.ReadAsStringAsync();
            }

            // Construir mensaje de error según el código de estado HTTP
            // Se usa "switch expression" (sintaxis moderna de C# 8.0+)
            string mensaje = respuesta.StatusCode switch
            {
                // 400: El servidor no pudo procesar la petición (validación falló, datos incorrectos)
                HttpStatusCode.BadRequest => $"Solicitud incorrecta (400). {detalle}",

                // 401: Token inválido, expirado o ausente
                HttpStatusCode.Unauthorized => "Acceso no autorizado. Verifique sus credenciales o el token.",

                // 403: Token válido pero el usuario no tiene permisos para esta acción
                HttpStatusCode.Forbidden => "Acceso denegado. No tiene permisos suficientes.",

                // 404: La URL solicitada no existe en el servidor
                HttpStatusCode.NotFound => "Recurso no encontrado en el servidor.",

                // 500: Error interno del backend (excepción no controlada, error de BD, etc.)
                HttpStatusCode.InternalServerError => "Error interno en el servidor.",

                // Cualquier otro código de estado
                _ => $"Error inesperado ({(int)respuesta.StatusCode}). {detalle}"
            };

            throw new Exception(mensaje);
        }
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var cliente = await CrearClienteConTokenAsync();
            var respuesta = await cliente.GetAsync(endpoint);
            await LanzarSiError(respuesta);
            return await respuesta.Content.ReadFromJsonAsync<T>();
        }
        public async Task<T?> PostAsync<T>(string endpoint, object datos)
        {
            var cliente = await CrearClienteConTokenAsync();
            var respuesta = await cliente.PostAsJsonAsync(endpoint, datos);
            await LanzarSiError(respuesta);
            return await respuesta.Content.ReadFromJsonAsync<T>();
        }
        public async Task<List<T>> ObtenerTodosAsync<T>(string tabla)
        {
            // Crear cliente HTTP autenticado con el token JWT
            var cliente = await CrearClienteConTokenAsync();

            // Realizar petición GET a /api/{tabla}
            // Ejemplo: GET http://localhost:5031/api/producto
            var respuesta = await cliente.GetAsync($"api/{tabla}");

            // Validar que no hubo errores (lanza excepción si código es 4xx o 5xx)
            await LanzarSiError(respuesta);

            // Deserializar la respuesta JSON a un objeto ApiRespuesta<List<T>>
            // El backend envuelve los datos en esta estructura estándar
            var resultado = await respuesta.Content.ReadFromJsonAsync<ApiRespuesta<List<T>>>();

            // Devolver la lista de datos, o lista vacía si es null
            // Usar ?? (null-coalescing operator) garantiza que nunca devolvemos null
            return resultado?.Datos ?? new List<T>();
        }

        public async Task<T?> ObtenerPorClaveAsync<T>(string tabla, string campoClave, object valor)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Petición GET con parámetros en la URL
            // Ejemplo: GET /api/producto/codigo/PR001
            var respuesta = await cliente.GetAsync($"api/{tabla}/{campoClave}/{valor}");

            // Validar respuesta (puede lanzar excepción si es 404 o error)
            await LanzarSiError(respuesta);

            // Deserializar respuesta a ApiRespuesta<T>
            var resultado = await respuesta.Content.ReadFromJsonAsync<ApiRespuesta<T>>();

            return resultado == null ? default : resultado.Datos;
        }

        public async Task<string> CrearAsync<T>(string tabla, T entidad)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            var respuesta = await cliente.PostAsJsonAsync($"api/{tabla}", entidad);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Registro creado correctamente.";
        }
        public async Task<string> CrearAsync<T>(string tabla, T entidad, string camposEncriptar)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Construir URL con parámetro de query string
            // Ejemplo: /api/usuario?camposEncriptar=contrasena
            var url = $"api/{tabla}?camposEncriptar={camposEncriptar}";

            // Petición POST con JSON en el cuerpo
            var respuesta = await cliente.PostAsJsonAsync(url, entidad);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Registro creado correctamente.";
        }
        public async Task<string> ActualizarAsync<T>(string tabla, string campoClave, object valor, T entidad)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Petición PUT con la clave del registro en la URL
            // Ejemplo: PUT /api/producto/codigo/PR001
            var respuesta = await cliente.PutAsJsonAsync($"api/{tabla}/{campoClave}/{valor}", entidad);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Registro actualizado correctamente.";
        }
        public async Task<string> ActualizarAsync<T>(string tabla, string campoClave, object valor, T entidad, string camposEncriptar)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Construir URL con parámetros de clave y campos a encriptar
            // Ejemplo: /api/usuario/email/usuario@ejemplo.com?camposEncriptar=contrasena
            var url = $"api/{tabla}/{campoClave}/{valor}?camposEncriptar={camposEncriptar}";

            // Petición PUT con JSON en el cuerpo
            var respuesta = await cliente.PutAsJsonAsync(url, entidad);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Registro actualizado correctamente.";
        }
        public async Task<string> EliminarAsync(string tabla, string campoClave, object valor)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Petición DELETE con la clave del registro en la URL
            // Ejemplo: DELETE /api/producto/codigo/PR001
            var respuesta = await cliente.DeleteAsync($"api/{tabla}/{campoClave}/{valor}");

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Registro eliminado correctamente.";
        }

        public async Task<string> EjecutarStoredProcedureAsync<T>(string nombreSP, T parametros)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Convertir el objeto parametros a un diccionario
            var parametrosDict = new Dictionary<string, object?>();

            // Agregar el nombre del SP como primer parámetro
            parametrosDict["nombreSP"] = nombreSP;

            // Agregar todos los parámetros del objeto T al diccionario
            var propiedades = typeof(T).GetProperties();
            foreach (var propiedad in propiedades)
            {
                var valor = propiedad.GetValue(parametros);
                parametrosDict[propiedad.Name] = valor;
            }

            // Petición POST al endpoint de procedimientos almacenados
            // Ejemplo: POST /api/procedimientos/ejecutarsp
            var respuesta = await cliente.PostAsJsonAsync("api/procedimientos/ejecutarsp", parametrosDict);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Stored procedure ejecutado correctamente.";
        }
        public async Task<RespuestaSP> EjecutarStoredProcedureConResultadosAsync<T>(string nombreSP, T parametros)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Convertir el objeto parametros a un diccionario
            var parametrosDict = new Dictionary<string, object?>();

            // Agregar el nombre del SP como primer parámetro
            parametrosDict["nombreSP"] = nombreSP;

            // Agregar todos los parámetros del objeto T al diccionario
            var propiedades = typeof(T).GetProperties();
            foreach (var propiedad in propiedades)
            {
                var valor = propiedad.GetValue(parametros);
                parametrosDict[propiedad.Name] = valor;
            }

            // Petición POST al endpoint de procedimientos almacenados
            var respuesta = await cliente.PostAsJsonAsync("api/procedimientos/ejecutarsp", parametrosDict);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Deserializar y devolver resultados completos
            var resultado = await respuesta.Content.ReadFromJsonAsync<RespuestaSP>();
            return resultado ?? new RespuestaSP();
        }
        public async Task<string> EjecutarStoredProcedureAsync<T>(string nombreSP, T parametros, string camposEncriptar)
        {
            // Crear cliente autenticado
            var cliente = await CrearClienteConTokenAsync();

            // Convertir el objeto parametros a un diccionario
            var parametrosDict = new Dictionary<string, object?>();

            // Agregar el nombre del SP como primer parámetro
            parametrosDict["nombreSP"] = nombreSP;

            if (parametros is Dictionary<string, object?> dict)
            {
                // Si ya es un diccionario, copiar los elementos
                foreach (var kvp in dict)
                {
                    parametrosDict[kvp.Key] = kvp.Value;
                }
            }
            else
            {
                // Si es un objeto anónimo, usar reflexión
                var propiedades = typeof(T).GetProperties();
                foreach (var propiedad in propiedades)
                {
                    var valor = propiedad.GetValue(parametros);
                    parametrosDict[propiedad.Name] = valor;
                }
            }

            // Construir URL con parámetro de encriptación
            var url = $"api/procedimientos/ejecutarsp?camposEncriptar={camposEncriptar}";

            // Petición POST al endpoint de procedimientos almacenados
            var respuesta = await cliente.PostAsJsonAsync(url, parametrosDict);

            // Validar respuesta
            await LanzarSiError(respuesta);

            // Devolver mensaje de éxito
            return "Stored procedure ejecutado correctamente.";
        }
        private class ApiRespuesta<T>
        {

            public int Estado { get; set; }


            public string? Mensaje { get; set; }

            public T? Datos { get; set; }
        }

        private class ApiError
        {
            public int Estado { get; set; }
            public string? Mensaje { get; set; }
        }
    }

    public class RespuestaSP
    {
        public string? Procedimiento { get; set; }
        public List<Dictionary<string, object>>? Resultados { get; set; }
        public int Total { get; set; }
        public string? Mensaje { get; set; }
    }
}
