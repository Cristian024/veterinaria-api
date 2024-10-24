using Dapper;
using Helpers;
using Microsoft.Data.Sqlite;
using Models;
using Repository.Aplication;

namespace Repository;

public class ClienteRepository : IClienteRepository
{
    private readonly string? _connectionString;
    public ClienteRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqLiteConnection");
    }
    private SqliteConnection Connection => new SqliteConnection(_connectionString);
    public async Task AddAsync(Cliente entity)
    {
        var clienteDto = new ClienteDto
        {
            Cli_Id = null,
            Per_Id = entity.Per_Id
        };

        using (var connection = Connection)
        {
            entity.Cli_Id = null;

            var insertQuery = $@"
            INSERT INTO Clientes({Database.GetNameAttributes(clienteDto)}) 
            VALUES({Database.GetNameAttributesInsertion(clienteDto)});";

            Console.WriteLine(insertQuery);

            await connection.OpenAsync();

            using (var insertCommand = new SqliteCommand(insertQuery, connection))
            {
                Database.AddParametersInNonQuery(insertCommand, clienteDto);

                await insertCommand.ExecuteNonQueryAsync();

                var lastInsertIdQuery = "SELECT last_insert_rowid();";
                using (var lastInsertCommand = new SqliteCommand(lastInsertIdQuery, connection))
                {
                    var lastInsertedId = await lastInsertCommand.ExecuteScalarAsync();

                    var id = Convert.ToInt32(lastInsertedId);

                    var lastCliente = await GetByIdAsync(id);

                    if (lastCliente != null)
                    {
                        entity.Cli_Id = lastCliente.Cli_Id;
                        entity.Persona = lastCliente.Persona;
                    }
                }
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var connection = Connection)
        {
            await connection.QueryFirstOrDefaultAsync<Cliente>("DELETE FROM Clientes WHERE Cli_Id = @id", new { id });
        }
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        using (var connection = Connection)
        {
            var query = $"SELECT c.Cli_Id, c.Per_Id, p.* FROM Clientes c LEFT JOIN Personas p ON c.Per_Id = p.Per_Id";

            var clientes = await connection.QueryAsync<Cliente, Persona, Cliente>(query, (cliente, persona) =>
            {
                cliente.Persona = persona;
                return cliente;
            }, splitOn: "Per_Id");

            return clientes;
        }
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        using (var connection = Connection)
        {
            var query = $"SELECT c.Cli_Id, c.Per_Id, p.* FROM Clientes c LEFT JOIN Personas p ON c.Per_Id = p.Per_Id WHERE c.Cli_Id = @id";

            var cliente = await connection.QueryAsync<Cliente, Persona, Cliente>(query, (cliente, persona) =>
            {
                cliente.Persona = persona;
                return cliente;
            }, new { id }, splitOn: "Per_Id");

            return cliente.FirstOrDefault();
        }
    }

    public Task UpdateAsync(Cliente entity)
    {
        throw new NotImplementedException();
    }
}