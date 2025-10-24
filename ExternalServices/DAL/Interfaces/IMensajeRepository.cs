using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;

namespace ExternalServices.DAL.Interfaces
{
    internal interface IMensajeRepository
    {
        void Add(Mensaje MensajeLog);
    }
}
