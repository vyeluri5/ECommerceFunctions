using EcommFunctions.Functions.Interfaces;
using EcommFunctions.Functions.Types;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceFunctions.Dependencies
{
    public class IoCModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductFunction>().To<ProductFunction>();
        }
    }
}
