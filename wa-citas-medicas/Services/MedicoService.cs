using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;
using wa_citas_medicas.Repositories;

namespace wa_citas_medicas.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<List<QueryMedicoDto>> GetAllMedicos()
        {
            return await _medicoRepository.GetAllMedicos();
        }

        public async Task<RegistroMedicoDto> GetMedicoById(int id)
        {
            Medico medico = await _medicoRepository.GetMedicoById(id);

            // Mapear entidad a DTO
            RegistroMedicoDto dto = new RegistroMedicoDto
            {
                Medicoid = medico.Medicoid,
                Nombre = medico.Nombre,
                Apellido = medico.Apellido,
                Telefono = medico.Telefono,
                Especialidad = medico.Especialidad
            };

            return dto;
        }

        public async Task saveMedico(RegistroMedicoDto medico)
        {
            await _medicoRepository.saveMedico(medico);
        }

        public async Task updateMedico(RegistroMedicoDto medico)
        {
            await _medicoRepository.updateMedico(medico);
        }
    }
}
