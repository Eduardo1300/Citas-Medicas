using wa_citas_medicas.Dtos;

namespace wa_citas_medicas.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<QueryUsuarioDto>> GetAllUsuarios(); 
        Task<QueryUsuarioDto?> ValidarUsuario(string nomusuario, string password); 
        Task<bool> RegistrarUsuario(RegistroUsuarioDto usuarioDto); 
    }
}
