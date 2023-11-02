using BusinessObject.Context;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserDao
    {
        public static UserDao instance;
        public static readonly object instanceLock = new object();
        public static UserDao Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDao();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<User> GetUsers()
        {
            List<User> users;
            try
            {
                var shopContext = new ShopContext();
                users = (from u in shopContext.Users
                         join r in shopContext.Roles on u.roleId equals r.roleId
                         select new User
                         {
                             userId = u.userId,
                             uImg= u.uImg,
                             email= u.email,
                             password= u.password,
                             firstName= u.firstName,
                             lastName= u.lastName,
                             phoneNumber= u.phoneNumber,
                             roleId=u.roleId,
                             address= u.address,
                             Roles = r
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }
        public IEnumerable<User> GetUsersRole(int id)
        {
            List<User> users;
            try
            {
                var shopContext = new ShopContext();
                users = (from u in shopContext.Users
                         join r in shopContext.Roles on u.roleId equals r.roleId
                         where u.roleId == id
                         select new User
                         {
                             userId = u.userId,
                             uImg = u.uImg,
                             email = u.email,
                             password = u.password,
                             firstName = u.firstName,
                             lastName = u.lastName,
                             phoneNumber = u.phoneNumber,
                             roleId = u.roleId,
                             address = u.address,
                             Roles = r
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }
        public IEnumerable<User> GetUsersName(string name)
        {
            List<User> users;
            try
            {
                var shopContext = new ShopContext();
                users = (from u in shopContext.Users
                         join r in shopContext.Roles on u.roleId equals r.roleId
                         where u.lastName.Contains(name)
                         select new User
                         {
                             userId = u.userId,
                             uImg = u.uImg,
                             email = u.email,
                             password = u.password,
                             firstName = u.firstName,
                             lastName = u.lastName,
                             phoneNumber = u.phoneNumber,
                             roleId = u.roleId,
                             address = u.address,
                             Roles = r
                         }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }
        public User GetUserById(int id)
        {
            User user = null;
            try
            {
                var shopContext = new ShopContext();
                user = (from u in shopContext.Users
                        join r in shopContext.Roles on u.roleId equals r.roleId
                        where u.userId == id
                        select new User
                        {
                            userId = u.userId,
                            uImg = u.uImg,
                            email = u.email,
                            password = u.password,
                            firstName = u.firstName,
                            lastName = u.lastName,
                            phoneNumber = u.phoneNumber,
                            roleId = u.roleId,
                            address = u.address,
                            Roles = r
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public User GetUserByEmail(string email)
        {
            User user = null;
            try
            {
                var shopContext = new ShopContext();
                user = (from u in shopContext.Users
                        join r in shopContext.Roles on u.roleId equals r.roleId
                        where u.email == email
                        select new User
                        {
                            userId = u.userId,
                            uImg = u.uImg,
                            email = u.email,
                            password = u.password,
                            firstName = u.firstName,
                            lastName = u.lastName,
                            phoneNumber = u.phoneNumber,
                            roleId = u.roleId,
                            address = u.address,
                            Roles = r
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public User Login(string email, string password)
        {
            User user = null;
            try
            {
                var shopContext = new ShopContext();
                user = (from u in shopContext.Users
                        join r in shopContext.Roles on u.roleId equals r.roleId
                        where u.email == email && u.password == password
                        select new User
                        {
                            userId = u.userId,
                            uImg = u.uImg,
                            email = u.email,
                            password = u.password,
                            firstName = u.firstName,
                            lastName = u.lastName,
                            phoneNumber = u.phoneNumber,
                            roleId = u.roleId,
                            address = u.address,
                            Roles = r
                        }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
        public void AddUser(User user)
        {
            try
            {
                var mail = GetUserByEmail(user.email);
                if (mail == null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Users.Add(user);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The email already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                var id = GetUserById(user.userId);
                if (id != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Entry<User>(user).State = EntityState.Modified;
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The User does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RemoveUser(User user)
        {
            try
            {
                var id = GetUserById(user.userId);
                if (id != null)
                {
                    var shopContext = new ShopContext();
                    shopContext.Users.Remove(user);
                    shopContext.SaveChanges();
                }
                else
                {
                    throw new Exception("The User does not already exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
