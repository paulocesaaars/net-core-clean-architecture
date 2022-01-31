using Dapper.Contrib.Extensions;

namespace Deviot.Administration.Postgres.Views
{
    [Table("tb_user")]
    public class UserView
    {
        [ExplicitKey]
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
    }
}
