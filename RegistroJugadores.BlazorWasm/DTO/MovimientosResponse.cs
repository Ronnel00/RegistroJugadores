namespace RegistroJugadores.ApiDTO;

public class MovimientosResponse
{
    public int MovimientoId { get; set; }
    public string Jugador { get; set; } = "X";

    public int PosicionFila { get; set; }

    public int PosicionColumna { get; set; }

}
