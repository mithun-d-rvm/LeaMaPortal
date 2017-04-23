using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class AgreementDocumentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HttpPostedFileBase File { get; set; }

        public List<AgreementDocumentExist> agreementDocumentExistList { get; set; }

    }
    public class AgreementDocumentExist
    {
        public AgreementDocumentExist()
        {
            agreementDocumentExistList = new List<AgreementDocumentExist>();
        }
        public int Id { get; set; }
        [StringLength(150)]
        public string Doc_name { get; set; }

        [StringLength(300)]
        public string Doc_Path { get; set; }

        public List<AgreementDocumentExist> agreementDocumentExistList { get; set; }

       
    }
}