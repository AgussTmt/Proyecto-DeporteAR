using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    internal class ReservaHistorialRepository : SqlTransactRepository, IReservaHistorialRepository
    {
        public ReservaHistorialRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        public void Add(ReservaHistorial entity)
        {
            string sql = @"INSERT INTO dbo.DbReservaHistorial
                           (IdHistorial, IdCanchaHorario, IdCliente, FechaHoraEvento, EstadoAnterior, EstadoNuevo, Detalle)
                           VALUES
                           (@IdH, @IdCH, @IdCli, @Fecha, @EstadoAnt, @EstadoNue, @Detalle)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdH", entity.IdHistorial),
                new SqlParameter("@IdCH", entity.IdCanchaHorario),
                // Manejar posible Guid? nulo para IdCliente
                new SqlParameter("@IdCli", (object)entity.IdCliente ?? DBNull.Value),
                new SqlParameter("@Fecha", entity.FechaHoraEvento),
                new SqlParameter("@EstadoAnt", (object)entity.EstadoAnterior ?? DBNull.Value),
                new SqlParameter("@EstadoNue", entity.EstadoNuevo),
                new SqlParameter("@Detalle", (object)entity.Detalle ?? DBNull.Value)
            );
        }
    }
}
