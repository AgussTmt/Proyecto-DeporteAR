using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.WinForms.Gestiones.Settings
{
    public class PatenteDeFamilia
    {
        public string Rol { get; set; }
        public string Patente { get; set; }
        public bool Habilitada { get; set; }

        public override string ToString()
        {
            return $"{Patente} - {(Habilitada ? "Habilitada" : "Deshabilitada")} (Rol: {Rol})";
        }
    }
}
