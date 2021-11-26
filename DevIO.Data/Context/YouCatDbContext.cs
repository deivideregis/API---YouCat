using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevIO.Data.Context
{
    //cria as tabelas
    //add-migration Identity -Context YouCatDbContext
    //atualizar o banco de dados de novas tabelas
    //update-database -Context YouCatDbContext
    //add nova migration renomeado - para inserir novas ou alterar tabelas
    //dotnet ef migrations add CriandoSQLServerV1 --context YouCatDbContext --project "G:\PROJETO\API\APIYoutCat\DevIO.Data\DevIO.Data.csproj"
    //atualiza nova migrations - para inserir novas ou alterar tabelas
    //dotnet ef database update CriandoSQLServerV1 --context YouCatDbContext --project "G:\PROJETO\API\APIYoutCat\DevIO.Data\DevIO.Data.csproj"
    public class YouCatDbContext : DbContext
    {
        public YouCatDbContext(DbContextOptions<YouCatDbContext> options) : base(options) { }

        public DbSet<TipoEstudo> TipoEstudo { get; set; }

        public DbSet<Estudos> Estudos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YouCatDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
