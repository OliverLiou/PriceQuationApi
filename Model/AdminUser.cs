using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PriceQuationApi.Model
{
    public class AdminUser : IdentityUser
    {
        //Dotnet Core Identity 
        //https://docs.microsoft.com/zh-tw/aspnet/core/migration/identity?view=aspnetcore-5.0

        /// <summary>
        /// 部門代碼
        /// </summary>
        /// <value></value>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 員工編號
        /// </summary>
        /// <value></value>
        [MaxLength(10)]
        public string EmployeeId { get; set; }

        /// <summary>
        /// 員工名稱
        /// </summary>
        /// <value></value>
        [MaxLength(50)]
        public string EmployeeName { get; set; }

        //[一對多] Department
        public Department Department { get; set; }
    }
}