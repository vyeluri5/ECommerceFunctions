using EcommFunctions.Functions.Interfaces;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceFunctions.Dependencies
{
    public interface IEcommFunctionFactory : IDisposable
    {
        TFunction Create<TFunction>(TraceWriter log)
            where TFunction : IEcommFunction;
    }
}
