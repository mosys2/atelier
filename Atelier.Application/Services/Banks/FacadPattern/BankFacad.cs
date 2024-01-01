using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Banks.Commands;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Domain.MongoEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Banks.FacadPattern
{
    public class BankFacad : IBankFacad
    {
        private readonly IMongoRepository<Cheque> _chequeRepository;
        private readonly IMongoRepository<Bank> _bankRepository;
        public BankFacad(IMongoRepository<Cheque> chequeRepository, IMongoRepository<Bank> bankRepository)
        {
            _chequeRepository = chequeRepository;
            _bankRepository = bankRepository;
        }
        private IAddNewBankService _addNewBankService;
        private IEditBankService _editBankService;
        public IAddNewBankService AddNewBankService
        {
            get
            {
                return _addNewBankService = _addNewBankService ?? new AddNewBankService(_bankRepository);
            }
        }

        public IEditBankService EditBankService
        {
            get
            {
                return _editBankService = _editBankService ?? new EditBankService(_chequeRepository,_bankRepository);
            }
        }
    }
}
