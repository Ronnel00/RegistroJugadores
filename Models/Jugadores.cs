using System.ComponentModel.DataAnnotations;

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
        public int Partida { get; set; }
    }
}
