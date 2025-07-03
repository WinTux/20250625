
using EjemplosMAUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EjemplosMAUI.Conexion
{
    internal class RestConexionDatos : IRestConexionDatos
    {
        public readonly HttpClient httpClient;
        private readonly string dominio;
        private readonly string url;
        private readonly JsonSerializerOptions opcionesJson;
        public RestConexionDatos()
        {
            httpClient = new HttpClient();
            //dominio = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5236":"http://localhost:5236";
            dominio = "http://192.168.1.208:5236";
            url = $"{dominio}/api";
            opcionesJson = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        public async Task AddPltoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No hay conexión a la red");
                return;
            }
            try
            {
                // Convertir el objeto Plato a JSON
                string jsonPlato = JsonSerializer.Serialize(plato, opcionesJson);
                StringContent contenido = new StringContent(jsonPlato, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync($"{url}/plato", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Plato agregado correctamente.");
                }
                else
                {
                    Debug.WriteLine($"Error al agregar el plato: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción al agregar el plato: {ex.Message}");
            }
        }

        public async Task DeletePlatoAsync(int id)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No hay conexión a la red");
                return;
            }
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{url}/plato/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Plato eliminado correctamente.");
                }
                else
                {
                    Debug.WriteLine($"Error al eliminar el plato: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción al eliminar el plato: {ex.Message}");
            }
        }

        public async Task<List<Plato>> GetPlatosAsync()
        {
            List<Plato> platos = new List<Plato>();
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet) { 
                Debug.WriteLine("No hay conexión a la red");
                return platos;
            }
            try
            {
                var response = await httpClient.GetAsync($"{url}/plato");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    platos = JsonSerializer.Deserialize<List<Plato>>(contenido, opcionesJson) ?? new List<Plato>();
                }
                else
                {
                    Debug.WriteLine($"Error al obtener los platos: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción al obtener los platos: {ex.Message}");
            }
            return platos;
        }

        public async Task UpdatePlatoAsync(Plato plato)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("No hay conexión a la red");
                return;
            }
            try
            {
                // Convertir el objeto Plato a JSON
                string jsonPlato = JsonSerializer.Serialize(plato, opcionesJson);
                StringContent contenido = new StringContent(jsonPlato, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PutAsync($"{url}/plato/{plato.Id}", contenido);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Plato modificado correctamente.");
                }
                else
                {
                    Debug.WriteLine($"Error al modificar el plato: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Excepción al modificar el plato: {ex.Message}");
            }
        }
    }
}
