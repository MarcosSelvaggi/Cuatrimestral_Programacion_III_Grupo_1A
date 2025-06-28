using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

//Clase auxiliar para el manejo de las localidades en los drop down list
namespace Dominio
{
    public class Localidades
    {
        public string Nombre { get; set; }    
    }

    public class ListaDeLocalidades
    {
        public List<Localidades> Localidades { get; set; }
    }
}

