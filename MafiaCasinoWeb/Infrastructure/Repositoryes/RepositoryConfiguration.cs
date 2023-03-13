using DAL.Repository;
using DAL.Repository.IRepository;

namespace _18_E_LEARN.Web.Infrastructure.Repositoryes
{
    public class RepositoryConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            // Add category repository
            services.AddScoped<IRewardsRopository, RewardsRepository>();
        }
    }
}
