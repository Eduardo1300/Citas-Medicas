using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using wa_citas_medicas.Data;
using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Repositories
{
    public class CitaMedicoRepository : ICitaMedicaRepository
    {
        private readonly AppDbContext _context;

        public CitaMedicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QueryCitaMedicaDto>> GetAllCitasMedicas()
        {
            var citas = await _context.Set<QueryCitaMedicaDto>()
                .FromSqlRaw("EXEC listarcitas")
                .ToListAsync();

            return citas;
        }

        public async Task<Citamedica> GetCitaMedicaById(int id)
        {
            return await _context.Citamedicas.FindAsync(id);
        }

        public async Task<bool> saveCitaMedica(RegistroCitaMedicaDto citamedicaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {

                var conflicto = await _context.Citamedicas
                     .AnyAsync(c => c.Fecha.Date == citamedicaDto.Fecha.Date && c.Medicoid == citamedicaDto.Medicoid);

                if (conflicto)
                {
                    await transaction.RollbackAsync();
                    return false;
                }


                var fechaParam = new SqlParameter("@fecha", citamedicaDto.Fecha.Date);
                var pacienteParam = new SqlParameter("@pacienteid", citamedicaDto.Pacienteid);
                var medicoParam = new SqlParameter("@medicoid", citamedicaDto.Medicoid);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC registrarcitas @fecha, @pacienteid, @medicoid",
                    fechaParam, pacienteParam, medicoParam
                );

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine("Error al guardar la cita médica: " + ex.Message);
                throw;
            }
        }



        public async Task updateCitaMedica(RegistroCitaMedicaDto citamedicaDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                
                var conflicto = await _context.Citamedicas
                    .AnyAsync(c => c.Fecha.Date == citamedicaDto.Fecha.Date
                                && c.Medicoid == citamedicaDto.Medicoid
                                && c.Citaid != citamedicaDto.Citaid);

                if (conflicto)
                {
                    
                    throw new InvalidOperationException("El médico ya tiene una cita en esta fecha.");
                }

                
                var idParam = new SqlParameter("@citaid", citamedicaDto.Citaid);
                var fechaParam = new SqlParameter("@fecha", citamedicaDto.Fecha.Date);
                var pacienteParam = new SqlParameter("@pacienteid", citamedicaDto.Pacienteid);
                var medicoParam = new SqlParameter("@medicoid", citamedicaDto.Medicoid);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC actualizarcita @citaid, @fecha, @pacienteid, @medicoid",
                    idParam, fechaParam, pacienteParam, medicoParam
                );

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                if (transaction.GetDbTransaction().Connection != null)
                {
                    await transaction.RollbackAsync();
                }

                Console.WriteLine("Error al actualizar la cita médica: " + ex.Message);
                throw;
            }
        }
    }
    }