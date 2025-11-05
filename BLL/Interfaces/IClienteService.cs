using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interfaces;
using BLL.Services.Dto;
using DomainModel;

namespace BLL.Interfaces
{
    public interface IClienteService : IGenericService <Cliente>
    {
        List<RankingClienteDTO> GetRankingClientes(int topN);

        void Delete(Guid id);
    }
}
