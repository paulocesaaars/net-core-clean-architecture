using System.Data;

namespace Deviot.Administration.Postgres.Core
{
    public interface IDbService
    {
        string Schema { get; }

        IDbConnection CreateConnection();

        Task<IEnumerable<TOutput>> ExecuteQueryRequestAsync<TOutput>(Request queryRequest);

        Task<TOutput> ExecuteQueryFirstOrDefaultAsync<TOutput>(Request queryRequest);

        Task ExecuteCommandRequestAsync(Request commandRequests);

        Task ExecuteCommandRequestAsync(IEnumerable<Request> commandRequests);

        Task<int> ExecuteCommandRowsRequestAsync(IEnumerable<Request> commandRequests);

        Task ExecuteProcedureRequestAsync(
            IEnumerable<Request> commandRequests,
            IDbTransaction? transaction = null,
            int? commandTimeout = null
            );

        Task<IEnumerable<TOutput>> ExecuteProcedureRequestAsync<TOutput>(
            Request commandRequest,
            IDbTransaction? transaction = null,
            int? commandTimeout = null
            );
    }
}
