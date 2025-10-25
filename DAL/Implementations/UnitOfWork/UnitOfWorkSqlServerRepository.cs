using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DAL.Interfaces;
using System.Data.SqlClient;

namespace Patrones_3parcial.UnitOfWork.Implementaciones.UnitOfWork
{
    internal class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {
        public ICanchaRepository CanchaRepository { get; }

        public IClienteRepository ClienteRepository { get; }

        public ICompeticionRepository CompeticionRepository { get; }

        public IEquipoRepository EquipoRepository { get; }

        public IJugadorRepository JugadorRepository { get; }

        public ICanchaHorarioRepository CanchaHorarioRepository { get; }

        public IFixtureRepository FixtureRepository { get; }

        public IClasificacionRepository ClasificacionRepository { get; }

        public ICatalogRepository CatalogRepository { get; }

        public ICanchaDisponibilidadRepository CanchaDisponibilidadRepository { get; }

        public IReservaHistorialRepository ReservaHistorialRepository { get; }

        private string CanchaDao = ConfigurationManager.AppSettings["CanchaRepository"];
        private string ClienteDao = ConfigurationManager.AppSettings["ClienteRepository"];
        private string CompeticionDao = ConfigurationManager.AppSettings["CompeticionRepository"];
        private string EquipoDao = ConfigurationManager.AppSettings["EquipoRepository"];
        private string JugadorDao = ConfigurationManager.AppSettings["JugadorRepository"];
        private string CanchaHorarioDao = ConfigurationManager.AppSettings["CanchaHorarioRepository"];
        private string FixtureDao = ConfigurationManager.AppSettings["FixtureRepository"];
        private string ClasificacionDao = ConfigurationManager.AppSettings["ClasificacionRepository"];
        private string CatalogDao = ConfigurationManager.AppSettings["CatalogRepository"];
        private string CanchaDisponibilidadDao = ConfigurationManager.AppSettings["CanchaDisponibilidadRepository"];
        private string ReservaHistorialDao = ConfigurationManager.AppSettings["ReservaHistorialRepository"];
        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction)
        {
            Type canchaType = Type.GetType(CanchaDao);
            var canchaInstance = Activator.CreateInstance(canchaType, new object[] { context, transaction });
            CanchaRepository = (ICanchaRepository)canchaInstance;

            
            Type clienteType = Type.GetType(ClienteDao);
            var clienteInstance = Activator.CreateInstance(clienteType, new object[] { context, transaction });
            ClienteRepository = (IClienteRepository)clienteInstance;

            
            Type competicionType = Type.GetType(CompeticionDao);
            var competicionInstance = Activator.CreateInstance(competicionType, new object[] { context, transaction });
            CompeticionRepository = (ICompeticionRepository)competicionInstance;

            
            Type equipoType = Type.GetType(EquipoDao);
            var equipoInstance = Activator.CreateInstance(equipoType, new object[] { context, transaction });
            EquipoRepository = (IEquipoRepository)equipoInstance;

            
            Type jugadorType = Type.GetType(JugadorDao);
            var jugadorInstance = Activator.CreateInstance(jugadorType, new object[] { context, transaction });
            JugadorRepository = (IJugadorRepository)jugadorInstance;

            
            Type canchaHorarioType = Type.GetType(CanchaHorarioDao);
            var canchaHorarioInstance = Activator.CreateInstance(canchaHorarioType, new object[] { context, transaction });
            CanchaHorarioRepository = (ICanchaHorarioRepository)canchaHorarioInstance;


            Type fixtureType = Type.GetType(FixtureDao);
            FixtureRepository = (IFixtureRepository)Activator.CreateInstance(fixtureType, new object[] { context, transaction });


            Type clasificacionType = Type.GetType(ClasificacionDao);
            ClasificacionRepository = (IClasificacionRepository)Activator.CreateInstance(clasificacionType, new object[] { context, transaction });

            Type catalogType = Type.GetType(CatalogDao);
            CatalogRepository = (ICatalogRepository)Activator.CreateInstance(catalogType, new object[] { context, transaction });

            Type canchaDisponibilidadType = Type.GetType(CanchaDisponibilidadDao);
            CanchaDisponibilidadRepository = (ICanchaDisponibilidadRepository)Activator.CreateInstance(canchaDisponibilidadType, new object[] { context, transaction });

            Type historialType = Type.GetType(ReservaHistorialDao);
            ReservaHistorialRepository = (IReservaHistorialRepository)Activator.CreateInstance(historialType, new object[] { context, transaction });

        }


    }
}

