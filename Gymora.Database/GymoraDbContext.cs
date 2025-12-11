using Gymora.Database.Entities;
using Gymora.Database.Entities.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Gymora.Database
{
    public class GymoraDbContext:DbContext, IGymoraDbContext
    {
        public GymoraDbContext(DbContextOptions<GymoraDbContext> options):base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<VerifyCodeModel> VerifyCodes { get; set; }
        public DbSet<CoachModel> Coaches { get; set; }
        public DbSet<SystemLog> SystemLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasOne(x => x.Coach)
                .WithOne(x => x.User).HasForeignKey<UserModel>(x=>x.CoachId);

            modelBuilder.Entity<CoachModel>()
                .HasOne(x => x.User)
                .WithOne(x => x.Coach).HasForeignKey<CoachModel>(x => x.UserId);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
