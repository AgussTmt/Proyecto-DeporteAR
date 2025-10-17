using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services__ArqBase_.Bll;

namespace Services__ArqBase_.Facade
{
    public static class DatabaseService
    {
        public static async Task RealizarBackupAsync(string rutaArchivo, string nombreConexion)
        {
            var backupBLL = new BackUpService();
            await backupBLL.CrearBackupAsync(rutaArchivo, nombreConexion);
        }

        public static async Task RealizarRestoreAsync(string rutaArchivo, string nombreConexion)
        {
            var backupBLL = new BackUpService();
            await backupBLL.RestaurarBackupAsync(rutaArchivo, nombreConexion);
        }
    }
}
