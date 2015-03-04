using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;


namespace LicnseServer.WebApi.Controllers
{
    public class LicenseController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "Testando API!!!";
        }
    }
}
