using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class JugadorAdapter
    {
        private readonly static JugadorAdapter _instance = new JugadorAdapter();
        public static JugadorAdapter Current { get { return _instance; } }
        private JugadorAdapter() { }

        /// <summary>
        /// Mapea un array de object[] a una entidad Jugador.
        /// Asume el orden: 0:IdJugador, 1:IdEquipo, 2:Nombre, 
        /// 3:PartidosJugados, 4:Mvp, 5:Apellido
        /// </summary>
        public Jugador Get(object[] values)
        {
            if (values == null || values.Length < 6)
            {
                throw new ArgumentException("Se requieren 6 valores para mapear un Jugador.");
            }

            return new Jugador
            {
                Idjugador = (Guid)values[0],
                IdEquipo = (Guid)values[1],
                Nombre = values[2]?.ToString(),
                PartidosJugados = values[3] == DBNull.Value ? 0 : (int)values[3],
                CantMvp = values[4] == DBNull.Value ? 0 : (int)values[4], // Mapea Mvp a CantMvp
                Apellido = values[5]?.ToString()
                // Los diccionarios se llenan en el repositorio
            };
        }
    }
}
