using System.Data.Entity;
using BLL.Interfaces.Interfaces;
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
            kernel.Bind<DbContext>().To<AuctionContext>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<ILotRepository>().To<LotRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>().InRequestScope();
            kernel.Bind<IRoleRepository>().To<RoleRepository>().InRequestScope();
            kernel.Bind<IRateRepository>().To<RateRepository>().InRequestScope();
            kernel.Bind<ILotStateRepository>().To<LotStateRepository>().InRequestScope();

            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<ILotService>().To<LotService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IRateService>().To<RateService>().InRequestScope();
        }
    }
}