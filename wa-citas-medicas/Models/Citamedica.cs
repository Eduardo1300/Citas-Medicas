using System;
using System.Collections.Generic;

namespace wa_citas_medicas.Models;

public partial class Citamedica
{
    public int Citaid { get; set; }

    public DateTime Fecha { get; set; }

    public int Pacienteid { get; set; }

    public int Medicoid { get; set; }

    public virtual Medico Medico { get; set; } = null!;

    public virtual Paciente Paciente { get; set; } = null!;
}
