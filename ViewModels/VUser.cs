using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PriceQuationApi.ViewModels
{
    public class VUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }

        public List<string> RoleNames { get; set; }
    }
}