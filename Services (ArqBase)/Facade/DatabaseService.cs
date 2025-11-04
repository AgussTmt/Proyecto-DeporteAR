using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services__ArqBase_.Bll;

namespace Services__ArqBase_.Facade
{
    /// <summary>
    /// Fachada (Facade) estática que provee una API simple
    /// para las operaciones de la base de datos, como Backup y Restore.
    /// Delega la lógica de negocio a <see cref="BackUpService"/>.
    /// </summary>
    public static class DatabaseService
    {

        /// <summary>
        /// Inicia la creación de un backup de la base de datos de forma asincrónica.
        /// </summary>
        /// <param name="rutaArchivo">La ruta física completa (en el servidor de base de datos) 
        /// donde se guardará el archivo .bak.</param>
        /// <param name="nombreConexion">El nombre de la clave del connection string en el archivo de configuración.</param>
        /// <returns>Una tarea (Task) que representa la operación asincrónica.</returns>
        public static async Task RealizarBackupAsync(string rutaArchivo, string nombreConexion)
        {
            var backupBLL = new BackUpService();
            await backupBLL.CrearBackupAsync(rutaArchivo, nombreConexion);
        }

        /// <summary>
        /// Inicia la restauración de una base de datos desde un archivo de backup de forma asincrónica.
        /// </summary>
        /// <param name="rutaArchivo">La ruta física completa (en el servidor de base de datos) 
        /// desde donde se leerá el archivo .bak.</param>
        /// <param name="nombreConexion">El nombre de la clave del connection string en el archivo de configuración.</param>
        /// <returns>Una tarea (Task) que representa la operación asincrónica.</CIU/>
        /// <remarks>
        /// ¡Atención! Esta es una operación destructiva (WITH REPLACE) que
        /// desconectará a todos los usuarios de la base de datos.
        /// </remarks>
        public static async Task RealizarRestoreAsync(string rutaArchivo, string nombreConexion)
        {
            var backupBLL = new BackUpService();
            await backupBLL.RestaurarBackupAsync(rutaArchivo, nombreConexion);
        }
    }
}
