using BusinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public IEnumerable<Role> GetRoles() => RoleDao.Instance.GetRoles();
    }
}
