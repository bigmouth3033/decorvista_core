using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechWizWebApp.Domain
{
    public class Permission
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public bool PermissionA { get; set; } = false;

        public bool PermissionB { get; set; } = false;

        public bool PermissionC { get; set; } = false;
        public virtual User? User { get; set; }
    }
}
