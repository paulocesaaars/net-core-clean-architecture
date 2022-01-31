using Deviot.Administration.Postgres.Core;
using System.Text;

namespace Deviot.Administration.Postgres.Builders
{
    public partial class UserBuilder
    {
        private readonly string _dbSchema;

        private const string PARAM_ID = "@ID";
        private const string PARAM_EMAIL = "@EMAIL";
        private const string PARAM_fullname = "@fullname";
        private const string PARAM_LIMIT = "@LIMIT";
        private const string PARAM_OFFSET = "@OFFSET";

        public UserBuilder(IDbService dbService)
        {
            _dbSchema = dbService.Schema;
        }

        protected string FindAllSql()
        {
            var query = new StringBuilder();
            query.AppendLine($"SELECT id");
            query.AppendLine($"     , email");
            query.AppendLine($"     , fullname");
            query.AppendLine($"  FROM {_dbSchema}.tb_user");
            query.AppendLine($"	WHERE LOWER(fullname) like LOWER({PARAM_fullname})");
            query.AppendLine($"	  AND LOWER(email) like LOWER({PARAM_EMAIL})");
            query.AppendLine($"	ORDER BY fullname");
            query.AppendLine($"  LIMIT {PARAM_LIMIT} OFFSET {PARAM_OFFSET}");

            return query.ToString();
        }

        protected string FindByIdSql()
        {
            var query = new StringBuilder();
            query.AppendLine($"SELECT id");
            query.AppendLine($"     , email");
            query.AppendLine($"     , fullname");
            query.AppendLine($"  FROM {_dbSchema}.tb_user");
            query.AppendLine($" WHERE id = {PARAM_ID}");

            return query.ToString();
        }

        protected string FindByEmailSql()
        {
            var query = new StringBuilder();
            query.AppendLine($"SELECT id");
            query.AppendLine($"     , email");
            query.AppendLine($"     , fullname");
            query.AppendLine($"  FROM {_dbSchema}.tb_user");
            query.AppendLine($" WHERE LOWER(email) = LOWER({PARAM_EMAIL})");

            return query.ToString();
        }

        protected string TotalRegistersSql()
        {
            var query = new StringBuilder();
            query.AppendLine($"SELECT COUNT(*) from {_dbSchema}.tb_user");

            return query.ToString();
        }

        protected string InsertSql()
        {
            var query = new StringBuilder();
            query.AppendLine($"INSERT INTO {_dbSchema}.tb_user");
            query.AppendLine($"     ( id");
            query.AppendLine($"     , email");
            query.AppendLine($"     , fullname");
            query.AppendLine($"     )");
            query.AppendLine($"VALUES ( {PARAM_ID}");
            query.AppendLine($"       , LOWER({PARAM_EMAIL})");
            query.AppendLine($"       , {PARAM_fullname}");
            query.AppendLine($"       )");

            return query.ToString();
        }

        protected string UpdateSql()
        {
            var query = new StringBuilder();
            query.AppendLine($"UPDATE {_dbSchema}.tb_user");
            query.AppendLine($"   SET email = {PARAM_EMAIL}");
            query.AppendLine($"     , fullname = {PARAM_fullname}");
            query.AppendLine($" WHERE id = {PARAM_ID}");

            return query.ToString();
        }
    }
}
