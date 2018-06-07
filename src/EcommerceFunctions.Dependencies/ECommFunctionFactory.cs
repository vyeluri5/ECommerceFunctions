using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommFunctions.Functions.Interfaces;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using Ninject;

namespace EcommerceFunctions.Dependencies
{
    public class ECommFunctionFactory : IEcommFunctionFactory
    {

        private readonly IKernel kernel;
        private bool isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AglFunctionFactory"/> class.
        /// </summary>
        public ECommFunctionFactory()
        {
            kernel = new StandardKernel(new IoCModule());
        }

        public TFunction Create<TFunction>(TraceWriter log, CloudTable cloudTable) 
            where TFunction : IEcommFunction
        {
            var _cloudTable = new Ninject.Parameters.ConstructorArgument("_cloudTable", cloudTable);

            var function = kernel.Get<TFunction>(_cloudTable);
            function.Log = log;
            return function;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
