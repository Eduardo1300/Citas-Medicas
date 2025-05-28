using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using wa_citas_medicas.Data;
using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly AppDbContext _context;

        public PacienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QueryPacienteDto>> GetAllPacientes()
        {
            var pacientes = await _context.Set<QueryPacienteDto>()
                .FromSqlRaw("EXEC listarpacientes")
                .ToListAsync();

            return pacientes;
        }

        public async Task<Paciente> GetPacienteById(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task savePaciente(RegistroPacienteDto paciente)
        {
            var parametros = new[]
            {
                new SqlParameter("@nombre", paciente.Nombre),
                new SqlParameter("@apellido", paciente.Apellido),
                new SqlParameter("@edad", paciente.Edad),
                new SqlParameter("@telefono", (object?)paciente.Telefono ?? DBNull.Value),
                new SqlParameter("@estado", (object?)paciente.Estado ?? DBNull.Value),
                new SqlParameter("@dni", paciente.Dni),
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC registrarpaciente @nombre, @apellido, @edad, @telefono, @estado, @dni",
                parametros
            );
        }

        public async Task updatePaciente(RegistroPacienteDto paciente)
        {
            var parametros = new[]
            {
                new SqlParameter("@pacienteid", paciente.Pacienteid),
                new SqlParameter("@nombre", paciente.Nombre),
                new SqlParameter("@apellido", paciente.Apellido),
                new SqlParameter("@edad", paciente.Edad),
                new SqlParameter("@telefono", (object?)paciente.Telefono ?? DBNull.Value),
                new SqlParameter("@estado", (object?)paciente.Estado ?? DBNull.Value),
                new SqlParameter("@dni", paciente.Dni),
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC actualizarpaciente @pacienteid, @nombre, @apellido, @edad, @telefono, @estado, @dni",
                parametros
            );
        }
    }
}
