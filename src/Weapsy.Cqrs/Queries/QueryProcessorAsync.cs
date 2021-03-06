using System;
using System.Threading.Tasks;
using Weapsy.Cqrs.Dependencies;

namespace Weapsy.Cqrs.Queries
{
    /// <inheritdoc />
    /// <summary>
    /// QueryDispatcherAsync
    /// </summary>
    /// <seealso cref="T:WeapsyCqrs.Queries.IQueryDispatcherAsync" />
    public class QueryProcessorAsync : IQueryProcessorAsync
    {
        private readonly IResolver _resolver;

        public QueryProcessorAsync(IResolver resolver)
        {
            _resolver = resolver;
        }

        /// <inheritdoc />
        public async Task<TResult> ProcessAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var handler = _resolver.Resolve<IQueryHandlerAsync<TQuery, TResult>>();

            if (handler == null)
                throw new ApplicationException($"No handler of type WeapsyCqrs.Queries.IQueryHandlerAsync<TQuery, TResult>> found for query '{query.GetType().FullName}'");

            return await handler.RetrieveAsync(query);
        }
    }
}