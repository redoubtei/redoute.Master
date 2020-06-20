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
        [Remote("ValidateUserName", "baseuser", "maseter", AdditionalFields = "Id,UserName", ErrorMessage = "请重新填写！")]
        public string Username { get; set; }
    }
}
