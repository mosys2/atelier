using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Domain.Entities.Users
{
    internal class UserRoleExtraData:IdentityUserRole<Guid>
    {
        public string test { get; set; }
    }
}
