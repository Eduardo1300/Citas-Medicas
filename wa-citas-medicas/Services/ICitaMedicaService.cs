using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Services
{
    public interface ICitaMedicaService
    {
        Task<List<QueryCitaMedicaDto>> GetAllCitasMedicas();

        Task<bool> saveCitaMedica(RegistroCitaMedicaDto registroCitaMedicaDto);
        Task updateCitaMedica(RegistroCitaMedicaDto citamedicaDto);
        Task<RegistroCitaMedicaDto> GetCitaMedicaById(int id);
    }
}