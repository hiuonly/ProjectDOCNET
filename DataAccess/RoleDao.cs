using BusinessObject.Context;
using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoleDao
    {
        public static RoleDao instance;
        public static readonly object instanceLock = new object();
        public static RoleDao Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new RoleDao();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Role> GetRoles()
        {
            List<Role> roles;
            try
            {
                var shopContext = new ShopContext();
                roles = shopContext.Roles.ToList();
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return roles;
        }
    }
}
