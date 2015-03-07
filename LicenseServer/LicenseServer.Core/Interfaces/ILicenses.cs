using System.Linq;
using LicenseServer.Core.Modelos;

namespace LicenseServer.Core.Interfaces
{
    public interface ILicenses : IRepositoryBase<License>
    {

        License FindByAppKey(string appkey);

    }
}
