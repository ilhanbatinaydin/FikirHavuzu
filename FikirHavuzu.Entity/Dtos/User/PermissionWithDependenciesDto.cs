using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FikirHavuzu.Entity.Dtos.User
{
    public class PermissionWithDependenciesDto : PermissionDto
    {
        public List<int> RequiredPermissionIds { get; set; } = new List<int>();
    }
}
