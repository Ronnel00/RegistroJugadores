namespace RegistroJugadores.ApiDTO;

public class PartidaRequest
{
    public int Jugador1Id { get; set; }
    public int? Jugador2Id { get; set; }
    public PartidaRequest(int jugador1Id, int? jugador2Id)
    {
        Jugador1Id = jugador1Id;
        Jugador2Id = jugador2Id;
    }
}
