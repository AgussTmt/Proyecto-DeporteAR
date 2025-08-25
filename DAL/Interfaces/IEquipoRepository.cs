using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace DAL.Interfaces
{
    internal interface IEquipoRepository
    {
        void Update(Equipo equipo);

        void Add(Equipo equipo);

        Equipo GetByCompeticion(Competicion competicion);

        void AddPlayers(List<Jugador> jugadores);
    }
}
