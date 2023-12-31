﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.MongoEntities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        Guid BranchId { get; set; }
        bool IsRemoved { get; set; }
        DateTime InsertTime {  get; set; }
    }
}
