using wa_citas_medicas.Dtos;
using wa_citas_medicas.Models;
using wa_citas_medicas.Repositories;

namespace wa_citas_medicas.Services
{
    public class CitaMedicaService : ICitaMedicaService
    {
        private readonly ICitaMedicaRepository _citaRepository;

        public CitaMedicaService(ICitaMedicaRepository citaRepository)
        {
            _citaRepository = citaRepository;
        }

        public async Task<List<QueryCitaMedicaDto>> GetAllCitasMedicas()
        {
            return await _citaRepository.GetAllCitasMedicas();
        }

        public async Task<RegistroCitaMedicaDto?> GetCitaMedicaById(int id)
        {
            Citamedica? citamedica = await _citaRepository.GetCitaMedicaById(id);

            if (citamedica == null)
                return null;

            
            return new RegistroCitaMedicaDto
            {
                Citaid = citamedica.Citaid,
                Fecha = citamedica.Fecha,
                Pacienteid = citamedica.Pacienteid,
                Medicoid = citamedica.Medicoid
            };
        }

        public async Task<bool> saveCitaMedica(RegistroCitaMedicaDto registroCitaMedicaDto)
        {
            return await _citaRepository.saveCitaMedica(registroCitaMedicaDto);
        }

        public async Task updateCitaMedica(RegistroCitaMedicaDto citamedicaDto)
        {
            await _citaRepository.updateCitaMedica(citamedicaDto);
        }
    }
}