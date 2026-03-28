using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    public static class RepositoryExtensions //extensionlar fonksiyonlar statik olmalılar
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services,IConfiguration configuration)
            //void yerine IServiceCollection döndürüyoruz ki,fluent bir chain yapı kurabilelim
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStrings = configuration.GetSection(ConnectionsStringOption.Key)
                    .Get<ConnectionsStringOption>();

                options.UseSqlServer(connectionStrings!.SqlServer,
                    sqlServerOptionsAction=> //Ef Core'un migrationları yanlış yerde aramaması için yaptık.Böylece 
                                            //Proje adı değişşe vs. bile  assembly adını doğru alırız.
                    {
                        sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                    });
            });
            
                services.AddScoped<IProductRepository,ProductRepository>(); //singleton kullanmıyoruz burda. Dbcontext zaten scoped
                                                                            //(efcoreda)
           services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>)); //Eğer IGenericRepository, birden fazla t1,t2 alsaydı
            return services;                                                              //Burada IGenericREpo<,> şeklinde virgül koyacaktık.

        }
    }
}
