using BackendService.DataAccess.Repository.Implementation;
using BackendService.DataAccess.Repository.Interface;
using BackendService.DataAccess.UnitOfWork.Implementation;
using BackendService.DataAccess.UnitOfWork.Interface;
using BackendService.Helper.Implementation;
using BackendService.Helper.Interface;
using Microsoft.EntityFrameworkCore;

namespace BackendService.Extensions
{
    public static class ServiceExtension
    {
        //-----------------------------[ Inject DbContext ]-------------------------------------
        public static void AddDatabaseContext<TDbContext>(this IServiceCollection services, string? connectionString) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options =>
            {
                //---------------------------[ Database Connection ]------------------------------------
                options.UseSqlite(connectionString);
                //--------------------------------------------------------------------------------------
            });
        }
        //--------------------------------------------------------------------------------------

        //-----------------------------[ Inject Helper Here ]-----------------------------------
        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddTransient<IConsoleHelper, ConsoleHelper>();
        }
        //--------------------------------------------------------------------------------------

        //---------------------------[ Inject Repository Here ]---------------------------------
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBookRepository, BookRepository>();
        }
        //--------------------------------------------------------------------------------------

        //--------------------------[ Inject Unit Of Work Here ]--------------------------------
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        //--------------------------------------------------------------------------------------

    }

}
