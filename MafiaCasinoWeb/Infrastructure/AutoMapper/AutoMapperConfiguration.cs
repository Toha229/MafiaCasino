using DAL.AutoMapper.User;
using DAL.Models.VM;
using DAL.Models;

namespace MafiaCasinoWeb.Infrastructure.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Config(IServiceCollection services)
        {
            // User mapping
            services.AddAutoMapper(typeof(AutoMapperUserProfile));
        }
    }
}
