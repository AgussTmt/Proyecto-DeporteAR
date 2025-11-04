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
    /// <summary>
    /// Repositorio SQL para gestionar los registros de auditoría (logs) de las reservas
    /// (la entidad <see cref="ReservaHistorial"/>).
    /// </summary>
    /// <remarks>
    /// Este es un repositorio "append-only" (solo agregar). No expone métodos
    /// de actualización o eliminación, lo cual es correcto para una tabla de log.
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class ReservaHistorialRepository : SqlTransactRepository, IReservaHistorialRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public ReservaHistorialRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        /// <summary>
        /// Agrega un nuevo evento de historial (un log) a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="ReservaHistorial"/> a insertar.</param>
        public void Add(ReservaHistorial entity)
        {
            string sql = @"INSERT INTO dbo.DbReservaHistorial
                           (IdHistorial, IdCanchaHorario, IdCliente, FechaHoraEvento, EstadoAnterior, EstadoNuevo, Detalle)
                           VALUES
                           (@IdH, @IdCH, @IdCli, @Fecha, @EstadoAnt, @EstadoNue, @Detalle)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdH", entity.IdHistorial),
                new SqlParameter("@IdCH", entity.IdCanchaHorario),
                new SqlParameter("@IdCli", (object)entity.IdCliente ?? DBNull.Value),
                new SqlParameter("@Fecha", entity.FechaHoraEvento),
                new SqlParameter("@EstadoAnt", (object)entity.EstadoAnterior ?? DBNull.Value),
                new SqlParameter("@EstadoNue", entity.EstadoNuevo),
                new SqlParameter("@Detalle", (object)entity.Detalle ?? DBNull.Value)
            );
        }
    }
}