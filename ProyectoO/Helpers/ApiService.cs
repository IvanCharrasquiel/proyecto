// Helpers/ApiService.cs
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoO.Helpers
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public string BaseUrl { get; }

        public ApiService(string baseUrl)
        {
            _httpClient = new HttpClient();
            BaseUrl = baseUrl;
        }

        // Método para establecer el encabezado de autorización
        public void SetAuthorizationHeader(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        // Método GET
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var url = $"{BaseUrl}/{endpoint}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al obtener datos: {response.ReasonPhrase}. Detalles: {errorContent}");
            }
        }

        // Método POST
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var url = $"{BaseUrl}/{endpoint}";
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al enviar datos: {response.ReasonPhrase}. Detalles: {errorContent}");
            }
        }

        // Método PUT
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var url = $"{BaseUrl}/{endpoint}";
            var jsonData = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseData);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al actualizar datos: {response.ReasonPhrase}. Detalles: {errorContent}");
            }
        }

        // Método DELETE
        public async Task<int> DeleteAsync(string endpoint)
        {
            var url = $"{BaseUrl}/{endpoint}";
            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // Método para subir archivos (por ejemplo, fotos de perfil)
        public async Task<string> UploadFileAsync(string endpoint, string filePath)
        {
            var url = $"{BaseUrl}/{endpoint}";
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            content.Add(fileContent, "file", System.IO.Path.GetFileName(filePath));

            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return responseData; // Suponiendo que el servidor devuelve la URL de la foto
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al subir el archivo: {response.ReasonPhrase}. Detalles: {errorContent}");
            }
        }
    }
}
