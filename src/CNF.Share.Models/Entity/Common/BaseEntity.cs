using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace CNF.Share.Models.Entity.Common
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? ModifyTime { get; set; }
        DateTime CreateTime { get; set; }
        /// <summary>
        /// true为正常，  false为删除
        /// </summary>
        bool Status { get; set; }
    }

    /// <summary>
    /// 所有不是多租户的数据库实体基类
    /// </summary>
    public class BaseEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Status { get; set; } = true;
    }

    /// <summary>
    /// 所有多租户数据库实体基类
    /// </summary>
    public class BaseTenantEntity : IGlobalTenant, IEntity
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        public int TenantId { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string TenantName { get; set; }
        public DateTime? ModifyTime { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Status { get; set; } = true;
    }

    /// <summary>
    /// 所有多租户数据库(分类，栏目，菜单等树形结构使用)实体基类
    /// </summary>
    public class BaseTenantTreeEntity : BaseTenantEntity
    {
        // Desc:栏位集合    
        public string ParentList { get; set; }
        /// Desc:栏位等级     
        public int Layer { get; set; }
        public int ParentId { get; set; }
    }



}
