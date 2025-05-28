namespace wa_citas_medicas.Dtos
{
    public class QueryUsuarioDto
    {
        public int Idusuario { get; set; }
        public string Nomusuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public bool Activo { get; set; }
    }
}
