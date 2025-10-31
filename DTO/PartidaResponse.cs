namespace RegistroJugadores.ApiDTO;

public class PartidaResponse
{
    public int PartidaId { get; set; }
    public int Jugador1Id { get; set; }
    public int? Jugador2Id { get; set; }
    public string? EstadoTablero { get; set; }
    public string? EstadoPartida { get; set; }
    public int? TurnoJugadorId { get; set; }
    public string? Ganador { get; set; }
}
