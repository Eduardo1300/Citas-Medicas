using Microsoft.AspNetCore.Mvc;
using wa_citas_medicas.Dtos;
using wa_citas_medicas.Services;

namespace wa_citas_medicas.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pacientes = await _pacienteService.GetAllPacientes();
            return View(pacientes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RegistroPacienteDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistroPacienteDto pacienteDto)
        {
            if (ModelState.IsValid)
            {
                await _pacienteService.savePaciente(pacienteDto);
                return RedirectToAction("Index");
            }

            return View(pacienteDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _pacienteService.GetPacienteById(id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegistroPacienteDto pacienteDto)
        {
            if (ModelState.IsValid)
            {
                await _pacienteService.updatePaciente(pacienteDto);
                return RedirectToAction("Index");
            }

            return View(pacienteDto);
        }
    }
}
