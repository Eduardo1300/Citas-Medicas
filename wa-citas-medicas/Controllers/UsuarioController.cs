using Microsoft.AspNetCore.Mvc;
using wa_citas_medicas.Services;
using wa_citas_medicas.Dtos;

namespace wa_citas_medicas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        

        [HttpGet]
        [Route("Usuario/Registrar")]
        public IActionResult FormRegistrarUsuario()
        {
            return View("Registrar");
        }

        [HttpPost]
        [Route("Usuario/Registrar")]
        public async Task<IActionResult> RegistrarUsuario(RegistroUsuarioDto dto)
        {
            if (!ModelState.IsValid)
                return View("Registrar", dto);

            var registrado = await _usuarioService.RegistrarUsuario(dto);

            if (!registrado)
            {
                ViewBag.ErrorMessage = "Hubo un error al registrar el usuario.";
                return View("Registrar", dto);
            }
            return RedirectToAction("Login", "Usuario");
        }

        [HttpPost]
        [Route("Usuario/Validar")]
        public async Task<IActionResult> ValidarUsuario(string nomusuario, string password)
        {
            var usuario = await _usuarioService.ValidarUsuario(nomusuario, password);

            if (usuario == null)
            {
                ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                return View("Login");
            }

            TempData["UsuarioLogueado"] = usuario.Nomusuario;

            return RedirectToAction("MenuPrincipal", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}
