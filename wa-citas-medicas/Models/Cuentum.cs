using System;
using System.Collections.Generic;

namespace wa_citas_medicas.Models;

public partial class Cuentum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Saldo { get; set; }

    public virtual ICollection<Transaccion> TransaccionCuentaDestinos { get; set; } = new List<Transaccion>();

    public virtual ICollection<Transaccion> TransaccionCuentaOrigens { get; set; } = new List<Transaccion>();
}
