using Dapper;
using Deviot.Administration.Postgres.Core;
using Deviot.Administration.Postgres.Views;
using System.Data;

namespace Deviot.Administration.Postgres.Builders
{
    public partial class UserBuilder : IUserBuilder
    {

        public Request FindAllRequest(
            string fullname, 
            string email, 
            int limit, 
            int offset
            )
        {
            fullname = $"%{fullname}%";
            email = $"%{email}%";
            var sql = FindAllSql();
            var parameters = new DynamicParameters();
            parameters.Add(PARAM_fullname, fullname, DbType.String, ParameterDirection.Input);
            parameters.Add(PARAM_EMAIL, email, DbType.String, ParameterDirection.Input);
            parameters.Add(PARAM_LIMIT, limit, DbType.Int16, ParameterDirection.Input);
            parameters.Add(PARAM_OFFSET, offset, DbType.Int16, ParameterDirection.Input);

            return new Request(sql, parameters);
        }

        public Request FindByEmailRequest(string email)
        {
            var sql = FindByEmailSql();
            var parameters = new DynamicParameters();
            parameters.Add(PARAM_EMAIL, email, DbType.String, ParameterDirection.Input);

            return new Request(sql, parameters);
        }

        public Request FindByIdRequest(Guid id)
        {
            var sql = FindByIdSql();
            var parameters = new DynamicParameters();
            parameters.Add(PARAM_ID, id, DbType.Guid, ParameterDirection.Input);

            return new Request(sql, parameters);
        }

        public Request TotalRegistersRequest()
        {
            var sql = TotalRegistersSql();
            var parameters = new DynamicParameters();

            return new Request(sql, parameters);
        }

        public Request InsertRequest(UserView user)
        {
            var sql = InsertSql();
            var parameters = new DynamicParameters();
            parameters.Add(PARAM_ID, user.Id, DbType.Guid, ParameterDirection.Input);
            parameters.Add(PARAM_EMAIL, user.Email, DbType.String, ParameterDirection.Input);
            parameters.Add(PARAM_fullname, user.FullName, DbType.String, ParameterDirection.Input);

            return new Request(sql, parameters);
        }

        public Request UpdateRequest(UserView user)
        {
            var sql = UpdateSql();
            var parameters = new DynamicParameters();
            parameters.Add(PARAM_ID, user.Id, DbType.Guid, ParameterDirection.Input);
            parameters.Add(PARAM_EMAIL, user.Email, DbType.String, ParameterDirection.Input);
            parameters.Add(PARAM_fullname, user.FullName, DbType.String, ParameterDirection.Input);

            return new Request(sql, parameters);
        }
    }
}
