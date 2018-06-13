using EcommFunctions.Functions.Interfaces;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace EcommFunctions.Dependencies.Interfaces
{
    public interface IEcommFunctionFactory : IDisposable
    {
        TFunction Create<TFunction>(TraceWriter log, CloudTable cloudTable)
            where TFunction : IEcommFunction;
    }
}
