using System.Data;

namespace Common.Data.User
{
    public interface IUserData
    {
        public Task<DataTable> GetUserByUsername(string username);
        public Task InsertUser(Models.User user);
    }
}
