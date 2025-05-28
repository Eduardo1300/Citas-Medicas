using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using wa_citas_medicas.Data;
using wa_citas_medicas.Services;
using wa_citas_medicas.Dtos;

namespace wa_citas_medicas.Controllers
{
    public class CitaMedicaController : Controller
    {
        private readonly ICitaMedicaService _citaService;
        private readonly IPacienteService _pacienteService;
        private readonly IMedicoService _medicoService;
        private readonly AppDbContext _context;


        public CitaMedicaController(ICitaMedicaService citaService, AppDbContext context,
                                    IPacienteService pacienteService, IMedicoService medicoService)
        {
            _citaService = citaService;
            _context = context;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var citas = await _citaService.GetAllCitasMedicas();
            return View(citas);
        }

        [HttpPost]
        [Route("CitaMedica/Registrar")]
        public async Task<IActionResult> RegistrarCita(RegistroCitaMedicaDto dto)
        {
            if (!ModelState.IsValid)
                return View("Registrar", dto);

            var resultado = await _citaService.saveCitaMedica(dto);

            if (!resultado)
            {
                ViewBag.ErrorMessage = "El médico ya tiene una cita para esa fecha.";

                ViewBag.Pacientes = _context.Pacientes
                    .Select(p => new SelectListItem
                    {
                        Value = p.Pacienteid.ToString(),
                        Text = $"{p.Nombre} {p.Apellido}"
                    }).ToList();

                ViewBag.Medicos = _context.Medicos
                    .Select(m => new SelectListItem
                    {
                        Value = m.Medicoid.ToString(),
                        Text = $"{m.Nombre} {m.Apellido} - {m.Especialidad}"
                    }).ToList();

                return View("Registrar", dto);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("CitaMedica/Registrar")]
        public IActionResult FormRegistrarCita()
        {
            var pacientes = _context.Pacientes
                .Select(p => new SelectListItem
                {
                    Value = p.Pacienteid.ToString(),
                    Text = $"{p.Nombre} {p.Apellido}"
                }).ToList();

            var medicos = _context.Medicos
                .Select(m => new SelectListItem
                {
                    Value = m.Medicoid.ToString(),
                    Text = $"{m.Nombre} {m.Apellido} - {m.Especialidad}"
                }).ToList();

            ViewBag.Pacientes = pacientes;
            ViewBag.Medicos = medicos;

            return View("Registrar");
        }

        [HttpGet]

        public async Task<IActionResult> EditarCita(int id)
        {
            var cita = await _citaService.GetCitaMedicaById(id);

            if (cita == null)
                return NotFound();

            
            var pacientes = await _pacienteService.GetAllPacientes();
            ViewBag.Pacientes = pacientes.Select(p => new SelectListItem
            {
                Value = p.Pacienteid.ToString(),
                Text = $"{p.Nombre} {p.Apellido}"
            }).ToList();

            var medicos = await _medicoService.GetAllMedicos();
            ViewBag.Medicos = medicos.Select(m => new SelectListItem
            {
                Value = m.Medicoid.ToString(),
                Text = $"{m.Nombre} {m.Apellido} - {m.Especialidad}"
            }).ToList();

            return View(cita); 
        }


        [HttpPost]

        public async Task<IActionResult> EditarCita(int id, RegistroCitaMedicaDto dto)
        {
            if (!ModelState.IsValid)
            {
                var pacientes = await _pacienteService.GetAllPacientes();
                ViewBag.Pacientes = pacientes.Select(p => new SelectListItem
                {
                    Value = p.Pacienteid.ToString(),
                    Text = $"{p.Nombre} {p.Apellido}"
                }).ToList();

                var medicos = await _medicoService.GetAllMedicos();
                ViewBag.Medicos = medicos.Select(m => new SelectListItem
                {
                    Value = m.Medicoid.ToString(),
                    Text = $"{m.Nombre} {m.Apellido} - {m.Especialidad}"
                }).ToList();

                return View(dto);
            }

            try
            {
                await _citaService.updateCitaMedica(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                var pacientes = await _pacienteService.GetAllPacientes();
                ViewBag.Pacientes = pacientes.Select(p => new SelectListItem
                {
                    Value = p.Pacienteid.ToString(),
                    Text = $"{p.Nombre} {p.Apellido}"
                }).ToList();

                var medicos = await _medicoService.GetAllMedicos();
                ViewBag.Medicos = medicos.Select(m => new SelectListItem
                {
                    Value = m.Medicoid.ToString(),
                    Text = $"{m.Nombre} {m.Apellido} - {m.Especialidad}"
                }).ToList();

                return View(dto);
            }
        }


    }
}