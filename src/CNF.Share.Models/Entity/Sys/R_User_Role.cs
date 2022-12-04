using CNF.Share.Models.Entity.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Models.Entity.Sys
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Sys_R_User_Role")]
    public partial class R_User_Role : BaseEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int UserId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int RoleId { get; set; }

    }
}
