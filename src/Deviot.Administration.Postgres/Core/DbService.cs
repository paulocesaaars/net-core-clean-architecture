using Dapper;
using Deviot.Administration.Postgres.Configurations;
using Npgsql;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Administration.Postgres.Core
{
    [ExcludeFromCodeCoverage]
    public class DbService : IDbService
    {
        private readonly DbPostgresConfig _configuration;

        private const string CONNECTION_STRING_ERROR = "A conexão do Postgres não foi informada";
        private const string GENERIC_ERROR = "Houve um problema no serviço de banco de dados.";

        public string Schema => _configuration.Schema;

        public DbService(DbPostgresConfig configuration)
        {
            _configuration = configuration;
            if (string.IsNullOrEmpty(_configuration.PostgresConnectionString))
                throw new ArgumentNullException(CONNECTION_STRING_ERROR);

        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_configuration.PostgresConnectionString);

        public async Task<IEnumerable<TOutput>> ExecuteQueryRequestAsync<TOutput>(Request queryRequest)
        {
            var result = default(IEnumerable<TOutput>);
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                result = await connection.QueryAsync<TOutput>(
                    queryRequest.Sql,
                    queryRequest.DynamicParameters);

                if (result is null || result.AsList().Count == 0)
                    return null;
            }
            catch(Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public async Task<TOutput> ExecuteQueryFirstOrDefaultAsync<TOutput>(Request queryRequest)
        {
            var result = default(TOutput);
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                result = await connection.QueryFirstOrDefaultAsync<TOutput>(
                    queryRequest.Sql,
                    queryRequest.DynamicParameters);
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public async Task ExecuteCommandRequestAsync(Request commandRequest)
        {
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    await connection.ExecuteAsync(
                        commandRequest.Sql,
                        commandRequest.DynamicParameters,
                        transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task ExecuteCommandRequestAsync(IEnumerable<Request> commandRequests)
        {
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    foreach (var commandRequest in commandRequests)
                    {
                        await connection.ExecuteAsync(
                            commandRequest.Sql,
                            commandRequest.DynamicParameters,
                            transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<int> ExecuteCommandRowsRequestAsync(IEnumerable<Request> commandRequests)
        {
            int affectedLines = 0;
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    
                    foreach (var commandRequest in commandRequests)
                    {
                        affectedLines += await connection.ExecuteAsync(
                            commandRequest.Sql,
                            commandRequest.DynamicParameters,
                            transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }

            return affectedLines;
        }

        public async Task ExecuteProcedureRequestAsync(
            IEnumerable<Request> commandRequests, 
            IDbTransaction? transaction = null, 
            int? commandTimeout = null
            )
        {
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                foreach (var commandRequest in commandRequests)
                {
                    await connection.ExecuteAsync(
                        commandRequest.Sql,
                        commandRequest.DynamicParameters,
                        transaction,
                        commandTimeout,
                        CommandType.StoredProcedure
                        );
                }
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<IEnumerable<TOutput>> ExecuteProcedureRequestAsync<TOutput>(
            Request commandRequest, 
            IDbTransaction? transaction = null, 
            int? commandTimeout = null
            )
        {
            var result = default(IEnumerable<TOutput>);
            using var connection = CreateConnection();

            try
            {
                connection.Open();
                result = await connection.QueryAsync<TOutput>(
                    commandRequest.Sql,
                    commandRequest.DynamicParameters,
                    transaction,
                    commandTimeout,
                    CommandType.StoredProcedure
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(GENERIC_ERROR, ex);
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        
    }
}
