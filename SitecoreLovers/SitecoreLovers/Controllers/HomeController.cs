using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SitecoreLovers.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Sitecore.Data.Database db = Sitecore.Configuration.Factory.GetDatabase("master");
            var Item = db.GetItem("/sitecore/content/Home");
            return View();
        }
    }
}