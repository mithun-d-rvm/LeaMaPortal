using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    internal class DdlTenentDetailsViewModel
    {
        public SelectList TenantId { get; set; }
        public SelectList TenantName { get; set; }
    }
}