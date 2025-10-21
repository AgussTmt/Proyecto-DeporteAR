using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Bll;

namespace Services__ArqBase_.Facade
{
    public static class PassRecoveryService
    {
        public static bool SolicitarRecuperacion(string email)
        {
            return UsuarioBll.SolicitarRecuperacion(email);
        }

        public static void ResetearPassword(string email, string codigo, string nuevoPassword)
        {
            UsuarioBll.ResetearPassword(email, codigo, nuevoPassword);
        }
    }
}
