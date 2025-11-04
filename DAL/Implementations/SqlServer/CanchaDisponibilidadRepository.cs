using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Implementations.SqlServer.Adapters;
using DAL.Implementations.SqlServer.Helper;
using DAL.Interfaces;
using DomainModel;

namespace DAL.Implementations.SqlServer
{
    /// <summary>
    /// Repositorio SQL para gestionar la plantilla de disponibilidad horaria de las canchas
    /// (<see cref="CanchaDisponibilidad"/>).
    /// </summary>
    /// <remarks>
    /// Esta clase hereda de <see cref="SqlTransactRepository"/>, lo que significa
    /// que opera dentro de una transacción y conexión SQL existente,
    /// gestionada por una Unidad de Trabajo (Unit of Work).
    /// </remarks>
    public class CanchaDisponibilidadRepository : SqlTransactRepository, ICanchaDisponibilidadRepository
    {
        public CanchaDisponibilidadRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }
        private const string _sqlSelect = @"SELECT
                IdDisponibilidad, IdCancha, DiaSemana, HoraInicio, HoraFin
            FROM dbo.DbCanchaDisponibilidadSemanal";


        /// <summary>
        /// Agrega una nueva franja de disponibilidad (plantilla horaria) a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="CanchaDisponibilidad"/> a insertar.</param>
        public void Add(CanchaDisponibilidad entity)
        {
            string sql = @"INSERT INTO dbo.DbCanchaDisponibilidadSemanal
                           (IdDisponibilidad, IdCancha, DiaSemana, HoraInicio, HoraFin)
                           VALUES
                           (@IdDisp, @IdCancha, @Dia, @Inicio, @Fin)";

           
            int diaSemanaInt = (int)entity.DiaSemana;

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdDisp", entity.IdDisponibilidad),
                new SqlParameter("@IdCancha", entity.IdCancha),
                new SqlParameter("@Dia", diaSemanaInt),
                new SqlParameter("@Inicio", entity.HoraInicio), 
                new SqlParameter("@Fin", entity.HoraFin)      
            );
        }

        /// <summary>
        /// Elimina *todas* las plantillas de disponibilidad asociadas a una cancha específica.
        /// </summary>
        /// <param name="idCancha">El ID de la cancha cuya disponibilidad se va a borrar.</param>
        public void DeleteByCancha(Guid idCancha)
        {
            string sql = "DELETE FROM dbo.DbCanchaDisponibilidadSemanal WHERE IdCancha = @IdCancha";
            base.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@IdCancha", idCancha));
        }


        /// <summary>
        /// Obtiene la lista completa de plantillas de disponibilidad (horarios comerciales)
        /// para una cancha específica, ordenadas por día y hora.
        /// </summary>
        /// <param name="idCancha">El ID de la cancha a consultar.</param>
        /// <returns>Una lista de <see cref="CanchaDisponibilidad"/>.</returns>
        public List<CanchaDisponibilidad> GetByCancha(Guid idCancha)
        {
            var list = new List<CanchaDisponibilidad>();
            string sql = $"{_sqlSelect} WHERE IdCancha = @IdCancha ORDER BY DiaSemana, HoraInicio";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCancha", idCancha)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CanchaDisponibilidadAdapter.Current.Get(values));
                }
            }
            return list;
        }
    }
}
