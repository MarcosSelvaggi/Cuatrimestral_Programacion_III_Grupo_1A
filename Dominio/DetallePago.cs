using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DetallePago
{
    public int IdDetallePago { get; set; }
    public string Metodo { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; }
    public string Descripcion { get; set; }
}