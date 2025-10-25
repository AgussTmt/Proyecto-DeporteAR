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
    public class CanchaDisponibilidadRepository : SqlTransactRepository, ICanchaDisponibilidadRepository
    {
        public CanchaDisponibilidadRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }
        private const string _sqlSelect = @"SELECT
                IdDisponibilidad, IdCancha, DiaSemana, HoraInicio, HoraFin
            FROM dbo.DbCanchaDisponibilidadSemanal";

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

        public void DeleteByCancha(Guid idCancha)
        {
            string sql = "DELETE FROM dbo.DbCanchaDisponibilidadSemanal WHERE IdCancha = @IdCancha";
            base.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@IdCancha", idCancha));
        }

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
