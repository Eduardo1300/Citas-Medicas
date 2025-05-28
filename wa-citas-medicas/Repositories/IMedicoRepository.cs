using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Repositories
{
    public interface IMedicoRepository
    {
        Task<List<QueryMedicoDto>> GetAllMedicos();
        Task saveMedico(RegistroMedicoDto medico);
        Task updateMedico(RegistroMedicoDto medico);
        Task<Medico> GetMedicoById(int id);
    }
}
