using System.Net.Http.Json;
using RegistroJugadores.ApiDTO;

namespace RegistroJugadores.Services;

public interface IPartidasApiService
{
    Task<List<PartidaResponse>> GetPartidasAsync();
    Task<PartidaResponse?> GetPartidaAsync(int partidaId);
    Task<bool> JoinPartidaViaPutAsync(int partidaId, int jugador2Id);
}

public class PartidasApiService : IPartidasApiService
{
    private readonly HttpClient _http;

    public PartidasApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PartidaResponse>> GetPartidasAsync()
    {
        try
        {
            var partidas = await _http.GetFromJsonAsync<List<PartidaResponse>>("api/Partidas");
            return partidas ?? new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<PartidaResponse?> GetPartidaAsync(int partidaId)
    {
        try
        {
            return await _http.GetFromJsonAsync<PartidaResponse>($"api/Partidas/{partidaId}");
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> JoinPartidaViaPutAsync(int partidaId, int jugador2Id)
    {
        var partida = await GetPartidaAsync(partidaId);
        if (partida == null)
            return false;

        if (partida.Jugador2Id != null)
            return false; 

        var request = new PartidaRequest(partida.Jugador1Id, jugador2Id);
        var response = await _http.PutAsJsonAsync($"api/Partidas/{partidaId}", request);
        return response.IsSuccessStatusCode;
    }
}
