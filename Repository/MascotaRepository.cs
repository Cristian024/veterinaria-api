using Dapper;
using Microsoft.Data.Sqlite;
using Models;
using System.Data;
using Repository.Aplication;
using Helpers;
using SQLitePCL;

namespace Repository;

public class MascotaRepository : IMascotaRepository
{
    private readonly string? _connectionString;
    public MascotaRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqLiteConnection");
    }
    private SqliteConnection Connection => new SqliteConnection(_connectionString);
    public async Task AddAsync(Mascota entity)
    {
        using (var connection = Connection)
        {
            await connection.ExecuteAsync(
                $"INSERT INTO Mascotas({Database.GetNameAttributes(entity)}) VALUES({Database.GetNameAttributesInsertion(entity)})",
                entity
            );
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = Connection)
        {
            await connection.QueryFirstOrDefaultAsync<Mascota>("DELETE FROM Mascotas WHERE mas_id = @id", new { id });
        }
    }

    public async Task<IEnumerable<Mascota>> GetAllAsync()
    {
        using (var connection = Connection)
        {
            return await connection.QueryAsync<Mascota>("SELECT * FROM Mascotas");
        }
    }

    public async Task<Mascota?> GetByIdAsync(int id)
    {
        using (var connection = Connection)
        {
            return await connection.QueryFirstOrDefaultAsync<Mascota>("SELECT * FROM Mascotas WHERE mas_id = @id", new { id });
        }
    }

    public async Task UpdateAsync(Mascota mascota)
    {
        using (var connection = Connection)
        {
            await connection.QueryAsync<Mascota>(
                $"UPDATE Mascotas SET {Database.GetNameAttributesUpdate(mascota)} WHERE Mas_Id = @Mas_Id"
                , mascota
            );
        }
    }
}