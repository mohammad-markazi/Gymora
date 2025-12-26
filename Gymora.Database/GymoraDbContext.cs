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
        public DbSet<MovementModel> MovementModels { get; set; }
        public DbSet<QuestionModel> QuestionModels { get; set; }
        public DbSet<VideoMovementModel> VideoMovementModels { get; set; }
        public DbSet<PlanModel> PlanModels { get; set; }
        public DbSet<PlanQuestionModel> PlanQuestionModels { get; set; }
        public DbSet<PlanDetailModel> PlanDetailModels { get; set; }
        public DbSet<PlanMovementModel> PlanMovementModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasOne(u => u.Coach)
                .WithOne(c => c.User)
                .HasForeignKey<CoachModel>(c => c.UserId);

            base.OnModelCreating(modelBuilder);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
