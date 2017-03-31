using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class UserCreationViewModel
    {
        public int id { get; set; }

        [DisplayName("Display Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [DisplayName("Login ID")]
        [StringLength(100)]
        public string Userid { get; set; }

        [DisplayName("Password")]
        [StringLength(100)]
        public string Pwd { get; set; }

        [DisplayName("Confirm Password")]
        [StringLength(100)]
        public string Cnfpwd { get; set; }

        [DisplayName("Role")]
        [StringLength(100)]
        public string Category { get; set; }

        [DisplayName("Email ID")]
        [StringLength(100)]
        public string Email { get; set; }

        [DisplayName("Contact Number")]
        [StringLength(100)]
        public string Phoneno { get; set; }

        [DisplayName("Add")]
        public int? AddConfig { get; set; }

        [DisplayName("Edit")]
        public int? EditConfig { get; set; }

        [DisplayName("Delete")]
        public int? DeleteConfig { get; set; }

        [DisplayName("Menu Names")]
        [StringLength(50)]
        public string MenuConfig { get; set; }
        
        [StringLength(150)]
        public string Createduser { get; set; }

        [DisplayName("Active")]
        public int? Active { get; set; }

    }
    public class MenuRights
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public bool IsChecked { get; set; }
    }
}