using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RegistroJugadores.DAL;
using RegistroJugadores.Models;

namespace RegistroJugadores.Services
{
    public class PartidasService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<Partidas?> Guardar(Partidas partida)
        {
            if (!await Existe(partida.PartidaId))
            {
                return await Insertar(partida);
            }
            else
            {
                return await Modificar(partida);
            }
        }

        public async Task<bool> Existe(int PartidaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas.AnyAsync(p => p.PartidaId == PartidaId);
        }

        private async Task<Partidas?> Insertar(Partidas partida)
        {
            if (string.IsNullOrEmpty(partida.EstadoTablero))
                partida.EstadoTablero = "---------";

            if (partida.TurnoJugadorId == 0)
                partida.TurnoJugadorId = partida.Jugador1Id;

            partida.EstadoPartida = "En progreso";
            partida.FechaInicio = DateTime.UtcNow;

            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Partidas.Add(partida);
            await contexto.SaveChangesAsync();
            return partida;
        }

        private async Task<Partidas?> Modificar(Partidas partida)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();

            var original = await contexto.Partidas
                .FirstOrDefaultAsync(p => p.PartidaId == partida.PartidaId);

            if (original == null) return null;

            original.Jugador1Id = partida.Jugador1Id;
            original.Jugador2Id = partida.Jugador2Id;
            original.TurnoJugadorId = partida.TurnoJugadorId;
            original.GanadorId = partida.GanadorId;
            original.EstadoPartida = partida.EstadoPartida;
            original.FechaFin = partida.FechaFin;
            original.EstadoTablero = partida.EstadoTablero;

            await contexto.SaveChangesAsync();
            return original;
        }

        public async Task<Partidas?> Buscar(int PartidaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.Ganador)
                .Include(p => p.TurnoJugador)
                .FirstOrDefaultAsync(p => p.PartidaId == PartidaId);
        }

        public async Task<bool> Eliminar(int PartidaId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .Where(p => p.PartidaId == PartidaId)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Partidas>> Listar(Expression<Func<Partidas, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Partidas
                .Include(p => p.Jugador1)
                .Include(p => p.Jugador2)
                .Include(p => p.Ganador)
                .Include(p => p.TurnoJugador)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
