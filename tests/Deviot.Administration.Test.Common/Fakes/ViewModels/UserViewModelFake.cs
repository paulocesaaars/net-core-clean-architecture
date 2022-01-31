using Deviot.Administration.Api.ViewModel;

namespace Deviot.Administration.Test.Common.Fakes.ViewModels
{
    public class UserViewModelFake
    {
        public static UserViewModel GetUser1()
        {
            return new UserViewModel
            {
                Id = "af4bf17c-fca7-453d-a9eb-251f3837da10",
                Email = "paulo@teste.com",
                FullName = "Paulo César de Souza"
            };
        }

        public static UserViewModel GetUser2()
        {
            return new UserViewModel
            {
                Id = "883cf9fa-f7e9-43d4-a8cb-51fd65863ba6",
                Email = "bruna@teste.com",
                FullName = "Bruna Stefano Marques"
            };
        }

        public static IEnumerable<UserViewModel> GetUsers()
        {
            var result = new List<UserViewModel>(2);
            result.Add(GetUser1());
            result.Add(GetUser2());
            return result;
        }
    }
}
