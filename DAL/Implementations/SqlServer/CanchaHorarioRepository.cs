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
    /// Repositorio SQL para gestionar las reservas y horarios (<see cref="CanchaHorario"/>).
    /// Es la clase principal para interactuar con la grilla de turnos.
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class CanchaHorarioRepository : SqlTransactRepository, ICanchaHorarioRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public CanchaHorarioRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {

        }

        private const string _sqlSelect = @"SELECT 
                    ch.[IdCancha-Horario], ch.IdCancha, ch.Horario, ch.IdCliente, 
                    ch.Abonada, ch.FueCambiada, e.Descripcion
                FROM [DbCancha Horario] ch
                LEFT JOIN DbEstadoReserva e ON ch.IdEstadoReserva = e.IdEstadoReserva";

        /// <summary>
        /// Agrega un nuevo registro de <see cref="CanchaHorario"/> (un turno) a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="CanchaHorario"/> a insertar.</param>
        public void Add(CanchaHorario entity)
        {
            
            Guid idEstado = GetEstadoReservaId(entity.Estado);
            Guid? idCliente = entity.ReservadaPor?.IdCliente; 
            Guid idCancha = entity.Cancha.IdCancha;

            
            string sql = @"INSERT INTO [DbCancha Horario] 
                           (IdCancha, Horario, IdCliente, Abonada, FueCambiada, IdEstadoReserva, [IdCancha-Horario])
                           VALUES
                           (@IdCancha, @Horario, @IdCliente, @Abonada, @FueCambiada, @IdEstadoReserva, @IdCanchaHorario)";

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

        /// <summary>
        /// Obtiene todos los registros de <see cref="CanchaHorario"/> de la base de datos.
        /// </summary>
        /// <returns>Una colección de <see cref="CanchaHorario"/>.</returns>
        public IEnumerable<CanchaHorario> GetAll()
        {
            var horarios = new List<CanchaHorario>();
            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text))
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

        /// <summary>
        /// Obtiene todos los horarios que se encuentran en un estado específico
        /// (ej: 'Libre', 'Reservada').
        /// </summary>
        /// <param name="estadoReserva">El <see cref="EstadoReserva"/> (enum) a filtrar.</param>
        /// <returns>Una lista de <see cref="CanchaHorario"/>.</returns>
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

        /// <summary>
        /// Obtiene un <see cref="CanchaHorario"/> específico por su clave primaria.
        /// </summary>
        /// <param name="id">El ID (<c>IdCancha-Horario</c>) de la reserva.</param>
        /// <returns>La <see cref="CanchaHorario"/> encontrada, o <c>null</c> si no existe.</returns>

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

        /// <summary>
        /// Obtiene todos los horarios (turnos) para una fecha específica.
        /// La consulta filtra por DÍA (ignorando la hora de <paramref name="dateTime"/>).
        /// </summary>
        /// <param name="dateTime">La fecha a consultar.</param>
        /// <returns>Una lista de <see cref="CanchaHorario"/> para esa fecha.</returns>
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

        /// <summary>
        /// Obtiene todos los horarios ordenados por demanda (basado en la columna 'CantReservas').
        /// </summary>
        /// <returns>Una lista de <see cref="CanchaHorario"/> ordenada.</returns>
        /// <remarks>
        /// Esta consulta asume que la tabla [DbCancha Horario] tiene una columna
        /// llamada 'CantReservas' que se usa para ordenar, aunque no se selecciona
        /// en el <c>_sqlSelect</c> base.
        /// </remarks>
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

        /// <summary>
        /// Actualiza un registro de <see cref="CanchaHorario"/> existente en la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="CanchaHorario"/> con los datos modificados.</param>
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

        /// <summary>
        /// Método de ayuda (privado) para convertir un <see cref="EstadoReserva"/> (enum)
        /// a su <see cref="Guid"/> correspondiente, consultando la tabla 'DbEstadoReserva'.
        /// </summary>
        /// <param name="estado">El enum a convertir.</param>
        /// <returns>El <see cref="Guid"/> del estado.</returns>
        /// <exception cref="InvalidOperationException">Si el estado no existe en la tabla.</exception>
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


        /// <summary>
        /// Obtiene el turno *exacto* para una cancha y una fecha/hora específicas.
        /// </summary>
        /// <param name="idCancha">El ID de la <see cref="Cancha"/>.</param>
        /// <param name="hora">La fecha y hora exactas del turno.</param>
        /// <returns>El <see cref="CanchaHorario"/> encontrado, o <c>null</c>.</returns>
        public CanchaHorario GetByCanchaYHora(Guid idCancha, DateTime hora)
        {
            string sql = $"{_sqlSelect} WHERE ch.IdCancha = @IdCancha AND ch.Horario = @Hora";
            CanchaHorario horario = null;

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@Hora", hora)))
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


        /// <summary>
        /// Obtiene la fecha y hora del último turno (máxima) reservado para una cancha,
        /// excluyendo los turnos ocupados por torneos.
        /// </summary>
        /// <param name="idCancha">El ID de la <see cref="Cancha"/>.</param>
        /// <returns>La <see cref="DateTime"/> máxima, o <see cref="DateTime.MinValue"/> si no hay horarios.</returns>
        public DateTime GetMaximaFechaHorario(Guid idCancha)
        {
            Guid idEstadoTorneo = GetEstadoReservaId(EstadoReserva.OcupadoPorTorneo);
            string sql = @"SELECT MAX(Horario) 
                   FROM [DbCancha Horario] 
                   WHERE IdCancha = @IdCancha 
                   AND IdEstadoReserva != @IdEstadoTorneo";


            object result = base.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@IdEstadoTorneo", idEstadoTorneo));


            if (result == null || result == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            
            return Convert.ToDateTime(result);
        }


        /// <summary>
        /// Verifica si ya existe un turno creado (un <c>CanchaHorario</c>) para una
        /// cancha en una fecha y hora exactas.
        /// </summary>
        /// <param name="idCancha">El ID de la <see cref="Cancha"/>.</param>
        /// <param name="fechaHora">La fecha y hora exactas del turno a verificar.</param>
        /// <returns><c>true</c> si el turno ya existe, <c>false</c> en caso contrario.</returns>
        public bool ExisteHorario(Guid idCancha, DateTime fechaHora)
        {
            string sql = @"SELECT COUNT(*)
                           FROM [DbCancha Horario]
                           WHERE IdCancha = @IdCancha AND Horario = @FechaHora";

            
            object result = base.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@FechaHora", fechaHora));


            int count = Convert.ToInt32(result ?? 0);
            return count > 0;
        }


        /// <summary>
        /// Obtiene todos los turnos de una cancha específica dentro de un rango de fechas.
        /// </summary>
        /// <param name="idCancha">El ID de la <see cref="Cancha"/>.</param>
        /// <param name="fechaDesde">La fecha/hora de inicio (inclusiva).</param>
        /// <param name="fechaHasta">La fecha/hora de fin (exclusiva).</param>
        /// <returns>Una colección de <see cref="CanchaHorario"/>.</returns>
        public IEnumerable<CanchaHorario> GetHorariosRango(Guid idCancha, DateTime fechaDesde, DateTime fechaHasta)
        {
            string sql = $"{_sqlSelect} WHERE ch.IdCancha = @IdCancha " +
                   "AND ch.Horario >= @FechaDesde " +
                   "AND ch.Horario < @FechaHasta";

            var lista = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@FechaDesde", fechaDesde),
                new SqlParameter("@FechaHasta", fechaHasta)
            ))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    // El adapter solo mapea las 8 columnas
                    lista.Add(CanchaHorarioAdapter.Current.Get(values));
                }
            }
            return lista;




        }


        /// <summary>
        /// Cuenta cuántos turnos futuros (desde ahora en adelante) para una cancha
        /// están en estado 'Reservada', 'Espera' u 'OcupadoPorTorneo'.
        /// </summary>
        /// <param name="idCancha">El ID de la <see cref="Cancha"/>.</param>
        /// <returns>El número total de turnos "no libres" a futuro.</returns>
        public int CountSlotsOcupadosFuturos(Guid idCancha)
        {
            // Obtenemos los IDs de los estados "no-libres"
            Guid idEstadoReservada = GetEstadoReservaId(EstadoReserva.Reservada);
            Guid idEstadoEspera = GetEstadoReservaId(EstadoReserva.Espera);
            Guid idEstadoTorneo = GetEstadoReservaId(EstadoReserva.OcupadoPorTorneo);

            string sql = @"SELECT COUNT(*) 
                   FROM [DbCancha Horario] 
                   WHERE IdCancha = @IdCancha 
                   AND Horario >= GETDATE()
                   AND IdEstadoReserva IN (@Reservada, @Espera, @Torneo)";

            object result = base.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@IdCancha", idCancha),
                new SqlParameter("@Reservada", idEstadoReservada),
                new SqlParameter("@Espera", idEstadoEspera),
                new SqlParameter("@Torneo", idEstadoTorneo)
            );

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }
            return 0;
        }


        /// <summary>
        /// Cuenta cuántas reservas activas (en estado 'Reservada' y a futuro)
        /// tiene un cliente específico.
        /// </summary>
        /// <param name="idCliente">El ID del <see cref="Cliente"/>.</param>
        /// <returns>El número de reservas activas.</returns>
        public int CountReservasActivasByCliente(Guid idCliente)
        {
            Guid idEstadoReservada = GetEstadoReservaId(EstadoReserva.Reservada);

            string sql = @"SELECT COUNT(*) 
                   FROM [DbCancha Horario] 
                   WHERE IdCliente = @IdCliente 
                   AND Horario >= GETDATE()
                   AND IdEstadoReserva = @Reservada";

            object result = base.ExecuteScalar(sql, CommandType.Text,
                new SqlParameter("@IdCliente", idCliente),
                new SqlParameter("@Reservada", idEstadoReservada)
            );

            return (int)(result ?? 0);
        }


        /// <summary>
        /// Obtiene una lista de todos los turnos (reservas) anteriores a una fecha
        /// que están en estado 'Reservada' pero que figuran como 'No Abonada'.
        /// </summary>
        /// <param name="fechaLimite">La fecha/hora límite (los turnos anteriores a esta fecha).</param>
        /// <returns>Una colección de <see cref="CanchaHorario"/> (deudores).</returns>
        public IEnumerable<CanchaHorario> GetDeudores(DateTime fechaLimite)
        {

            string sql = $"{_sqlSelect} " +
                         "WHERE ch.Horario < @FechaLimite " +
                         "AND ch.Abonada = 0 " +
                         "AND ch.IdEstadoReserva = @IdEstadoReservada";

            Guid idEstadoReservada = GetEstadoReservaId(EstadoReserva.Reservada);
            var horarios = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@FechaLimite", fechaLimite),
                new SqlParameter("@IdEstadoReservada", idEstadoReservada)))
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


        /// <summary>
        /// Obtiene un informe de todos los turnos que fueron 'Abonados' (pagados)
        /// dentro de un rango de fechas, opcionalmente filtrado por una cancha.
        /// </summary>
        /// <param name="desde">Fecha/hora de inicio del reporte.</param>
        /// <param name="hasta">Fecha/hora de fin del reporte.</param>
        /// <param name="idCancha">Opcional. Si se provee, filtra por esta cancha.</param>
        /// <returns>Una colección de <see cref="CanchaHorario"/> pagados.</returns>
        public IEnumerable<CanchaHorario> GetHorariosAbonadosRango(DateTime desde, DateTime hasta, Guid? idCancha)
        {
            
            string sql = $"{_sqlSelect} " +
                         "WHERE ch.Horario >= @Desde " +
                         "AND ch.Horario <= @Hasta " +
                         "AND ch.Abonada = 1 " + 
                         "AND (@IdCancha IS NULL OR ch.IdCancha = @IdCancha)"; 

            var horarios = new List<CanchaHorario>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@Desde", desde),
                new SqlParameter("@Hasta", hasta),
                new SqlParameter("@IdCancha", (object)idCancha ?? DBNull.Value)
            ))
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

        public void CambiarHabilitado(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
    

