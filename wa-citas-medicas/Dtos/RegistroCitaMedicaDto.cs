namespace wa_citas_medicas.Dtos
{
    public class RegistroCitaMedicaDto
    {
        public int Citaid { get; set; }
        public DateTime Fecha { get; set; }
        public int Pacienteid { get; set; }
        public int Medicoid { get; set; }
    }
}
