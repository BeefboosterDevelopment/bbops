using System;
using Beefbooster.Bull.Entities.Models;
using Beefbooster.Data;
using Beefbooster.Operations.PredictabullServices;
using Beefbooster.Operations.PredictabullServices.PredictabullRepositories;
using Beefbooster.Operations.Service;
using Microsoft.Practices.Unity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Beefbooster.Operations.WebUI.App_Start
{
    /// <summary>
    ///     Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container

        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        ///     Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        ///     There is no need to register concrete types such as controllers or API controllers (unless you want to
        ///     change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here

            container.RegisterType<IPredictABullAccountServices, PredictABullAccountServices>();
            container.RegisterType<ISelectionServices, SelectionServices>();
            container.RegisterType<ISaleBullRrepository, SaleBullRepository>();
            container.RegisterType<IPercentileRepository, PercentileRepository>();
            container.RegisterType<IAccountRepository, AccountRepository>();

            container.RegisterType<IDataContextAsync, BullContext>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager());

            container.RegisterType<ISpringSaleService, SpringSaleService>();
            container.RegisterType<IShufflerService, ShufflerService>();
            container.RegisterType<IRepositoryAsync<SpringSale>, Repository<SpringSale>>();
            container.RegisterType<IRepositoryAsync<SpringSaleDate>, Repository<SpringSaleDate>>();
            container.RegisterType<IRepositoryAsync<VWPOD>, Repository<VWPOD>>();
        }
    }
}