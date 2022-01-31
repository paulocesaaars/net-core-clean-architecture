using Deviot.Administration.Domain.Entities;

namespace Deviot.Administration.Test.Common.Fakes.Entities
{
    public class UserFake
    {
        public static User GetUser1()
        {
            return new User(
                "af4bf17c-fca7-453d-a9eb-251f3837da10",
                "Paulo César de Souza",
                "paulo@teste.com"
                );
        }

        public static User GetUser2()
        {
            return new User(
                "883cf9fa-f7e9-43d4-a8cb-51fd65863ba6",
                "Bruna Stefano Marques",
                "bruna@teste.com"
                );
        }

        public static IEnumerable<User> GetUsers()
        {
            var result = new List<User>(2);
            result.Add(GetUser1());
            result.Add(GetUser2());
            return result;
        }
    }
}
