using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsersRole(int id);
        IEnumerable<User> GetUsersName(string name);
        User GetUserById(int id);
        User GetUserByEmail(string email);
        User Login(string email, string password);
        void AddUser(User user);
        void UpdateUser(User user);
        void RemoveUser(User user);
    }
}
