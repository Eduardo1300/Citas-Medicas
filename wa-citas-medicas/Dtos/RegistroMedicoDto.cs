namespace wa_citas_medicas.Dtos
{
    public class RegistroMedicoDto
    {
        public int Medicoid { get; set; } 
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Especialidad { get; set; } = null!;
    }
}
