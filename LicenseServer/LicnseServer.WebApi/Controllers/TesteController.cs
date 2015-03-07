using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LicenseServer.Core.Interfaces.BO;

namespace LicnseServer.WebApi.Controllers
{
    public class TesteController : Controller
    {
        private readonly ILicenseBO licenseBO;

        public TesteController(ILicenseBO licenseBO)
        {
            this.licenseBO = licenseBO;
        }

        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }
    }
}