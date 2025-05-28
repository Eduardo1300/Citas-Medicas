using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using wa_citas_medicas.Data;
using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        private readonly AppDbContext _context;

        public MedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QueryMedicoDto>> GetAllMedicos()
        {
            var medicos = await _context.Set<QueryMedicoDto>()
                .FromSqlRaw("EXEC listarmedicos")
                .ToListAsync();

            return medicos;
        }

        public async Task<Medico> GetMedicoById(int id)
        {
            return await _context.Medicos.FindAsync(id);
        }

        public async Task saveMedico(RegistroMedicoDto medico)
        {
            var parametros = new[]
            {
                new SqlParameter("@nombre", medico.Nombre),
                new SqlParameter("@apellido", medico.Apellido),
                new SqlParameter("@telefono", (object?)medico.Telefono ?? DBNull.Value),
                new SqlParameter("@especialidad", medico.Especialidad)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC registrarmedico @nombre, @apellido, @telefono, @especialidad",
                parametros
            );
        }

        public async Task updateMedico(RegistroMedicoDto medico)
        {
            var parametros = new[]
            {
                new SqlParameter("@medicoid", medico.Medicoid),
                new SqlParameter("@nombre", medico.Nombre),
                new SqlParameter("@apellido", medico.Apellido),
                new SqlParameter("@telefono", (object?)medico.Telefono ?? DBNull.Value),
                new SqlParameter("@especialidad", medico.Especialidad)
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC actualizarmedico @medicoid, @nombre, @apellido, @telefono, @especialidad",
                parametros
            );
        }
    }
}
