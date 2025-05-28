using System;

namespace wa_citas_medicas.Dtos
{
    public class QueryCitaMedicaDto
    {
        public int Citaid { get; set; }
        public DateTime Fecha { get; set; }
        public string NombrePaciente { get; set; } = null!;
        public string ApellidoPaciente { get; set; } = null!;
        public string NombreMedico { get; set; } = null!;
        public string ApellidoMedico { get; set; } = null!;
        public string especialidad { get; set; } = null!;
    }
}
