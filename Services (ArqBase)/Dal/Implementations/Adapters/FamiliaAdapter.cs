using Services.Dal.Interfaces;
using Services.DomainModel;
using Services.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Services.Dal.Implementations.Adapters
{
    internal class FamiliaAdapter : IAdapter<Familia>
    {
        #region Singleton
        private readonly static FamiliaAdapter _instance = new FamiliaAdapter();

        public static FamiliaAdapter Current
        {
            get
            {
                return _instance;
            }
        }

        private FamiliaAdapter()
        {
            //Implent here the initialization of your singleton
        }

        #endregion
        public Familia Get(object[] values)
        {
            Familia familia = new Familia();
            familia.Id = Guid.Parse(values[0].ToString());
            familia.Nombre = values[1].ToString();
            familia.Habilitado = Convert.ToBoolean(values[2]);


            string hashGuardado = values[3] == DBNull.Value ? null : values[3].ToString();
            string hashCalculado = CalcularHash(familia);
            if (hashGuardado != hashCalculado)
            {

                throw new SecurityException($"¡Datos corruptos! La fila para la familia '{familia.Nombre}' ha sido manipulada.");
            }

            familia.VerificadorHash = hashGuardado;



            familia.AddRange(new FamiliaFamiliaRepository().GetByObject(familia));

            familia.AddRange(new FamiliaPatenteRepository().GetByObject(familia));

            return familia;
        }

        private string CalcularHash(Familia familia)
        {
            
            string datosConcatenados = $"{familia.Id}-{familia.Nombre}-{familia.Habilitado}";

            return CryptographyService.HashMd5(datosConcatenados);
        }

    }
}
