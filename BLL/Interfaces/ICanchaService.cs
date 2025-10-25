using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using DomainModel;

namespace BLL.Interfaces
{
    //con el IGeneric cubro todo
    public interface ICanchaService
    {
        void Add(Cancha entity, Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad);
        Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> GetDisponibilidadSemanal(Guid idCancha);

        void CambiarHabilitado(Guid id);

        IEnumerable<Cancha> GetAll();

        Cancha GetById(Guid id);

        void Update(Cancha entity, Dictionary<DayOfWeek, (TimeSpan start, TimeSpan end)> disponibilidad);

        IEnumerable<Cancha> GetAllIncludingDisabled();

        bool EsHorarioValido(Guid idCancha, DateTime fechaHora);

    }
}
