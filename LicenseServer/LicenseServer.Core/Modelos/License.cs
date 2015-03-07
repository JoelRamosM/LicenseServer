using System;
using LicenseServer.Core.Modelos.Commom;

namespace LicenseServer.Core.Modelos
{
    public class License : Entidade
    {
        public string AppKey { get; set; }
        public string Produto { get; set; }
        public string Empresa { get; set; }
        public DateTime Validade { get; set; }
        public DateTime Criacao { get; set; }
    }
}
