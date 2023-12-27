using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Application.Services.Cheques.Commands;
using Atelier.Application.Services.Cheques.Queries;
using Atelier.Domain.MongoEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Cheques.FacadPattern
{
    public class ChequeFacad : IChequeFacad
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        private readonly IMongoRepository<Bank> _bankRepository;
        private readonly IMongoRepository<Person> _personRepository;
        public ChequeFacad(IMongoRepository<Cheque> chequeRepository, IMongoRepository<Bank> bankRepository, IMongoRepository<Person> personRepository)
        {
            _bankRepository = bankRepository;
            _chequeRepository = chequeRepository;
            _personRepository = personRepository;
        }
        //
        private IAddChequeService _addChequeService;
        private IEditChequeService _editChequeService;
        private IRemoveChequeService _removeChequeService;
        private IGetAllChequeService _getAllChequeService;
        public IAddChequeService AddChequeService
        {
            get
            {
                return _addChequeService = _addChequeService ?? new AddChequeService(_chequeRepository,_bankRepository,_personRepository);
            }
        }

        public IEditChequeService EditChequeService
        {
            get
            {
                return _editChequeService = _editChequeService ?? new EditChequeService(_chequeRepository, _bankRepository, _personRepository);
            }
        }

        public IRemoveChequeService RemoveChequeService
        {
            get
            {
                return _removeChequeService = _removeChequeService ?? new RemoveChequeService(_chequeRepository);
            }
        }

        public IGetAllChequeService GetAllChequesService
        {
            get
            {
                return _getAllChequeService = _getAllChequeService ?? new GetAllChequeService(_chequeRepository);
            }
        }
    }
}
