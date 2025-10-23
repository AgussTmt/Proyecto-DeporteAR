using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class EquipoAdapter
    {
        private readonly static EquipoAdapter _instance = new EquipoAdapter();
        public static EquipoAdapter Current { get { return _instance; } }
        private EquipoAdapter() { }

        /// <summary>
        /// Mapea un array de object[] a una entidad Equipo.
        /// Asume el orden: 0:IdEquipo, 1:CantAusencias, 2:FechaDeCreacion, 
        /// 3:Nombre, 4:IdCliente (para Capitan), 5:EstadoAsistenciaDesc (para Enum)
        /// </summary>
        public Equipo Get(object[] values)
        {
            if (values == null || values.Length < 6)
                throw new ArgumentException("Se requieren 6 valores para mapear un Equipo.");

            Enum.TryParse(values[5]?.ToString(), out EstadoAsistencia estado);

            return new Equipo
            {
                IdEquipo = (Guid)values[0],
                CantAusencias = values[1] == DBNull.Value ? 0 : (int)values[1],
                FechaCreacion = (DateTime)values[2],
                Nombre = values[3]?.ToString(),
                Capitan = (values[4] == DBNull.Value)
                            ? null
                            : new Cliente { IdCliente = (Guid)values[4] },
                EstadoProxPartido = estado,
                Jugadores = new List<Jugador>() // Se llena después
            };
        }
    }
}
