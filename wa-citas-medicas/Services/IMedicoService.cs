using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Services
{
    public interface IMedicoService
    {
        Task<List<QueryMedicoDto>> GetAllMedicos();
        Task saveMedico(RegistroMedicoDto medico);
        Task updateMedico(RegistroMedicoDto medico);
        Task<RegistroMedicoDto> GetMedicoById(int id);
    }
}
