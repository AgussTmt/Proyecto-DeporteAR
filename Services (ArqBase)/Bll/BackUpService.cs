using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services__ArqBase_.Dal;

namespace Services__ArqBase_.Bll
{
    internal class BackUpService
    {
        internal async Task CrearBackupAsync(string rutaArchivo, string nombreConexion)
        {
            var backupRepo = new BackUpRepository(nombreConexion);

            if (string.IsNullOrWhiteSpace(rutaArchivo))
                throw new ArgumentException("La ruta del archivo no puede estar vacía.");
            await backupRepo.EjecutarBackupAsync(rutaArchivo);
        }

        internal async Task RestaurarBackupAsync(string rutaArchivo, string nombreConexion)
        {
            var backupRepo = new BackUpRepository(nombreConexion);
            await backupRepo.EjecutarRestoreAsync(rutaArchivo);
        }
    }
}
