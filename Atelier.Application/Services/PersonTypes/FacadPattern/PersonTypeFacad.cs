using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Branches.Queries;
using Atelier.Application.Services.PersonTypes.Commands;
using Atelier.Application.Services.PersonTypes.Queries;
using Atelier.Domain.MongoEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.PersonTypes.FacadPattern
{
    public class PersonTypeFacad : IPersonTypeFacad
    {
        private readonly IMongoRepository<PersonType> _personTypeRepository;
        public PersonTypeFacad(IMongoRepository<PersonType> personTypeRepository)
        {
            _personTypeRepository = personTypeRepository;
        }
        private IAddPersonTypeService _addPersonTypeService;
        private IGetAllPersonTypeService _getAllPersonTypeService;
        public IAddPersonTypeService AddPersonTypeService
        {
            get
            {
                return _addPersonTypeService = _addPersonTypeService ?? new AddPersonTypeService(_personTypeRepository);
            }
        }

        public IGetAllPersonTypeService GetAllPersonTypeService
        {
            get
            {
                return _getAllPersonTypeService = _getAllPersonTypeService ?? new GetAllPersonType(_personTypeRepository);
            }
        }
    }
}
