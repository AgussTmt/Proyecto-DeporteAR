using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services__ArqBase_.Dal;

namespace Services__ArqBase_.Bll
{
    /// <summary>
    /// Provee la lógica de negocio (BLL) para gestionar las operaciones
    /// de copia de seguridad (Backup) y restauración (Restore) de la base de datos.
    /// Actúa como una capa intermedia hacia <see cref="BackUpRepository"/>.
    /// </summary>
    internal class BackUpService
    {

        /// <summary>
        /// Inicia la creación de un backup de la base de datos de forma asincrónica.
        /// </summary>
        /// <param name="rutaArchivo">La ruta física completa (en el servidor de base de datos) 
        /// donde se guardará el archivo .bak.</param>
        /// <param name="nombreConexion">El nombre de la clave del connection string en el archivo de configuración.</param>
        /// <returns>Una tarea (Task) que representa la operación asincrónica.</returns>
        /// <exception cref="ArgumentException">Se lanza si <paramref name="rutaArchivo"/> es nula, vacía o solo espacios en blanco.</exception>
        internal async Task CrearBackupAsync(string rutaArchivo, string nombreConexion)
        {
            var backupRepo = new BackUpRepository(nombreConexion);

            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException("La ruta del archivo no puede estar vacía.");
            await backupRepo.EjecutarBackupAsync(rutaArchivo);
        }


        /// <summary>
        /// Inicia la restauración de una base de datos desde un archivo de backup de forma asincrónica.
        /// </summary>
        /// <param name="rutaArchivo">La ruta física completa (en el servidor de base de datos) 
        /// desde donde se leerá el archivo .bak.</param>
        /// <param name="nombreConexion">El nombre de la clave del connection string en el archivo de configuración.</param>
        /// <returns>Una tarea (Task) que representa la operación asincrónica.</CIU/>
        internal async Task RestaurarBackupAsync(string rutaArchivo, string nombreConexion)
        {
            var backupRepo = new BackUpRepository(nombreConexion);
            await backupRepo.EjecutarRestoreAsync(rutaArchivo);
        }
    }
}
