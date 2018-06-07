using EcommFunctions.Functions.Interfaces;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceFunctions.Dependencies
{
    public interface IEcommFunctionFactory : IDisposable
    {
        TFunction Create<TFunction>(TraceWriter log, CloudTable cloudTable)
            where TFunction : IEcommFunction;
    }
}
