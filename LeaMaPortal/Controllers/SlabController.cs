using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LeaMaPortal.Models.DBContext;
using LeaMaPortal.Models;
using MvcPaging;
using LeaMaPortal.Helpers;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace LeaMaPortal.Controllers
{
    public class SlabController : Controller
    {
        private Entities db = new Entities();

        // GET: Slab
        public PartialViewResult Index(string Search, int? page, int? defaultPageSize)
        {
            try
            {
                ViewData["Search"] = Search;
                int currentPageIndex = page.HasValue ? page.Value : 1;
                int PageSize = defaultPageSize.HasValue ? defaultPageSize.Value : PagingProperty.DefaultPageSize;
                ViewBag.defaultPageSize = new SelectList(PagingProperty.DefaultPagelist, defaultPageSize);
                IList<SlabViewModel> list;
                if (string.IsNullOrWhiteSpace(Search))
                {
                    list = db.tbl_slabmaster.Where(x => x.Delmark != "*").OrderBy(x => x.id).Select(x => new SlabViewModel()
                    {
                        Id = x.id,
                        SlabId = x.slabid,
                        Unit_From = x.Unit_From,
                        Unit_to = x.Unitto,
                        Colour = x.Colour,
                        Residence_type = x.Residence_type,
                        Utility_id = x.Utility_id,
                        Utility_Name = x.Utility_Name,
                        rate = x.rate,
                    }).ToPagedList(currentPageIndex, PageSize);
                }
                else
                {
                    list = db.tbl_slabmaster.Where(x => x.Delmark != "*"
                                    && (x.slabid.ToString().ToLower().Contains(Search.ToLower())
                                    || x.Residence_type.ToLower().Contains(Search.ToLower())
                                    || x.Utility_Name.ToLower().Contains(Search.ToLower())))
                                  .OrderBy(x => x.id).Select(x => new SlabViewModel()
                                  {
                                      Id = x.id,
                                      SlabId = x.slabid,
                                      Unit_From = x.Unit_From,
                                      Unit_to = x.Unitto,
                                      Colour = x.Colour,
                                      Residence_type = x.Residence_type,
                                      Utility_id = x.Utility_id,
                                      Utility_Name = x.Utility_Name,
                                      rate = x.rate,
                                  }).ToPagedList(currentPageIndex, PageSize);
                }

                return PartialView("../Master/SlabMaster/_List", list);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        [HttpGet]
        public PartialViewResult AddOrUpdate()
        {
            SlabViewModel model = new SlabViewModel();
            ViewBag.Utility_Name = new SelectList(db.tbl_utilitiesmaster.OrderBy(o => o.Utility_Name).Distinct(), "Utility_Name", "Utility_Name");
            ViewBag.Colour = new SelectList(StaticHelper.GetStaticData(StaticHelper.SLAB_COLOUR_DROPDOWN), "Name", "Name");
            //ViewBag.Unit_id = new SelectList(db.tbl_propertiesmaster.OrderBy(o => o.Unit_Property_Name).Distinct(), "Unit_Property_Name", "Unit_Property_Name");
            ViewBag.Residence_type = new SelectList(StaticHelper.GetStaticData(StaticHelper.SLAB_RESIDENCE_DROPDOWN), "Name", "Name");
            model.SlabId = db.tbl_slabmaster.OrderByDescending(o => o.id).Select(s => s.slabid).FirstOrDefault();
            model.SlabId = Convert.ToInt32(model.SlabId) + 1;
            return PartialView("../Master/SlabMaster/_AddOrUpdate", model);
        }
        // POST: CheckList/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> AddOrUpdate([Bind(Include = "SlabId,Utility_id,Utility_Name,Unit_From,Unit_to,Residence_type,Colour,rate,Id")] SlabViewModel model)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (ModelState.IsValid)
                {
                    MySqlParameter pa = new MySqlParameter();
                    string PFlag = "INSERT";

                    if (model.Id == 0)
                    {

                    }
                    else
                    {
                        PFlag = "UPDATE";
                    }
                    //tbl_slabmaster slab = new tbl_slabmaster();
                    //slab.slabid = model.SlabId;
                    //slab.Unit_From = model.Unit_From;
                    //slab.Unitto = model.Unit_to;
                    //slab.Utility_id = model.Utility_id;
                    //slab.Utility_Name = model.Utility_Name;
                    //slab.rate = model.rate;
                    //slab.Colour = model.Colour;
                    //slab.Residence_type = model.Residence_type;
                    //db.tbl_slabmaster.Add(slab);
                    object[] param = { new MySqlParameter("@PFlag", PFlag),
                                           new MySqlParameter("@PUtility_id",model.Utility_id),
                                            new MySqlParameter("@PUtility_Name",model.Utility_Name),
                                            new MySqlParameter("@Pslabid",model.SlabId),
                                            new MySqlParameter("@PUnit_From",model.Unit_From),
                                            new MySqlParameter("@PUnitto",model.Unit_to),
                                            new MySqlParameter("@Prate",model.rate),
                                            new MySqlParameter("@PColour",model.Colour),
                                            new MySqlParameter("@PResidence_type",model.Residence_type),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                    var RE = await db.Database.SqlQuery<object>("CALL Usp_Slabmaster_All(@PFlag,@PUtility_id,@PUtility_Name,@Pslabid,@PUnit_From,@PUnitto,@Prate,@PColour,@PResidence_type,@PCreateduser)", param).ToListAsync();
                    await db.SaveChangesAsync();
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> Edit(int Id)
        {
            try
            {
                if (Id == 0)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_slabmaster tbl_slab = await db.tbl_slabmaster.FindAsync(Id);
                if (tbl_slab == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                SlabViewModel model = new SlabViewModel()
                {
                    Id = tbl_slab.id,
                    SlabId = tbl_slab.slabid,
                    Unit_From = tbl_slab.Unit_From,
                    Unit_to = tbl_slab.Unitto,
                    Colour = tbl_slab.Colour,
                    Residence_type = tbl_slab.Residence_type,
                    Utility_id = tbl_slab.Utility_id,
                    Utility_Name = tbl_slab.Utility_Name,
                    rate = tbl_slab.rate,
                };
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<ActionResult> Delete(int Id)
        {
            MessageResult result = new MessageResult();
            try
            {
                if (Id == 0)
                {
                    return Json(new MessageResult() { Errors = "Bad request" }, JsonRequestBehavior.AllowGet);
                }
                tbl_slabmaster tbl_slab = await db.tbl_slabmaster.FindAsync(Id);
                if (tbl_slab == null)
                {
                    return Json(new MessageResult() { Errors = "Not found" }, JsonRequestBehavior.AllowGet);
                }
                object[] param = { new MySqlParameter("@PFlag", "DELETE"),
                                           new MySqlParameter("@PUtility_id",tbl_slab.Utility_id),
                                            new MySqlParameter("@PUtility_Name",tbl_slab.Utility_Name),
                                            new MySqlParameter("@Pslabid",tbl_slab.slabid),
                                            new MySqlParameter("@PUnit_From",tbl_slab.Unit_From),
                                            new MySqlParameter("@PUnitto",tbl_slab.Unitto),
                                            new MySqlParameter("@Prate",tbl_slab.rate),
                                            new MySqlParameter("@PColour",tbl_slab.Colour),
                                            new MySqlParameter("@PResidence_type",tbl_slab.Residence_type),
                                           new MySqlParameter("@PCreateduser",System.Web.HttpContext.Current.User.Identity.Name)
                                         };
                var spResult = await db.Database.SqlQuery<object>("Usp_Slabmaster_All(@PFlag,@PId,@PUtility_id,@PUtility_Name,@Pslabid,@PUnit_From,@PUnitto,@Prate,@PColour,@PResidence_type,@PCreateduser)", param).ToListAsync();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new MessageResult() { Errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        //[HttpGet]
        public async Task<string> GetUtilityId(string UtilityName)
        {
            try
            {
                var utility = await db.tbl_utilitiesmaster.FirstOrDefaultAsync(f => f.Utility_Name == UtilityName);
                return utility.Utility_id;
            }
            catch (Exception ex)
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
