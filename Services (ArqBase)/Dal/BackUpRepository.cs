using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services__ArqBase_.Dal
{
    internal class BackUpRepository
    {
        private readonly string _connectionString;
        public BackUpRepository(string BaseDedatos)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[BaseDedatos].ConnectionString;
        }

        internal async Task EjecutarBackupAsync(string rutaArchivo)
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            string dbName = builder.InitialCatalog;

            string sqlCommand = $"BACKUP DATABASE [{dbName}] TO DISK = @ruta";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(sqlCommand, connection))
                {
                    command.Parameters.AddWithValue("@ruta", rutaArchivo);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        internal async Task EjecutarRestoreAsync(string rutaArchivo)
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            string dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            string masterConnectionString = builder.ConnectionString;

            using (var connection = new SqlConnection(masterConnectionString))
            {
                await connection.OpenAsync();

                //Me aseguro de ser la unica conexion
                string sqlSetSingleUser = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                using (var cmdSetSingleUser = new SqlCommand(sqlSetSingleUser, connection))
                {
                    await cmdSetSingleUser.ExecuteNonQueryAsync();
                }

                //restore
                string sqlRestore = $"RESTORE DATABASE [{dbName}] FROM DISK = @ruta WITH REPLACE";
                using (var cmdRestore = new SqlCommand(sqlRestore, connection))
                {
                    cmdRestore.CommandTimeout = 3600;
                    cmdRestore.Parameters.AddWithValue("@ruta", rutaArchivo);
                    await cmdRestore.ExecuteNonQueryAsync();
                }

                // restauro multi usuario a la base
                string sqlSetMultiUser = $"ALTER DATABASE [{dbName}] SET MULTI_USER";
                using (var cmdSetMultiUser = new SqlCommand(sqlSetMultiUser, connection))
                {
                    await cmdSetMultiUser.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
