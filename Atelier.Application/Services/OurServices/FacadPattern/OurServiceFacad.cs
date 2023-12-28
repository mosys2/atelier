using Atelier.Application.Interfaces.FacadPattern;
using Atelier.Application.Interfaces.Repository;
using Atelier.Application.Services.OurServices.Commands;
using Atelier.Application.Services.OurServices.Queries;
using Atelier.Application.Services.Persons.Commands;
using Atelier.Domain.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Services.OurServices.FacadPattern
{
    public class OurServiceFacad:IOurServiceFacad
    {
        private readonly IMongoRepository<OurService> _ourServiceRepository;
        public OurServiceFacad(IMongoRepository<OurService> ourServiceRepository)
        {
            _ourServiceRepository = ourServiceRepository;
        }
        private IAddOurServiceService _addOurServiceService;
        private IGetOurServiceService _getOurServiceService;
        private IEditOurServiceService _editOurService;
        private IRemoveOurServiceService _removeOurServiceService;
        public IAddOurServiceService AddOurServiceService
        {
            get
            {
                return _addOurServiceService = _addOurServiceService ?? new AddOurServiceService(_ourServiceRepository);
            }
        }
        public IGetOurServiceService GetOurServiceService
        {
            get
            {
                return _getOurServiceService=_getOurServiceService ?? new GetOurServiceService(_ourServiceRepository);
            }
        }
        public IEditOurServiceService EditOurService
        {
            get
            {
                return _editOurService=_editOurService ?? new EditOurService(_ourServiceRepository);
            }
        }
        public IRemoveOurServiceService RemoveOurServiceService
        {
            get
            {
                return _removeOurServiceService=_removeOurServiceService ?? new RemoveOurServiceService(_ourServiceRepository);
            }
        }


    }
}
