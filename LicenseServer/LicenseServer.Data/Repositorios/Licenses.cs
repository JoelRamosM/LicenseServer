using System;
using LicenseServer.Core;
using LicenseServer.Core.Interfaces;
using LicenseServer.Core.Modelos;
using LicenseServer.Data.Repositorios.Commom;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace LicenseServer.Data.Repositorios
{
    public class Licenses : RepositoryBase<License>, ILicenses
    {
        public Licenses(ConnStr connStr)
            : base(connStr)
        {
        }

        public License FindByAppKey(string appkey)
        {
            var query = Query.EQ("AppKey", appkey);
            return collection.FindOne(query);
        }

        public License Include(License entidade)
        {
            entidade.AppKey = Guid.NewGuid().ToString();
            entidade.Criacao = DateTime.Now;
            return base.Include(entidade);
        }
    }
}
