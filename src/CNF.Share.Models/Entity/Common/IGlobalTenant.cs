using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Share.Models.Entity.Common
{
    /// <summary>
    /// 全局多租户id
    /// </summary>
    public interface IGlobalTenant
    {
        public int TenantId { get; set; }
    }
}
