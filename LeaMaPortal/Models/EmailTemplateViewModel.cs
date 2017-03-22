using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class EmailTemplateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Template ID")]
        public string TemplateID { get; set; }
        [DisplayName("Template Name")]
        public string TemplateName { get; set; }
        [DisplayName("Subject")]
        public string Subject { get; set; }
        [DisplayName("Body")]
        public string Body { get; set; }
        [DisplayName("Body Text")]
        public string BodyText { get; set; }
        [DisplayName("Subject Parameter")]
        public string SubjectParameter { get; set; }
    }
}