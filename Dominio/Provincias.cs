using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Dominio.Provincias;

//Clase auxiliar para el manejo de las provincias en los drop down list
namespace Dominio
{
    public class Provincias
    {
        public string Nombre { get; set; }
        
    }
    public class ListaDeProvincias
    {
        public List<Provincias> Provincias { get; set; }
    }
}
