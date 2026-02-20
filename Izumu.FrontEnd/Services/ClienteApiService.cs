using System.Net.Http.Json;
using Izumu.Shared.Models;

namespace Izumu.FrontEnd.Services;

public class ClienteApiService {
    private readonly HttpClient _http;
    public ClienteApiService(HttpClient http) => _http = http;

    public async Task<List<Cliente>> Listar() => 
        await _http.GetFromJsonAsync<List<Cliente>>("api/clientes") ?? new();

    public async Task<HttpResponseMessage> Guardar(Cliente cliente) => 
        await _http.PostAsJsonAsync("api/clientes", cliente);

    public async Task<HttpResponseMessage> Eliminar(int id) => 
        await _http.DeleteAsync($"api/clientes/{id}");
}