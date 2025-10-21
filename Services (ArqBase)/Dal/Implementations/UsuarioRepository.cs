using Dal.Tools;
using Services.Bll;
using Services.Dal.Implementations.Adapters;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations
{
    internal class UsuarioRepository : IUsuarioRepository
    {
        #region Statements
        private string SelectAllStatement
        {
            get => "SELECT IdUsuario, Nombre, Email, Habilitado FROM [dbo].[Usuario]";
        }
        #endregion
        public void RegistrarUsuario(Usuario usuario)
        {
            usuario.IdUsuario = Guid.NewGuid(); // Generar un nuevo Id para el usuario
            string commandText = "INSERT INTO Usuario (IdUsuario, Nombre, Password, Email, Habilitado) VALUES (@IdUsuario, @Nombre, @Password, @Email, @Habilitado)";
            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text, new SqlParameter("@IdUsuario", usuario.IdUsuario),
                new SqlParameter("@Nombre", usuario.Nombre),
                new SqlParameter("@Password", usuario.Password),
                new SqlParameter("@Email", usuario.Email),
                new SqlParameter("@Habilitado", usuario.Habilitado)
            );
        }

        public Usuario GetByCredentials(string user, string password)
        {
            
            string commandText = "SELECT * FROM Usuario WHERE Nombre = @Nombre AND Password = @Password";

            using(SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Nombre", user),
                new SqlParameter("@Password", password)))
            {
                if (dataReader.Read())
                {
                    object[] data = new object[dataReader.FieldCount];
                    dataReader.GetValues(data);

                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> ListUsuarios = new List<Usuario>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader("SELECT * FROM [dbo].[Usuario]",
                                                                    CommandType.Text,
                                                                    new SqlParameter[] {} ))
            {
                while (reader.Read())
                {
                    object[] data = new object[reader.FieldCount];
                    reader.GetValues(data);

                    Usuario usuario = UsuarioAdapter.Current.Get(data);
                    ListUsuarios.Add(usuario);
                }
            }

            return ListUsuarios;
        }

        public Usuario GetById (Guid id)
        {
            string commandText = "SELECT * FROM Usuario WHERE IdUsuario = @Id";

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Id", id)))
            {
                if (dataReader.Read())
                {
                    object[] data = new object[dataReader.FieldCount];
                    dataReader.GetValues(data);

                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }

        }

        public Usuario GetByEmail(string email)
        {
            string commandText = "SELECT * FROM Usuario WHERE Email = @Email";

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(commandText, CommandType.Text,
                new SqlParameter("@Email", email)))
            {
                if (dataReader.Read())
                {
                    object[] data = new object[dataReader.FieldCount];
                    dataReader.GetValues(data);

                    return UsuarioAdapter.Current.Get(data);
                }
                return null;
            }
        }

        public void UpdatePassword(Usuario usuario, string passwordHasheada)
        {
            string commandText = "UPDATE Usuario SET Password = @Password WHERE IdUsuario = @IdUsuario";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@Password", passwordHasheada),
                new SqlParameter("@IdUsuario", usuario.IdUsuario)
            );
        }

        public void CleanRecoveryCode(Usuario usuario)
        {
            string commandText = "UPDATE Usuario SET CodigoRecuperacion = NULL, CodigoExpiracion = NULL WHERE IdUsuario = @IdUsuario";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@IdUsuario", usuario.IdUsuario)
            );
        }

        public void SaveRecoveryCode(Usuario usuario, string codigo, DateTime expiracion)
        {
            string commandText = "UPDATE Usuario SET CodigoRecuperacion = @Codigo, CodigoExpiracion = @Expiracion WHERE IdUsuario = @IdUsuario";

            SqlHelper.ExecuteNonQuery(commandText, CommandType.Text,
                new SqlParameter("@Codigo", codigo),
                new SqlParameter("@Expiracion", expiracion),
                new SqlParameter("@IdUsuario", usuario.IdUsuario)
            );
        }
    }
}
