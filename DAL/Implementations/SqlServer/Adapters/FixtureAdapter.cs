using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Implementations.SqlServer.Adapters
{
    internal class FixtureAdapter
    {
        private readonly static FixtureAdapter _instance = new FixtureAdapter();
        public static FixtureAdapter Current { get { return _instance; } }
        private FixtureAdapter() { }

        /// <summary>
        /// Mapea un array de object[] a una entidad Fixture.
        /// Asume el orden: 0:IdFixture, 1:IdCompeticion, 2:Estado (string), 
        /// 3:Resultado, 4:Horario
        /// </summary>
        public Fixture Get(object[] values)
        {
            if (values == null || values.Length < 5)
            {
                throw new ArgumentException("Se requieren 5 valores para mapear un Fixture.");
            }

            Enum.TryParse(values[2]?.ToString(), out EstadoFixture estado);

            return new Fixture
            {
                IdFixture = (Guid)values[0],
                IdCompeticion = (Guid)values[1],
                Estado = estado,
                Resultado = values[3]?.ToString(),
                CanchaHorario = new CanchaHorario { IdCanchaHorario = (Guid)values[4] },
                Equipos = new List<Equipo>() 
            };
        }
    }
}
