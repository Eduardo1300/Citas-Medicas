using wa_citas_medicas.Dtos;

namespace wa_citas_medicas.Services
{
    public interface IUsuarioService
    {
        Task<List<QueryUsuarioDto>> GetAllUsuarios();
        Task<QueryUsuarioDto?> ValidarUsuario(string nomusuario, string password);
        Task<bool> RegistrarUsuario(RegistroUsuarioDto usuarioDto);
    }
}
