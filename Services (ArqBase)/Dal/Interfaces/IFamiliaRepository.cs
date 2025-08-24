using Services.DomainModel;
using System;

namespace Services.Dal.Implementations
{
    internal interface IFamiliaRepository
    {
        Familia GetById(Guid id);
    }
}