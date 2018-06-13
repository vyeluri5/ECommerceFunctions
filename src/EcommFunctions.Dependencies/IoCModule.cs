using EcommFunctions.Functions.Types;
using Ninject.Modules;

namespace EcommFunctions.Dependencies
{
    public class IoCModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductFunction>().To<ProductFunction>();
        }
    }
}
