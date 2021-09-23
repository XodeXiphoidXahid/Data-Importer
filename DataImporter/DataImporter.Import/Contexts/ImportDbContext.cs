using Microsoft.EntityFrameworkCore;
using DataImporter.Import.Entities;

namespace DataImporter.Import.Contexts
{
    public class ImportDbContext : DbContext, IImportDbContext
    {
        private string _connectionString;
        private string _migrationAssemblyName;

        public ImportDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(g => g.ExcelDatas)
                .WithOne(e => e.Group);

            //One to One relationship
            modelBuilder.Entity<Group>()
            .HasOne<GroupColumnName>(g=>g.GroupColumnName)
            .WithOne(c => c.Group)
            .HasForeignKey<GroupColumnName>(g => g.GroupId);

            //One to Many
            modelBuilder.Entity<Group>()
                .HasMany(g => g.FileLocations)
                .WithOne(e => e.Group);


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<ExcelData> ExcelDatas { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<FileLocation> FileLocations { get; set; }
        public DbSet<GroupColumnName> GroupColumnNames { get; set; }
    }
}
