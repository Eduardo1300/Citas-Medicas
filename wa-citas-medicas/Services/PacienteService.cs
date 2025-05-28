using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;
using wa_citas_medicas.Repositories;

namespace wa_citas_medicas.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<List<QueryPacienteDto>> GetAllPacientes()
        {
            return await _pacienteRepository.GetAllPacientes();
        }

        public async Task<RegistroPacienteDto> GetPacienteById(int id)
        {
            Paciente paciente = await _pacienteRepository.GetPacienteById(id);

            // Mapear entidad a DTO
            RegistroPacienteDto dto = new RegistroPacienteDto
            {
                Pacienteid = paciente.Pacienteid,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                Edad = paciente.Edad,
                Telefono = paciente.Telefono,
                Estado = paciente.Estado,
                Dni = paciente.Dni
            };

            return dto;
        }

        public async Task savePaciente(RegistroPacienteDto paciente)
        {
            await _pacienteRepository.savePaciente(paciente);
        }

        public async Task updatePaciente(RegistroPacienteDto paciente)
        {
            await _pacienteRepository.updatePaciente(paciente);
        }
    }
}
