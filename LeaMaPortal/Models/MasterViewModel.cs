using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaMaPortal.Models
{
    public class MasterViewModel
    {
        public int FormMasterId { get; set; }
    }
    public class MessageResult
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public string Errors { get; set; }
        public object Result { get; set; }

    }
}