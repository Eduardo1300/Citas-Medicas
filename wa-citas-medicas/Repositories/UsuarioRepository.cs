using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using wa_citas_medicas.Data;
using wa_citas_medicas.Dtos;

namespace wa_citas_medicas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QueryUsuarioDto>> GetAllUsuarios()
        {
            var usuarios = await _context.Set<QueryUsuarioDto>()
                .FromSqlRaw("EXEC listadousuarios")
                .ToListAsync();

            return usuarios;
        }

        public async Task<bool> RegistrarUsuario(RegistroUsuarioDto usuarioDto)
        {
            try
            {
                var nomusuario = new SqlParameter("@Nomusuario", usuarioDto.Nomusuario);
                var email = new SqlParameter("@Email", usuarioDto.Email);
                var password = new SqlParameter("@Password", usuarioDto.Password);
                var nombres = new SqlParameter("@Nombres", usuarioDto.Nombres);
                var apellidos = new SqlParameter("@Apellidos", usuarioDto.Apellidos);
                var activo = new SqlParameter("@Activo", usuarioDto.Activo);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC registrarusuario @Nomusuario, @Email, @Password, @Nombres, @Apellidos, @Activo",
                    nomusuario, email, password, nombres, apellidos, activo
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar usuario: " + ex.Message);
                return false;
            }
        }

        public async Task<QueryUsuarioDto?> ValidarUsuario(string nomusuario, string password)
        {
            var nomusuarioParam = new SqlParameter("@Nomusuario", nomusuario);
            var passwordParam = new SqlParameter("@Password", password);

            var usuario = _context.Set<QueryUsuarioDto>()
                .FromSqlRaw("EXEC validarusuario @Nomusuario, @Password", nomusuarioParam, passwordParam)
                .AsEnumerable() // 👈 fuerza ejecución inmediata, evita que EF trate de componer
                .FirstOrDefault();

            return usuario;
        }


    }
}
