using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        public void AddUser(User user) => UserDao.Instance.AddUser(user);

        public User GetUserByEmail(string email) => UserDao.Instance.GetUserByEmail(email);

        public User GetUserById(int id) => UserDao.Instance.GetUserById(id);

        public IEnumerable<User> GetUsers() => UserDao.Instance.GetUsers();

        public IEnumerable<User> GetUsersName(string name) => UserDao.Instance.GetUsersName(name);

        public IEnumerable<User> GetUsersRole(int id) => UserDao.Instance.GetUsersRole(id);

        public User Login(string email, string password) => UserDao.Instance.Login(email, password);

        public void RemoveUser(User user) => UserDao.Instance.RemoveUser(user);

        public void UpdateUser(User user) => UserDao.Instance.UpdateUser(user);
    }
}
