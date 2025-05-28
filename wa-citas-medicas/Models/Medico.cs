using System;
using System.Collections.Generic;

namespace wa_citas_medicas.Models;

public partial class Medico
{
    public int Medicoid { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string Especialidad { get; set; } = null!;

    public virtual ICollection<Citamedica> Citamedicas { get; set; } = new List<Citamedica>();
}
