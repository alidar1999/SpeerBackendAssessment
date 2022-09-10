using Common.Enums;
using Common.Models;

namespace Common.Business.Authentication
{
    public interface IAuth
    {
        public Task<LoginResponse> Login(User user);
        public Task<RegisterResponse> Register(User user);
        public Task<User?> GetUserByUsername(string username);
    }
}
