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
    internal class CanchaHorarioRepository : SqlTransactRepository, ICanchaHorarioRepository
    {
        public CanchaHorarioRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {

        }

        private const string _sqlSelect = @"SELECT 
                ch.[IdCancha-Horario], ch.IdCancha, ch.Horario, ch.IdCliente, 
                ch.Abonada, ch.FueCambiada, ch.CantReservas, e.Descripcion
            FROM [DbCancha Horario] ch
            LEFT JOIN DbEstadoReserva e ON ch.IdEstadoReserva = e.IdEstadoReserva";

        public void Add(CanchaHorario entity)
        {
            
            Guid idEstado = GetEstadoReservaId(entity.Estado);
            Guid? idCliente = entity.ReservadaPor?.IdCliente; 
            Guid idCancha = entity.Cancha.IdCancha;

            
            string sql = @"INSERT INTO [DbCancha Horario] 
                           (IdCancha, Horario, IdCliente, Abonada, FueCambiada, IdEstadoReserva, [IdCancha-Horario], CantReservas)
                           VALUES
                           (@IdCancha, @Horario, @IdCliente, @Abonada, @FueCambiada, @IdEstadoReserva, @IdCanchaHorario, 0)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@Horario", entity.FechaHorario),
                new SqlParameter("@IdCliente", (object)idCliente ?? DBNull.Value),
                new SqlParameter("@Abonada", entity.Abonada),
                new SqlParameter("@FueCambiada", entity.FueCambiada),
                new SqlParameter("@IdEstadoReserva", idEstado),
                new SqlParameter("@IdCanchaHorario", entity.IdCanchaHorario)
            );
        }

        public void AssignCliente(Cliente cliente, CanchaHorario canchaHorario)
        {
            
            Guid idEstadoReservada = GetEstadoReservaId(EstadoReserva.Reservada);

            string sql = @"UPDATE [DbCancha Horario] SET
                            IdCliente = @IdCliente,
                            IdEstadoReserva = @IdEstadoReserva,
                            CantReservas = CantReservas + 1 
                           WHERE [IdCancha-Horario] = @IdCanchaHorario";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCliente", cliente.IdCliente),
                new SqlParameter("@IdEstadoReserva", idEstadoReservada),
                new SqlParameter("@IdCanchaHorario", canchaHorario.IdCanchaHorario)
            );
        }

        public void CambiarHabilitado(Guid id)
        {
            string sql = @"UPDATE [DbCancha Horario] 
                           SET Abonada = ~Abonada 
                           WHERE [IdCancha-Horario] = @IdCanchaHorario";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCanchaHorario", id)
            );
        }


        public IEnumerable<CanchaHorario> GetAll()
        {
            var horarios = new List<CanchaHorario>();
            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text,new SqlParameter()))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    horarios.Add(CanchaHorarioAdapter.Current.Get(values));
                }
            }
            return horarios;
        }

        public List<CanchaHorario> GetByCliente(Cliente cliente)
        {
            string sql = $"{_sqlSelect} WHERE ch.IdCliente = @IdCliente";
            var horarios = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,new SqlParameter("@IdCliente", cliente.IdCliente)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    horarios.Add(CanchaHorarioAdapter.Current.Get(values));
                }
            }
            return horarios;
        }

        public List<CanchaHorario> GetByEstadoReserva(EstadoReserva estadoReserva)
        {
            string descripcion = estadoReserva.ToString();
            string sql = $"{_sqlSelect} WHERE e.Descripcion = @Descripcion";
            var horarios = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Descripcion", descripcion)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    horarios.Add(CanchaHorarioAdapter.Current.Get(values));
                }
            }
            return horarios;
        }

        public CanchaHorario GetById(Guid id)
        {
            string sql = $"{_sqlSelect} WHERE ch.[IdCancha-Horario] = @Id";
            CanchaHorario horario = null;

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    horario = CanchaHorarioAdapter.Current.Get(values);
                }
            }
            return horario;
        }

        public List<CanchaHorario> GetByTimeRange(DateTime dateTime)
        {
            
            string sql = $"{_sqlSelect} WHERE CONVERT(date, ch.Horario) = CONVERT(date, @Fecha)";
            var horarios = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Fecha", dateTime.Date)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    horarios.Add(CanchaHorarioAdapter.Current.Get(values));
                }
            }
            return horarios;
        }

        public List<CanchaHorario> GetOrderByDemand()
        {
            string sql = $"{_sqlSelect} ORDER BY ch.CantReservas DESC";
            var horarios = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    horarios.Add(CanchaHorarioAdapter.Current.Get(values));
                }
            }
            return horarios;
        }

        public void Update(CanchaHorario entity)
        {
            Guid idEstado = GetEstadoReservaId(entity.Estado);
            Guid? idCliente = entity.ReservadaPor?.IdCliente;
            Guid idCancha = entity.Cancha.IdCancha;
            

            string sql = @"UPDATE [DbCancha Horario] SET
                            IdCancha = @IdCancha,
                            Horario = @Horario,
                            IdCliente = @IdCliente,
                            Abonada = @Abonada,
                            FueCambiada = @FueCambiada,
                            IdEstadoReserva = @IdEstadoReserva
                           WHERE [IdCancha-Horario] = @IdCanchaHorario";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@Horario", entity.FechaHorario),
                new SqlParameter("@IdCliente", (object)idCliente ?? DBNull.Value),
                new SqlParameter("@Abonada", entity.Abonada),
                new SqlParameter("@FueCambiada", entity.FueCambiada),
                new SqlParameter("@IdEstadoReserva", idEstado),
                new SqlParameter("@IdCanchaHorario", entity.IdCanchaHorario)
            );
        }


        private Guid GetEstadoReservaId(EstadoReserva estado)
        {
            string descripcion = estado.ToString(); 
            string sql = "SELECT IdEstadoReserva FROM DbEstadoReserva WHERE Descripcion = @Descripcion";

            object result = base.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@Descripcion", descripcion));

            if (result == null || result == DBNull.Value)
            {
                throw new InvalidOperationException($"El estado '{descripcion}' no existe en la tabla DbEstadoReserva.");
            }
            return (Guid)result;
        }
    }
}
