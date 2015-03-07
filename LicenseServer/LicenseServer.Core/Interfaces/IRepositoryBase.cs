using System.Linq;
using LicenseServer.Core.Modelos.Commom;

namespace LicenseServer.Core.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : Entidade
    {
        TEntity Find(string id);
        TEntity Include(TEntity entidade);
        void Delete(string id);
        IQueryable<TEntity> All();
    }
}
