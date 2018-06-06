using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommFunctions.Functions.Interfaces
{
    public interface IEcommFunction : IDisposable
    {
        TraceWriter Log { get; set; }

        /// <summary>
        /// Invokes the function.
        /// </summary>
        /// <typeparam name="TInput">Type of input instance.</typeparam>
        /// <typeparam name="TOptions">Type of function options instance.</typeparam>
        /// <param name="input"><see cref="TIn"/> instance.</param>
        /// <param name="options"><see cref="TOptions"/> instance.</param>
        /// <returns>Returns output instance.</returns>
        Task<object> InvokeAsync<TInput, TType>(TInput input, TType options = default(TType));
    }
}
