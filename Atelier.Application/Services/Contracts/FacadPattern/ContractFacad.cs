using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Contracts.Commands;
using Atelier.Application.Services.Contracts.Queries;
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
        private IGetAllContractService _getAllContractService;
        private IEditContractService _editServiceContract;
        private IRemoveContractService _removeContractService;
        public IAddContractService AddContractService
        {
            get
            {
                return _addContractService=_addContractService ?? new AddContractService(_contractRepository, _personRepository);
            }
        }
        public IGetAllContractService GetAllContractService
        {
            get
            {
                return _getAllContractService=_getAllContractService ?? new GetAllContractService(_contractRepository);
            }
        }
        public IEditContractService EditServiceContract
        {
            get
            {
                return _editServiceContract=_editServiceContract ?? new EditServiceContract(_contractRepository, _personRepository);
            }
        }
        public IRemoveContractService RemoveContractService
        {
            get
            {
                return _removeContractService=_removeContractService ?? new RemoveContractService(_contractRepository);
            }
        }
    }
}
