using BussinesLayer.Concrate;
using DataAccessLayer.Concrate;
using EntityLaye.Concrate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sehir.Controllers
{
    public class EkleController : Controller
    {
        // GET: Ekle
        EkleManager ek = new EkleManager();
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AddNew()
        {



            List<SelectListItem> values = (from x in c.Admins.ToList() select new SelectListItem { Text = x.UserName, Value = x.AdminID.ToString() }).ToList();
            ViewBag.values = values;
         


            return View();

        }
        [HttpPost]
        public ActionResult AddNew(Ekle b)
        {
            Context c = new Context();
            string dosyaadı = Path.GetFileName(Request.Files[0].FileName); //ilk önce boş dosya gönderiyorum
            string uzantı = Path.GetExtension(Request.Files[0].FileName);
            string yol = "~/Image/" + dosyaadı + uzantı;
            Request.Files[0].SaveAs(Server.MapPath(yol));
            b.resim = "/Image/" + dosyaadı + uzantı;
            c.Users.Add(b);
            c.SaveChanges();

            return RedirectToAction("AdminList", "Ekle");
        }
        public ActionResult AdminList()
        {
            var UserList = ek.GetAll();
            return View(UserList);
        }
        public ActionResult BlogGit(int id)
        {
            var EkleDetails = ek.EkleByID(id);
            return View(EkleDetails);

        }
        public ActionResult DeleteAdmin(int id)
        {
            ek.DeleteEkle(id);
            return RedirectToAction("AdminList");
        }
  


    }
}