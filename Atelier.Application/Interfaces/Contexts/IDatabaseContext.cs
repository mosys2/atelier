using Atelier.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }


        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();
        void Dispose();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
