using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LeaMaPortal.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class LoggedinUser
    {
        public string Userid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Email { get; set; }
        public int? AddConfig { get; set; }
        public int? EditConfig { get; set; }
        public int? DeleteConfig { get; set; }
        public string MenuConfig { get; set; }
    }
}