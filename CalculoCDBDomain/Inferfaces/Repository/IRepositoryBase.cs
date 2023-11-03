using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculoCDBDomain.Inferfaces.Repository;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    void CriarBancoSQLite();
    Task Add(TEntity entity);
    Task CreateTable(string commandCreateTable);
    Task<TEntity> GetById(int id);
    Task<IEnumerable<TEntity>> GetAll();
    Task Update(TEntity obj);
    Task Remove(TEntity obj);
    Task Dispose();
}