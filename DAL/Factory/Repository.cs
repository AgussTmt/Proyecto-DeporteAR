using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer;

namespace DAL.Factory
{
    public static class Repository
    {
        static string backendType = ConfigurationManager.AppSettings["backendType"];

        public static ICanchaRepository GetCanchaInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.CanchaRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.CanchaRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static ICanchaHorarioRepository GetCanchaHorarioInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.CanchaHorarioRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.CanchaHorarioRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static IClasificacionRepository GetClasificacionInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.ClasificacionRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.ClasificacionRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static IClienteRepository GetClienteInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.ClienteRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.ClienteRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static ICompeticionRepository GetCompeticionInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.CompeticionRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.CompeticionRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static IEquipoRepository GetEquipoInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.EquipoRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.EquipoRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static IFixtureRepository GetFixtureInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.FixtureRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.FixtureRepository();
            }
            throw new Exception("PROBLEMAS");
        }


        public static IJugadorRepository GetJugadorInstance()
        {
            if (backendType == "memory")
            {
                //return new Dal.Implementations.Memory.JugadorRepository();
            }
            else if (backendType == "sqlserver")
            {
                return new DAL.Implementations.SqlServer.JugadorRepository();
            }
            throw new Exception("PROBLEMAS");
        }



    }
}
