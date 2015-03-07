using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LicnseServer.WebApi.Controllers
{
    public class LicensesController : Controller
    {
        // GET: Licenses
        public ActionResult Index()
        {
            return View();
        }
    }
}