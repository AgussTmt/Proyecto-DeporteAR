using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services__ArqBase_.Dal
{

    /// <summary>
    /// Proporciona métodos asincrónicos para ejecutar operaciones de 
    /// BACKUP y RESTORE en una base de datos SQL Server.
    /// </summary>
    internal class BackUpRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio de BackUp.
        /// </summary>
        /// <param name="BaseDedatos">El nombre de la clave (key) en el archivo de configuración 
        /// (App.config/Web.config) que contiene el ConnectionString.</param>
        public BackUpRepository(string BaseDedatos)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[BaseDedatos].ConnectionString;
        }


        /// <summary>
        /// Ejecuta un BACKUP completo de la base de datos especificada en el connection string
        /// y lo guarda en la ruta de archivo proporcionada.
        /// </summary>
        /// <param name="rutaArchivo">La ruta física completa (en el servidor de base de datos) 
        /// donde se guardará el archivo .bak.</param>
        /// <returns>Una tarea (Task) que representa la operación asincrónica.</returns>
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


        /// <summary>
        /// Ejecuta un RESTORE de la base de datos desde un archivo de backup.
        /// La operación fuerza la base de datos a modo SINGLE_USER para tomar control exclusivo,
        /// realiza la restauración y luego la devuelve a modo MULTI_USER.
        /// </summary>
        /// <param name="rutaArchivo">La ruta física completa (en el servidor de base de datos) 
        /// desde donde se leerá el archivo .bak.</param>
        /// <returns>Una tarea (Task) que representa la operación asincrónica.</returns>
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
