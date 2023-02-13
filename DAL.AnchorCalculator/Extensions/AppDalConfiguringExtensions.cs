using Core.AnchorCalculator;
using DAL.AnchorCalculator.Cotracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AnchorCalculator.Extensions
{
    public static class AppDalConfiguringExtensions
    {
        public static void AddDataBase(this IServiceCollection services, IApplicationDbContextFactory applicationDbContextFactory)
        {
            services.AddScoped(sp => applicationDbContextFactory.Create());

            services.AddDbContext<ApplicationDbContext>();

            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton(sp => applicationDbContextFactory);
        }
    }
}
