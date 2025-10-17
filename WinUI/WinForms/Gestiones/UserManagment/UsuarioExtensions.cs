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
                if (hijo is Patente patente)
                {
                    string estado;

                    if (!habilitadoPadre)
                    {
                        // El rol está deshabilitado
                        estado = patente.Habilitado
                            ? "[Deshabilitada por Rol]"
                            : "[Deshabilitada en Rol y por Rol]";
                    }
                    else
                    {
                        // El rol está habilitado
                        estado = patente.Habilitado
                            ? "[Habilitada]"
                            : "[Deshabilitada en Rol]";
                    }

                    salida.Add($"{indent}- {patente.DataKey} {estado}");
                }
                else if (hijo is Familia subFamilia)
                {
                    bool habilitadoSubFamilia = subFamilia.Habilitado && habilitadoPadre;
                    salida.Add($"{indent}SubRol: {subFamilia.Nombre} {(habilitadoSubFamilia ? "[Habilitado]" : "[Deshabilitado]")}");
                    RecorrerFamilia(subFamilia, habilitadoSubFamilia, salida, indent + "   ");
                }
            }
        }
    }
}
    
