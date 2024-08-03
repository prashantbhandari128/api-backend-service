using BackendService.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendService.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        //---------------[ Set Here ]----------------
        public DbSet<Book> Books { get; set; }
        //-------------------------------------------
    }
}
