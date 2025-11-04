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
    /// Repositorio SQL para gestionar las entidades <see cref="Competicion"/> (Torneos/Ligas).
    /// </summary>
    /// <remarks>
    /// Opera dentro de una transacción y conexión SQL existente (Unit of Work).
    /// </remarks>
    internal class CompeticionRepository : SqlTransactRepository, ICompeticionRepository
    {
        /// <summary>
        /// Inicializa el repositorio con el contexto de conexión y transacción de una
        /// Unidad de Trabajo (Unit of Work) existente.
        /// </summary>
        /// <param name="context">La <see cref="SqlConnection"/> activa.</param>
        /// <param name="_transaction">La <see cref="SqlTransaction"/> activa.</param>
        public CompeticionRepository(SqlConnection context, SqlTransaction _transaction) : base(context, _transaction)
        {
        }

        private const string _sqlSelect = @"SELECT 
                c.IdCompeticion, c.Cupos, c.Cupos_min, c.Estado, c.FechaCreacion, c.FechaInicio, 
                c.FranjaHoraria, c.Frecuencia, c.Nombre, c.PrecioInscripcion,
                f.Descripcion AS FormatoDescripcion,
                k.IdCancha,
                d.Descripcion AS DeporteDescripcion
            FROM DbCompeticion c
            LEFT JOIN DbFormato f ON c.IdFormato = f.IdFormato
            LEFT JOIN DbCancha k ON c.IdCancha = k.IdCancha
            LEFT JOIN DbDeporte d ON k.IdDeporte = d.IdDeporte";

        /// <summary>
        /// Agrega una nueva <see cref="Competicion"/> a la base de datos.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Competicion"/> a insertar.</param>
        public void Add(Competicion entity)
        {
            Guid formatoId = GetFormatoId(entity.Formato);
            string estado = entity.Estado.ToString();

            string sql = @"INSERT INTO DbCompeticion 
                           (IdCompeticion, Cupos, Cupos_min, Estado, FechaCreacion, FechaInicio, IdFormato, FranjaHoraria, Frecuencia, Nombre, PrecioInscripcion, IdCancha)
                           VALUES
                           (@Id, @Cupos, @CuposMin, @Estado, @FechaC, @FechaI, @IdFormato, @Franja, @Frec, @Nombre, @Precio, @IdCancha)";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Id", entity.IdCompeticion),
                new SqlParameter("@Cupos", entity.Cupos),
                new SqlParameter("@CuposMin", entity.CuposMinimos),
                new SqlParameter("@Estado", estado),
                new SqlParameter("@FechaC", entity.FechaCreacion),
                new SqlParameter("@FechaI", entity.FechaInicio),
                new SqlParameter("@IdFormato", formatoId),
                new SqlParameter("@Franja", (object)entity.FranjaHoraria ?? DBNull.Value),
                new SqlParameter("@Frec", entity.Frecuencia),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@Precio", entity.Precio),
                new SqlParameter("@IdCancha", entity.canchaAsignada.IdCancha)
            );
        }

        /// <summary>
        /// Inscribe un <see cref="Equipo"/> en una <see cref="Competicion"/>
        /// (inserta un registro en la tabla de unión <c>DbEquipoCompeticion</c>).
        /// </summary>
        /// <param name="idCompeticion">El ID de la competición.</param>
        /// <param name="idEquipo">El ID del equipo a inscribir.</param>
        public void AddEquipo(Guid idCompeticion, Guid idEquipo)
        {
            string sql = "INSERT INTO DbEquipoCompeticion (IdCompeticion, IdEquipo) VALUES (@IdComp, @IdEquipo)";
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdComp", idCompeticion),
                new SqlParameter("@IdEquipo", idEquipo)
            );
        }

        /// <summary>
        /// (No implementado) Cambia el estado de habilitación de una competición.
        /// </summary>
        public void CambiarHabilitado(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// (No implementado) Elimina una competición.
        /// </summary>
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene una lista de todas las competiciones.
        /// </summary>
        /// <returns>Una colección de <see cref="Competicion"/>.</returns>
        /// <remarks>
        public IEnumerable<Competicion> GetAll()
        {
            var list = new List<Competicion>();
            using (var reader = base.ExecuteReader(_sqlSelect, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            foreach (var c in list)
            {
                PopulateEquipos(c); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Obtiene una lista de todas las competiciones en las que participa un cliente (capitán).
        /// </summary>
        /// <param name="cliente">El <see cref="Cliente"/> (capitán) a consultar.</param>
        /// <returns>Una lista de <see cref="Competicion"/>.</returns>
        public List<Competicion> GetByClient(Cliente cliente)
        {
            string sql = $@"SELECT DISTINCT c.*
                            FROM ({_sqlSelect}) c
                            JOIN DbEquipoCompeticion ec ON c.IdCompeticion = ec.IdCompeticion
                            JOIN DbEquipo e ON ec.IdEquipo = e.IdEquipo
                            WHERE e.IdCliente = @IdCliente";

            var list = new List<Competicion>();
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCliente", cliente.IdCliente)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            foreach (var c in list)
            {
                PopulateEquipos(c); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Obtiene una <see cref="Competicion"/> específica por su ID,
        /// incluyendo su lista de equipos.
        /// </summary>
        /// <param name="id">El ID de la competición.</param>
        /// <returns>La <see cref="Competicion"/> encontrada (con equipos), o <c>null</c>.</returns>
        public Competicion GetById(Guid id)
        {
            Competicion competicion = null;
            string sql = $"{_sqlSelect} WHERE c.IdCompeticion = @Id";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    competicion = CompeticionAdapter.Current.Get(values);
                }
            }

            if (competicion != null)
            {

                PopulateEquipos(competicion); // Carga los equipos
            }

            return competicion;
        }

        /// <summary>
        /// Obtiene competiciones que colisionan en la misma cancha y franja horaria.
        /// </summary>
        /// <param name="competicion">La competición (con Cancha y Franja) a verificar.</param>
        /// <returns>Una lista de <see cref="Competicion"/> que colisionan.</returns>
        public List<Competicion> GetByTimeAndCancha(Competicion competicion)
        {
            string sql = $"{_sqlSelect} WHERE c.IdCancha = @IdCancha AND c.FranjaHoraria = @Franja";
            var list = new List<Competicion>();

            using (var reader = base.ExecuteReader(sql, CommandType.Text,
                new SqlParameter("@IdCancha", competicion.canchaAsignada.IdCancha),
                new SqlParameter("@Franja", competicion.FranjaHoraria)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            foreach (var c in list)
            {
                PopulateEquipos(c); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Obtiene todas las competiciones que aún tienen cupos (vacantes) disponibles.
        /// </summary>
        /// <returns>Una lista de <see cref="Competicion"/> con vacantes.</returns>
        public List<Competicion> GetWithVacancies()
        {
            string sql = $@"SELECT c.*
                FROM ({_sqlSelect}) c
                LEFT JOIN (
                    SELECT IdCompeticion, COUNT(*) AS EquiposInscriptos
                    FROM DbEquipoCompeticion
                    GROUP BY IdCompeticion
                ) ec ON c.IdCompeticion = ec.IdCompeticion
                WHERE c.Cupos > ISNULL(ec.EquiposInscriptos, 0)";

            var list = new List<Competicion>();
            using (var reader = base.ExecuteReader(sql, CommandType.Text))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    list.Add(CompeticionAdapter.Current.Get(values));
                }
            }

            foreach (var c in list)
            {
                PopulateEquipos(c); // N+1 consultas
            }
            return list;
        }

        /// <summary>
        /// Da de baja un <see cref="Equipo"/> de una <see cref="Competicion"/>
        /// (elimina el registro de la tabla de unión <c>DbEquipoCompeticion</c>).
        /// </summary>
        /// <param name="idCompeticion">El ID de la competición.</param>
        /// <param name="idEquipo">El ID del equipo a dar de baja.</param>
        public void RemoveEquipo(Guid idCompeticion, Guid idEquipo)
        {
            string sql = "DELETE FROM DbEquipoCompeticion WHERE IdCompeticion = @IdComp AND IdEquipo = @IdEquipo";
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdComp", idCompeticion),
                new SqlParameter("@IdEquipo", idEquipo)
            );
        }

        /// <summary>
        /// Actualiza un registro de <see cref="Competicion"/> existente.
        /// </summary>
        /// <param name="entity">La entidad <see cref="Competicion"/> con los datos modificados.</param>
        public void Update(Competicion entity)
        {
            Guid formatoId = GetFormatoId(entity.Formato);
            string estado = entity.Estado.ToString();

            string sql = @"UPDATE DbCompeticion SET
                            Cupos = @Cupos,
                            Cupos_min = @CuposMin,
                            Estado = @Estado,
                            FechaInicio = @FechaI,
                            IdFormato = @IdFormato,
                            FranjaHoraria = @Franja,
                            Frecuencia = @Frec,
                            Nombre = @Nombre,
                            PrecioInscripcion = @Precio,
                            IdCancha = @IdCancha
                           WHERE IdCompeticion = @Id";

            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@Cupos", entity.Cupos),
                new SqlParameter("@CuposMin", entity.CuposMinimos),
                new SqlParameter("@Estado", estado),
                new SqlParameter("@FechaI", entity.FechaInicio),
                new SqlParameter("@IdFormato", formatoId),
                new SqlParameter("@Franja", (object)entity.FranjaHoraria ?? DBNull.Value),
                new SqlParameter("@Frec", entity.Frecuencia),
                new SqlParameter("@Nombre", (object)entity.Nombre ?? DBNull.Value),
                new SqlParameter("@Precio", entity.Precio),
                new SqlParameter("@IdCancha", entity.canchaAsignada.IdCancha),
                new SqlParameter("@Id", entity.IdCompeticion)
            );
        }

        /// <summary>
        /// Método de ayuda (privado) para obtener el <see cref="Guid"/> (IdFormato)
        /// de un <see cref="FormatoEnum"/> a partir de su string.
        /// </summary>
        /// <param name="formato">El enum <see cref="FormatoEnum"/>.</param>
        /// <returns>El <see cref="Guid"/> del formato.</returns>
        /// <exception cref="InvalidOperationException">Si el formato no existe en la tabla DbFormato.</exception>
        private Guid GetFormatoId(FormatoEnum formato)
        {
            string desc = formato.ToString();
            string sql = "SELECT IdFormato FROM DbFormato WHERE Descripcion = @Descripcion";
            object result = base.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@Descripcion", desc));
            if (result == null || result == DBNull.Value)
                throw new InvalidOperationException($"El formato '{desc}' no fue encontrado en DbFormato.");
            return (Guid)result;
        }


        /// <summary>
        /// Método de ayuda (privado) que carga la lista de equipos (solo IDs)
        /// para una competición dada.
        /// </summary>
        /// <param name="competicion">La <see cref="Competicion"/> a la que se le cargarán los equipos.</param>
        private void PopulateEquipos(Competicion competicion)
        {
            string sql = @"SELECT ec.IdEquipo 
                   FROM DbEquipoCompeticion ec
                   JOIN DbEquipo e ON ec.IdEquipo = e.IdEquipo 
                   WHERE ec.IdCompeticion = @IdComp 
                   AND e.Habilitado = 1";
            competicion.ListaEquipos = new List<Equipo>();
            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdComp", competicion.IdCompeticion)))
            {
                while (reader.Read())
                {
                    competicion.ListaEquipos.Add(new Equipo { IdEquipo = (Guid)reader["IdEquipo"] });
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de todas las competiciones en las que participa un equipo.
        /// </summary>
        /// <param name="idEquipo">El ID del <see cref="Equipo"/>.</param>
        /// <returns>Una lista de <see cref="Competicion"/>.</returns>
        public List<Competicion> GetByEquipo(Guid idEquipo)
        {
            var lista = new List<Competicion>();
            string sql = $"{_sqlSelect} " +
                         "JOIN DbEquipoCompeticion ec ON c.IdCompeticion = ec.IdCompeticion " +
                         "WHERE ec.IdEquipo = @IdEquipo";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdEquipo", idEquipo)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    lista.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            foreach (var c in lista)
            {
                PopulateEquipos(c); // N+1 consultas
            }
            return lista;
        }

        /// <summary>
        /// Elimina *todos* los equipos inscritos en una competición.
        /// </summary>
        /// <param name="idCompeticion">El ID de la competición a "vaciar".</param>
        public void RemoveAllEquipos(Guid idCompeticion)
        {
            string sql = "DELETE FROM DbEquipoCompeticion WHERE IdCompeticion = @IdComp";
            base.ExecuteNonQuery(sql, CommandType.Text,
                new SqlParameter("@IdComp", idCompeticion)
            );
        }

        /// <summary>
        /// Obtiene una lista de todas las competiciones asignadas a una cancha específica.
        /// </summary>
        /// <param name="idCancha">El ID de la <see cref="Cancha"/>.</param>
        /// <returns>Una lista de <see cref="Competicion"/>.</returns>
        public List<Competicion> GetByCancha(Guid idCancha)
        {
            var lista = new List<Competicion>();
            // Usamos el _sqlSelect
            string sql = $"{_sqlSelect} WHERE c.IdCancha = @IdCancha";

            using (var reader = base.ExecuteReader(sql, CommandType.Text, new SqlParameter("@IdCancha", idCancha)))
            {
                while (reader.Read())
                {
                    object[] values = new object[reader.FieldCount];
                    reader.GetValues(values);
                    // Usamos tu adapter
                    lista.Add(CompeticionAdapter.Current.Get(values));
                }
            }
            // No necesitamos PopulateEquipos, solo los estados
            return lista;
        }
    }
}