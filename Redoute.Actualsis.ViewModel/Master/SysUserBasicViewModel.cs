using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Redoute.Actualsis.ViewModel.Master
{
    public class SysUserBasicViewModel : BasicViewModel
    {

        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入登陆名")]
        [StringLength(20, ErrorMessage = "用户名不能超长")]
        [Remote("ValidateUserName", "SysUsers", "SiteManager", AdditionalFields = "Id,UserName", ErrorMessage = "用户已经被占用，请更换！")]
        public string Username { get; set; }
    }
}
