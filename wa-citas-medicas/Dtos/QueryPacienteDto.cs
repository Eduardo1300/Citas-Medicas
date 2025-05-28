namespace wa_citas_medicas.Dtos
{
    public class QueryPacienteDto
    {
        public int Pacienteid { get; set; }

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public int Edad { get; set; }

        public string? Telefono { get; set; }

        public bool? Estado { get; set; }

        public string Dni { get; set; } = null!;
    }
}
