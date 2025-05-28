using System;
using System.Collections.Generic;

namespace wa_citas_medicas.Models;

public partial class Paciente
{
    public int Pacienteid { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public string? Telefono { get; set; }

    public bool? Estado { get; set; }

    public string Dni { get; set; } = null!;

    public virtual ICollection<Citamedica> Citamedicas { get; set; } = new List<Citamedica>();
}
