using System.Net.Http.Json;
using RegistroJugadores.ApiDTO;
using RegistroJugadores.BlazorWasm.Shared;

namespace RegistroJugadores.Services;

public interface IMovimientosApiService
{
    Task<Resource<List<MovimientosResponse>>> GetMovimientosAsync(int partidaId);
    Task<Resource<bool>> PostMovimientoAsync(MovimientosRequest request);
}

public class MovimientosApiService : IMovimientosApiService
{
    private readonly HttpClient _http;

    public MovimientosApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<Resource<List<MovimientosResponse>>> GetMovimientosAsync(int partidaId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<List<MovimientosResponse>>($"api/Movimientos/{partidaId}");
            return new Resource<List<MovimientosResponse>>.Success(result ?? new());
        }
        catch (Exception ex)
        {
            return new Resource<List<MovimientosResponse>>.Error(ex.Message);
        }
    }

    public async Task<Resource<bool>> PostMovimientoAsync(MovimientosRequest request)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/Movimientos", request);
            response.EnsureSuccessStatusCode();
            return new Resource<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return new Resource<bool>.Error(ex.Message);
        }
    }
}
