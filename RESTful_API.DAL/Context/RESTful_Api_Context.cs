using Microsoft.EntityFrameworkCore;
using RESTful_API.DTO.Entities;

namespace RESTful_API.DAL.Context
{
    public class RESTful_Api_Context : DbContext
    {


        public RESTful_Api_Context(DbContextOptions<RESTful_Api_Context> options) : base(options)
        {
            // Kendi SQL Server bağlantınızı appsettings.json üzerinden değiştirebilirsiniz.
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.ID);

            modelBuilder.Entity<Genre>()
                .HasKey(g => g.ID);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Genres)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreID);

            base.OnModelCreating(modelBuilder);
        }


    }
}


