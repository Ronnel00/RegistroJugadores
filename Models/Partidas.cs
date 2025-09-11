using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroJugadores.Models
{
    public class Partidas
    {
        [Key]
        public int PartidaId { get; set; }

        [Required(ErrorMessage = "El Jugador 1 es obligatorio.")]
        public int Jugador1Id { get; set; }

        public int? Jugador2Id { get; set; }
        [Required(ErrorMessage = "El estado de la partida es obligatorio.")]
        [StringLength(20, ErrorMessage = "El estado no debe exceder los 20 caracteres.")]
        public string EstadoPartida { get; set; }

        public int? GanadorId { get; set; }

        [Required(ErrorMessage = "Debe indicar el turno del jugador.")]
        public int TurnoJugadorId { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        public DateTime? FechaFin { get; set; }

        [ForeignKey(nameof(Jugador1Id))]
        public virtual Jugadores Jugador1 { get; set; }

        [ForeignKey(nameof(Jugador2Id))]
        public virtual Jugadores? Jugador2 { get; set; }

        [ForeignKey(nameof(GanadorId))]
        public virtual Jugadores? Ganador { get; set; }

        [ForeignKey(nameof(TurnoJugadorId))]
        public virtual Jugadores TurnoJugador { get; set; }

    }
}
