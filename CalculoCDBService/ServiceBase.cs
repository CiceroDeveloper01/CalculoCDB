using CalculoCDBService.Inferfaces;
using CalculoCDBService.Inferfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CalculoCDBService
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
            _repository.CriarBancoSQLite();
        }

        public async Task Add(TEntity obj)
        {
            await _repository.Add(obj);
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Update(TEntity obj)
        {
            await _repository.Update(obj);
        }

        public async Task Remove(TEntity obj)
        {
            await _repository.Remove(obj);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}