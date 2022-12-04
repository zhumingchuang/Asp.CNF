using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Models.Dtos.Input.Sys
{
    public class SetUserRoleInput
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool Status { get; set; } = true;
    }
}
