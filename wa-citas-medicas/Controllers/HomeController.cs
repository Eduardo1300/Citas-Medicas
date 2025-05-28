using Microsoft.AspNetCore.Mvc;

namespace wa_citas_medicas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult MenuPrincipal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EnviarListadoPaciente()
        {
            return RedirectToAction("Index","Paciente");
        }


        [HttpPost]
        public IActionResult EnviarListadoCitaMedica()
        {
            return RedirectToAction("Index", "CitaMedica");
        }


        [HttpPost]
        public IActionResult EnviarListadoMedico()
        {
            return RedirectToAction("Index", "Medico");
        }
    }
}
