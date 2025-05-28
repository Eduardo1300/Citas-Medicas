using Microsoft.AspNetCore.Mvc;
using wa_citas_medicas.Dtos;
using wa_citas_medicas.Services;

namespace wa_citas_medicas.Controllers
{
    public class MedicoController : Controller
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var medicos = await _medicoService.GetAllMedicos();
            return View(medicos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new RegistroMedicoDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistroMedicoDto medicoDto)
        {
            if (ModelState.IsValid)
            {
                await _medicoService.saveMedico(medicoDto);
                return RedirectToAction("Index");
            }

            return View(medicoDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var medico = await _medicoService.GetMedicoById(id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegistroMedicoDto medicoDto)
        {
            if (ModelState.IsValid)
            {
                await _medicoService.updateMedico(medicoDto);
                return RedirectToAction("Index");
            }

            return View(medicoDto);
        }
    }
}
