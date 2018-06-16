using Microsoft.Azure.WebJobs.Host;
using System;
using System.Threading.Tasks;
using EcommFunctions.Models;

namespace EcommFunctions.Functions.Interfaces
{
    public interface IEcommFunction : IDisposable
    {
        TraceWriter Log { get; set; }

        /// <summary>
        /// Gets the product using the function.
        /// </summary>
        /// <typeparam name="TInput">Type of input instance.</typeparam>
        /// <typeparam name="TType">Type of function options instance.</typeparam>
        /// <param name="input"><see cref="TInput"/> instance.</param>
        /// <param name="options"><see cref="TType"/> instance.</param>
        /// <returns>Returns output instance.</returns>
        Task<object> GetProductAsync<TInput, TType>(TInput input, TType options = default);

        /// <summary>
        /// Creates new product given an instance of <see cref="ProductEntity"/>
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TType"></typeparam>
        /// <param name="input"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<object> CreateProductAsync<TInput, TType>(TInput input, TType options = default);
    }
}
