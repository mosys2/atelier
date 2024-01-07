using Atelier.Application.Interfaces.Repository;
using Atelier.Common.Dto;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Contracts.Commands
{
    public interface IAddContractService
    {
        Task<ResultDto> Execute(RequestContractDto request);
    }
    public class AddContractService: IAddContractService
    {
        private readonly IMongoRepository<Contract> _contractRepository;

        public AddContractService(IMongoRepository<Contract> contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<ResultDto> Execute(RequestContractDto request)
        {
            throw new NotImplementedException();
        }
    }
}
