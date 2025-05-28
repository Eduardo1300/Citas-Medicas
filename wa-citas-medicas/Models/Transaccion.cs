using System;
using System.Collections.Generic;

namespace wa_citas_medicas.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public int CuentaOrigenId { get; set; }

    public int CuentaDestinoId { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Cuentum CuentaDestino { get; set; } = null!;

    public virtual Cuentum CuentaOrigen { get; set; } = null!;
}
