using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class EquipoRepository : IEquipoRepository
    {
        public void Add(Equipo equipo)
        {
            throw new NotImplementedException();
        }

        public void AddPlayers(List<Jugador> jugadores)
        {
            throw new NotImplementedException();
        }

        public Equipo GetByCompeticion(Competicion competicion)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipo equipo)
        {
            throw new NotImplementedException();
        }
    }
}
