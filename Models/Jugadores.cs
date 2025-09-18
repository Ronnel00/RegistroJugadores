using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroJugadores.Models
{
    public class Jugadores
    {
        [Key]
        public int JugadorId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo Partida es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El numero de partidas debe ser un valor valido.")]
        public int Victorias { get; set; } = 0;
        public int Empates { get; set; } = 0;
        public int Derrotas { get; set; } = 0;

        [InverseProperty(nameof(Models.Movimientos.Jugador))]
        public virtual ICollection<Movimientos> Movimientos { get; set; } = new List<Movimientos>();
    }
}
