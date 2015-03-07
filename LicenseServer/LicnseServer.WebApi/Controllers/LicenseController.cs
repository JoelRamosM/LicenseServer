using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using LicenseServer.Core.Interfaces;
using LicenseServer.Core.Interfaces.BO;
using LicenseServer.Core.Modelos;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;


namespace LicnseServer.WebApi.Controllers
{
    public class LicenseController : ApiController
    {
        private readonly ILicenseBO licensaBO;
        private readonly ILicenses todasLicenses;

        public LicenseController(ILicenseBO licensaBO, ILicenses todasLicenses)
        {
            this.licensaBO = licensaBO;
            this.todasLicenses = todasLicenses;
        }

        [HttpGet]
        public JsonResult<object> Eco()
        {
            return new JsonResult<object>(new { Teste = "Echo", Teste2 = "Eco 2" }, new JsonSerializerSettings(), Encoding.UTF8, this);
        }

        [HttpGet]
        public JsonResult<object> ValidaApp(string appKey)
        {
            try
            {
                var auth = licensaBO.IsAppValid(appKey);
                var message = auth ? "Licensa válida!" : "Licensa inválida!!";

                return new JsonResult<object>(new { Auth = auth, Message = message }, new JsonSerializerSettings(), Encoding.UTF8, this);
            }
            catch (Exception e)
            {
                ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
                return new JsonResult<object>(new { Auth = false, Message = e.Message }, new JsonSerializerSettings(), Encoding.UTF8, this);
            }
        }

        [HttpPost]
        public JsonResult<object> IncluirLicense([FromBody]License license)
        {
            try
            {
                //var licensa = todasLicenses.Include(new License() { AppKey = Guid.NewGuid().ToString(), Criacao = DateTime.Today, Empresa = "Testando API", Validade = DateTime.Today.AddDays(5), Produto = "Gestao Gas" });
                var licensa = todasLicenses.Include(license);
                return new JsonResult<object>(licensa, new JsonSerializerSettings(), Encoding.UTF8, this);

            }
            catch (Exception e)
            {
                ResponseMessage(new HttpResponseMessage(HttpStatusCode.BadRequest));
                return new JsonResult<object>(new { Auth = false, Message = e.Message }, new JsonSerializerSettings(), Encoding.UTF8, this);
            }
        }
    }
}
