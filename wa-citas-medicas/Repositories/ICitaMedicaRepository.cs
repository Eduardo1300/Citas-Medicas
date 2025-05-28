using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Repositories
{
    public interface ICitaMedicaRepository
    {
        Task<List<QueryCitaMedicaDto>> GetAllCitasMedicas();
        Task<bool> saveCitaMedica(RegistroCitaMedicaDto citamedicaDto);
        Task updateCitaMedica(RegistroCitaMedicaDto citamedicaDto);
        Task<Citamedica> GetCitaMedicaById(int id);
    }
}