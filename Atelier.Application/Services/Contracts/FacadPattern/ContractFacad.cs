using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Contracts.Commands;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Contracts.FacadPattern
{
    public class ContractFacad : IContractFacad
    {
        private readonly IMongoRepository<Contract> _contractRepository;
        private readonly IMongoRepository<Person> _personRepository;

        public ContractFacad(IMongoRepository<Contract> contractRepository, IMongoRepository<Person> personRepository)
        {
            _contractRepository = contractRepository;
            _personRepository=personRepository;

        }
        private IAddContractService _addContractService;
        public IAddContractService AddContractService
        {
            get
            {
                return _addContractService=_addContractService ?? new AddContractService(_contractRepository,_personRepository);
            }
        }
    }
}
