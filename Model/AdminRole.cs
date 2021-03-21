using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PriceQuationApi.Model
{
    public class AdminRole : IdentityRole
    {
        /// <summary>
        /// 角色名稱
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string RoleDesc { get; set; }
    }
}