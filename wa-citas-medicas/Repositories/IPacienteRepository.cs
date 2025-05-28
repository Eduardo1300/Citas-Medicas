using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;

namespace wa_citas_medicas.Repositories
{
    public interface IPacienteRepository
    {
        Task<List<QueryPacienteDto>> GetAllPacientes();
        Task savePaciente(RegistroPacienteDto paciente);
        Task updatePaciente(RegistroPacienteDto paciente);
        Task<Paciente> GetPacienteById(int id);

    }
}
