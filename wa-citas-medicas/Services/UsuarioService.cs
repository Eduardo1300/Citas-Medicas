using wa_citas_medicas.Dtos;
using wa_citas_medicas.Repositories;

namespace wa_citas_medicas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<QueryUsuarioDto>> GetAllUsuarios()
        {
            return await _usuarioRepository.GetAllUsuarios();
        }

        public async Task<bool> RegistrarUsuario(RegistroUsuarioDto usuarioDto)
        {
            return await _usuarioRepository.RegistrarUsuario(usuarioDto);
        }

        public async Task<QueryUsuarioDto?> ValidarUsuario(string nomusuario, string password)
        {
            return await _usuarioRepository.ValidarUsuario(nomusuario, password);
        }
    }
}
