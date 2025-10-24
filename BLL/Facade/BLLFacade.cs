using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Facade.Interfaces;
using BLL.Interfaces;
using BLL.Services;

namespace BLL.Facade
{
    internal class BLLFacade : IBLLFacade
    {

        private static readonly IBLLFacade _instance = new BLLFacade();

        public static IBLLFacade Current
        {
            get { return _instance; }
        }
        public ICanchaService CanchaService { get; }
        public ICanchaHorarioService CanchaHorarioService { get; }
        public IClienteService ClienteService { get; }
        public ICompeticionService CompeticionService { get; }
        public IEquipoService EquipoService { get; }
        public IFixtureService FixtureService { get; }
        public IJugadorService JugadorService { get; }
        public IClasificacionService ClasificacionService { get; }


        private BLLFacade()
        {
            CanchaService = new CanchaService();
            CanchaHorarioService = new CanchaHorarioService();
            ClienteService = new ClienteService();
            CompeticionService = new CompeticionService();
            EquipoService = new EquipoService();
            FixtureService = new FixtureService();
            JugadorService = new JugadorService();
            ClasificacionService = new ClasificacionService();
        }
    }
}
