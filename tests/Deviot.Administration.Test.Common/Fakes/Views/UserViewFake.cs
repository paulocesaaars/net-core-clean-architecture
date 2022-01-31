using Deviot.Administration.Postgres.Views;

namespace Deviot.Administration.Test.Common.Fakes.Views
{
    public class UserViewFake
    {
        public static UserView GetUser1()
        {
            return new UserView
            {
                Id = new Guid("af4bf17c-fca7-453d-a9eb-251f3837da10"),
                Email = "paulo@teste.com",
                FullName = "Paulo César de Souza"
            };
        }

        public static UserView GetUser2()
        {
            return new UserView
            {
                Id = new Guid("883cf9fa-f7e9-43d4-a8cb-51fd65863ba6"),
                Email = "bruna@teste.com",
                FullName = "Bruna Stefano Marques"
            };
        }

        public static IEnumerable<UserView> GetUsers()
        {
            var result = new List<UserView>(2);
            result.Add(GetUser1());
            result.Add(GetUser2());
            return result;
        }
    }
}
