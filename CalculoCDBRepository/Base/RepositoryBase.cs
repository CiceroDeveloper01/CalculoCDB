using CalculoCDBDomain.Inferfaces.Repository;
using CalculoCDBService.Taxas;
using CalculoCDBShared.GenerateSQL;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCDBRepository.Base;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    string BancoCreatePathTest = "";
    string BancoAccessPathTest = "";

    protected IConfiguration _configuration;
    private readonly ILogger<RepositoryBase<TEntity>> _logger;

    public RepositoryBase(IConfiguration configuration, ILogger<RepositoryBase<TEntity>> logger)
    {
        _logger = logger;
        _configuration = configuration;
        BancoCreatePathTest = _configuration.GetConnectionString("CreateDataBase");
        BancoAccessPathTest = _configuration.GetConnectionString("DataBase");
    }

    #region "Public Methods"
    /// <summary>
    /// Banco só deve ser criado uma vez e todas as chamadas executadas meramente ilustrativo
    /// </summary>
    public void CriarBancoSQLite()
    {
        try
        {
            _logger.LogInformation($@"Iniciando a Base Repository: {typeof(RepositoryBase<TEntity>)} e o Método: CriarBancoSQLite");
            if (!File.Exists(BancoCreatePathTest))
            {
                _logger.LogInformation($@"Criando a Base Repository: {typeof(RepositoryBase<TEntity>)} e o Método: CriarBancoSQLite");
                StreamWriter file = new StreamWriter(BancoCreatePathTest, true, Encoding.Default);
                file.Dispose();
            }
            else
                _logger.LogInformation($@"A Base Repository: {typeof(RepositoryBase<TEntity>)} e o Método: CriarBancoSQLite já está criada");
        }
        catch(Exception ex) 
        {
            _logger.LogInformation($@"Erro Repository: {typeof(RepositoryBase<TEntity>)} e o Método: CriarBancoSQLite a Mensagem: {ex.Message}");
        }
    }
    public async Task Add(TEntity entity)
    {
        _logger.LogInformation($@"Executando o Repository: {typeof(RepositoryBase<TEntity>)} e o Método: Add");
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
            _logger.LogInformation($@"Erro Repository: {typeof(RepositoryBase<TEntity>)} e o Add a Mensagem: {ex.Message}");
        }
    }
    public async Task CreateTable(string commandCreateTable)
    {
        _logger.LogInformation($@"Executando o Repository: {typeof(RepositoryBase<TEntity>)} e o Método: CreateTable");
        try
        {
            using (var cmd = new SqliteConnection(BancoAccessPathTest))
            {
                await cmd.ExecuteAsync(commandCreateTable);
            }
        }
        catch (Exception ex)
        {
            _logger.LogInformation($@"Erro Repository: {typeof(RepositoryBase<TEntity>)} e o Add a Mensagem: {ex.Message}");
        }
    }
    public async Task Dispose()
    {
        File.Delete(BancoCreatePathTest);
    }
    public async Task<IEnumerable<TEntity>> GetAll()
    {
        _logger.LogInformation($@"Executando o Repository: {typeof(RepositoryBase<TEntity>)} e o Método: GetAll");
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
            _logger.LogInformation($@"Erro Repository: {typeof(RepositoryBase<TEntity>)} e o getAll a Mensagem: {ex.Message}");
            return null;
        }
    }
    public async Task<TEntity> GetById(int id)
    {
        _logger.LogInformation($@"Executando o Repository: {typeof(RepositoryBase<TEntity>)} e o Método: GetById");
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
            _logger.LogInformation($@"Erro Repository: {typeof(RepositoryBase<TEntity>)} e o Add a Mensagem: {ex.Message}");
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