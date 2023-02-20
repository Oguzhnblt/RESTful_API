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

        public DbSet<Product> Products { get; set; }

    }
}


