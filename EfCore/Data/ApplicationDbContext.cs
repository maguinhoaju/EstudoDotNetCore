using Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            //Habilita o carregamento sob demanda. Util para consultas que trazem objetos dependentes
            optionsBuilder.UseLazyLoadingProxies();
        }

        //Fluent API - Estudar mais e trabalhar com o padrão e versionamento de banco
        // Este método é usado para redefinir propriedades de tabelas no banco de dados
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //V1.02

            //Alterando o nome da tabela
            modelBuilder.Entity<Produto>().ToTable("Produto");
            //Alterando o tamanho do campo NOME na tabela de Produto
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(100);
            // Ex.: Para definir uma chave primaria diferente de "Id"
            // Por default o Entity Framework define os campos "Id" como chave
            //modelBuilder.Entity<Produto>().HasKey(p => p.Codigo);

            //V1.02
            // criando tabela pedido e customizando chave e valor default da data
            modelBuilder.Entity<Pedido>().HasKey(p => p.Numero);
            modelBuilder.Entity<Pedido>().Property(p => p.Data)
                .IsRequired() //opcional pois a definição do defaultvalue já tornaria o campo "not null"
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}