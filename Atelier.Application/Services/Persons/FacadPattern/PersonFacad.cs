using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.Cheques.Commands;
using Atelier.Application.Services.Persons.Commands;
using Atelier.Application.Services.Persons.Queries;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.Persons.FacadPattern
{
    public class PersonFacad : IPersonFacad
    {
        private readonly IMongoRepository<Person> _personRepository;
        private readonly IMongoRepository<Job> _jobRepository;
        private readonly IMongoRepository<PersonType> _persontypeRepository;
        public PersonFacad(IMongoRepository<Person> personRepository, IMongoRepository<Job> jobRepository,  IMongoRepository<PersonType> persontypeRepository)
        {
            _jobRepository = jobRepository;
            _personRepository = personRepository;
            _persontypeRepository = persontypeRepository;

        }
        private  IAddPersonService _addPersonService;
        private  IGetAllPersonService _getAllPersonService;
        private  IEditPersonService _editPersonService;
        private  IRemovePersonService _removePersonService;
        public IAddPersonService AddPersonService
        {
            get
            {
                return _addPersonService = _addPersonService ?? new AddPersonService(_personRepository, _jobRepository, _persontypeRepository);
            }
        }

        public IGetAllPersonService GetAllPersonService
        {
            get
            {
                return _getAllPersonService = _getAllPersonService ?? new GetAllPersonService(_personRepository, _jobRepository, _persontypeRepository);
            }
        }

        public IEditPersonService EditPersonService
        {
            get
            {
                return _editPersonService = _editPersonService ?? new EditPersonService(_personRepository, _jobRepository, _persontypeRepository);
            }
        }

        public IRemovePersonService RemovePersonService
        {
            get
            {
                return _removePersonService = _removePersonService ?? new RemovePersonService(_personRepository);
            }
        }
    }
}
