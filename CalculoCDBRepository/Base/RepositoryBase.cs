using CalculoCDBService.Inferfaces.Repository;
using CalculoCDBShared.GenerateSQL;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCDBRepository.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        const string BancoCreatePathTest = @"C:\Testes\CDB.db";
        const string BancoAccessPathTest = @"Data Source=c:\\Testes\\CDB.db;";


        #region "Public Methods"
        /// <summary>
        /// Banco só deve ser criado uma vez e todas as chamadas executadas meramente ilustrativo
        /// </summary>
        public void CriarBancoSQLite()
        {
            try
            {
                if (!File.Exists(BancoCreatePathTest))
                {
                    StreamWriter file = new StreamWriter(BancoCreatePathTest, true, Encoding.Default);
                    file.Dispose();
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task Add(TEntity entity)
        {
            var properties = GenerateCommandSQL.ParseProperties((object)entity);
            var table = NameTable();
            var sql = string.Format("INSERT INTO [{0}] ({1}) VALUES(@{2})", table, string.Join(", ", properties.ValueNames), string.Join(", @", properties.ValueNames));

            try
            {
                using (var cmd = new SqliteConnection(BancoAccessPathTest))
                {
                    await cmd.ExecuteAsync(sql, entity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task CreateTable(string commandCreateTable)
        {
            try
            {
                using (var cmd = new SqliteConnection(BancoAccessPathTest))
                {
                    await cmd.ExecuteAsync(commandCreateTable);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task Dispose()
        {
            File.Delete(BancoCreatePathTest);
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var table = NameTable();
            try
            {
                using (var cmd = new SqliteConnection(BancoAccessPathTest))
                {
                    return cmd.QueryAsync<TEntity>($@"SELECT * FROM {table}").Result.AsList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<TEntity> GetById(int id)
        {
            var table = NameTable();
            try
            {
                using (var cmd = new SqliteConnection(BancoAccessPathTest))
                {
                    return await cmd.QueryFirstAsync<TEntity>($@"SELECT * FROM {table} WHERE id = {id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public Task Remove(TEntity obj)
        {
            throw new System.NotImplementedException();
        }
        public Task Update(TEntity obj)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region "Private Methods"
        private string NameTable()
        {
            var nameTable = typeof(TEntity).GetCustomAttributes(true);
            var nameTableIndetity = nameTable[0].GetType().GetProperty("Name").GetValue(nameTable[0]);
            return (string)nameTableIndetity;
        }

        #endregion


    }
}