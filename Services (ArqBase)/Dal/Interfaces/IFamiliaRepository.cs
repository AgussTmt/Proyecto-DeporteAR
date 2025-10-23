using Services.DomainModel;
using System;
using System.Collections.Generic;

namespace Services.Dal.Implementations
{
    internal interface IFamiliaRepository
    {
        Familia GetById(Guid id);

        public List<Familia> GetAll();

        Familia Add(Familia familia);
    }
}