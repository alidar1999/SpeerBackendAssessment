using Common.Data.User;
using Common.Enums;
using Common.Models;

namespace Common.Business.Authentication
{
    public class UserAuth : IAuth
    {
        private readonly IUserData _userData;
        public UserAuth(IUserData userData)
        {
            _userData = userData;
        }
        public async Task<LoginResponse> Login(User user)
        {
            User? userInfo = await GetUserByUsername(user.Username);
            if (userInfo == null)
            {
                return LoginResponse.IncorrectUsername;
            }

            return user.Password == userInfo.Password ? LoginResponse.Valid : LoginResponse.IncorrectPassword;
        }

        public async Task<RegisterResponse> Register(User user)
        {
            User? userInfo = await GetUserByUsername(user.Username);
            if (userInfo != null)
            {
                return RegisterResponse.UserAlreadyExist;
            }

            await _userData.InsertUser(user);

            return RegisterResponse.Valid;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var datatable = await _userData.GetUserByUsername(username);
            if (datatable != null)
            {
                if (datatable.Rows.Count > 0)
                {
                    return new User
                    {
                        Username = username,
                        Password = Convert.ToString(datatable.Rows[0]["Password"]),
                        Id = Convert.ToInt32(datatable.Rows[0]["UserId"])
                    };
                }
            }
            return null;
        }
    }
}
