using Microsoft.EntityFrameworkCore;

namespace Schulprojekt.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionSet> QuestionSets { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<GapField> GapFields { get; set; }
        public DbSet<GapOption> GapOptions { get; set; }
        public DbSet<McAnswer> McAnswers { get; set; }
        public DbSet<Theme> Themes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionType)
                .HasConversion<string>();

            modelBuilder.Entity<GapField>()
                .Property(g => g.InputType)
                .HasConversion<string>();

            modelBuilder.Entity<GapField>()
                .HasIndex(g => new { g.QuestionId, g.GapIndex })
                .IsUnique();

            modelBuilder.Entity<GapOption>()
                .HasIndex(o => new { o.GapId, o.OptionOrder })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
