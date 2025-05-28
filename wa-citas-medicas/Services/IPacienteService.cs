using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Services
{
    public interface IPacienteService
    {
        Task<List<QueryPacienteDto>> GetAllPacientes();
        Task savePaciente(RegistroPacienteDto paciente);
        Task updatePaciente(RegistroPacienteDto paciente);
        Task<RegistroPacienteDto> GetPacienteById(int id);
    }
}
