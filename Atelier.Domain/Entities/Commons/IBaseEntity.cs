﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.Entities.Commons
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime? InsertTime { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsRemoved { get; set; }
        DateTime? RemoveTime { get; set; }

    }
}
