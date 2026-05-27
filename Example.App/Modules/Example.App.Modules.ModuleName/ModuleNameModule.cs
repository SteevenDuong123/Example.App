using Example.App.Core;
using Example.App.Modules.ModuleName.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Example.App.Modules.ModuleName
{
    public class ModuleC : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleC(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ViewA");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewA>();
        }
    }
}