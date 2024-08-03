using BackendService.DataAccess.Context;
using BackendService.DataAccess.Entities;
using BackendService.DataAccess.Repository.Interface;

namespace BackendService.DataAccess.Repository.Implementation
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
