using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.DomainModel;

namespace WinUI.WinForms.Gestiones.Settings
{
    public static class UsuarioExtensions
    {
        public static List<string> GetPatentesAgrupadasPorRol(this Usuario usuario)
        {
            var salida = new List<string>();

            foreach (var familia in usuario.Privilegios.OfType<Familia>())
            {
                salida.Add($"Rol: {familia.Nombre} {(familia.Habilitado ? "[Habilitado]" : "[Deshabilitado]")}");
                RecorrerFamilia(familia, familia.Habilitado, salida, "   ");
                salida.Add(string.Empty);
            }

            return salida;
        }

        private static void RecorrerFamilia(Familia familia, bool habilitadoPadre, List<string> salida, string indent)
        {
            foreach (var hijo in familia.GetHijos())
            {
                bool habilitado = hijo.Habilitado && habilitadoPadre;

                if (hijo is Patente patente)
                {
                    salida.Add($"{indent}- {patente.DataKey} {(habilitado ? "[Habilitada]" : "[Deshabilitada]")}");
                }
                else if (hijo is Familia subFamilia)
                {
                    salida.Add($"{indent}SubRol: {subFamilia.Nombre} {(habilitado ? "[Habilitado]" : "[Deshabilitado]")}");
                    RecorrerFamilia(subFamilia, habilitado, salida, indent + "   ");
                }
            }
        }
    }
}
