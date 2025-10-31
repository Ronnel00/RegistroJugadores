using System.Net.Http.Json;
using RegistroJugadores.ApiDTO;

namespace RegistroJugadores.Services;

public interface IMovimientosApiService
{
    Task<List<MovimientosResponse>> GetMovimientosAsync(int partidaId);
    Task<bool> PostMovimientoAsync(MovimientosRequest request);
    Task<bool> PostMovimientoAsync(int partidaId, string jugador, int fila, int columna);
}

public class MovimientosApiService : IMovimientosApiService
{
    private readonly HttpClient _http;

    public MovimientosApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<MovimientosResponse>> GetMovimientosAsync(int partidaId)
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<MovimientosResponse>>($"api/Movimientos/{partidaId}");
            return data ?? new();
        }
        catch
        {
            return new();
        }
    }

    public async Task<bool> PostMovimientoAsync(MovimientosRequest request)
    {
        try
        {
            var resp = await _http.PostAsJsonAsync("api/Movimientos", request);
            return resp.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public Task<bool> PostMovimientoAsync(int partidaId, string jugador, int fila, int columna)
    {
        var req = new MovimientosRequest
        {
            PartidaId = partidaId,
            Jugador = jugador,
            PosicionFila = fila,
            PosicionColumna = columna
        };
        return PostMovimientoAsync(req);
    }
}
