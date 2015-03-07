using LicenseServer.Core.Interfaces;
using LicenseServer.Core.Modelos;
using LicenseServer.Data.Repositorios.Commom;
using MongoDB.Driver;

namespace LicenseServer.Data.Repositorios
{
    public class Licenses : RepositoryBase<License>, ILicenses
    {
        public Licenses(MongoDatabase dataBase)
            : base(dataBase)
        {
        }

        public License FindByAppKey(string appkey)
        {
            throw new System.NotImplementedException();
        }
    }
}
