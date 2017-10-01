using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using BLL.Services;
using DAL.Interfaces.Repositories;
using DAL.Repositories;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<AuctionContext>().InRequestScope();

            #region Services
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ILotService>().To<LotService>();
            kernel.Bind<IRateService>().To<RateService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            #endregion

            #region Repositories
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
            kernel.Bind<ILotRepository>().To<LotRepository>().InRequestScope();
            kernel.Bind<ILotStateRepository>().To<LotStateRepository>().InRequestScope();
            kernel.Bind<IRateRepository>().To<RateRepository>().InRequestScope();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InRequestScope();
            #endregion
        }
    }
}
