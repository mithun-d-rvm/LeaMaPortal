using LeaMaPortal.DBContext;
using LeaMaPortal.Helpers;
using LeaMaPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LeaMaPortal.Controllers
{
    public class NotificationController : Controller
    {
        private LeamaEntities db = new LeamaEntities();
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> getNotifications(string filter)
        {
            try
            {
                List<object> data = new List<object>();
                List<NotificationRenewalModel> RenewalData = new List<NotificationRenewalModel>();
                List<NotificationPdcModel> PdcData = new List<NotificationPdcModel>();
                List<NotificationRentalDueModel> RentalDueData = new List<NotificationRentalDueModel>();
                List<NotificationUtilityDuesModel> UtilityDuesData = new List<NotificationUtilityDuesModel>();
                List<NotificationAgreementApprovalModel> AgreementData = new List<NotificationAgreementApprovalModel>();
                NotificationHelper notify = new NotificationHelper();
                switch (filter)
                {
                    case StaticHelper.NOTIFICATION_PDC:
                        PdcData = await notify.getNotificationPdc();
                        return Json(PdcData, JsonRequestBehavior.AllowGet);
                    case StaticHelper.NOTIFICATION_RENEWAL:
                        RenewalData = await notify.getNotificationRenewal();
                        return Json(RenewalData, JsonRequestBehavior.AllowGet);
                    case StaticHelper.NOTIFICATION_RENTAL_DUE:
                        RentalDueData = await notify.getNotificationRentalDue();
                        return Json(RentalDueData, JsonRequestBehavior.AllowGet);
                    case StaticHelper.NOTIFICATION_UTILITY_DUES:
                        UtilityDuesData = await notify.getNotificationUtilityDues();
                        return Json(UtilityDuesData, JsonRequestBehavior.AllowGet);
                    case StaticHelper.NOTIFICATION_AGREEMENT_APPROVAL:
                        AgreementData = await notify.getNotificationAgreementApproval();
                        return Json(AgreementData, JsonRequestBehavior.AllowGet);
                    default :
                        return Json(data, JsonRequestBehavior.AllowGet);
                }                
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult getNotificationMenu()
        {
            try
            {
                var data = StaticHelper.GetStaticData(StaticHelper.NOTIFICATION_MENU);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw ex;
            }
         }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}