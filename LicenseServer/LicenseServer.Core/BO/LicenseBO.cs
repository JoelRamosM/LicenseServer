using System;
using System.Runtime.Remoting.Messaging;
using LicenseServer.Core.Interfaces;
using LicenseServer.Core.Interfaces.BO;

namespace LicenseServer.Core.BO
{
    public class LicenseBO : ILicenseBO
    {
        private readonly ILicenses todasLicencas;

        public LicenseBO(ILicenses todasLicencas)
        {
            this.todasLicencas = todasLicencas;
        }

        public bool IsAppValid(string appKey)
        {
            var licensa = todasLicencas.FindByAppKey(appKey);
            if (licensa == null) throw new Exception("Licensa não encontrada");
            return licensa.Validade.Date <= DateTime.Today;
        }
    }
}
