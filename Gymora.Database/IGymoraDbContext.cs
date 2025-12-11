using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gymora.Database.Entities;
using Gymora.Database.Entities.Utility;

namespace Gymora.Database
{
    public interface IGymoraDbContext
    {
        DbSet<UserModel> Users { get; set; }
        DbSet<VerifyCodeModel> VerifyCodes { get; set; }
        DbSet<CoachModel> Coaches { get; set; }
        DbSet<SystemLog> SystemLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}

