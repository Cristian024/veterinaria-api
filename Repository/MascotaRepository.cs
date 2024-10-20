using Dapper;
using Microsoft.Data.Sqlite;
using Models;
using System.Data;
using Repository.Aplication;

namespace Repository;

public class MascotaRepository : IMascotaRepository
{
    private readonly string? _connectionString;
    public MascotaRepository(IConfiguration configuration){
        _connectionString = configuration.GetConnectionString("SqLiteConnection");
    }
    private IDbConnection Connection => new SqliteConnection(_connectionString);
    public Task AddAsync(Mascota entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Mascota>> GetAllAsync()
    {
        using (var connection = Connection)
        {
            return connection.QueryAsync<Mascota>("SELECT * FROM Mascotas");
        }
    }

    public Task<Mascota> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Mascota entity)
    {
        throw new NotImplementedException();
    }
}