using Microsoft.EntityFrameworkCore;
using System.Xml;
using System.Xml.Linq;

namespace APISecurityBasic.Models
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options){
        
        
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Villa>().HasData(
            //    new Villa(1,
            //"Long",
            //"Hello",
            //3,
            //3,
            //3,
            // "a",
            //"b"
            //)
            //    ); 
        }
    }
}
