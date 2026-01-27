using System.Data;

namespace DirectoryService.Infrastructure;

public interface IDbConnectionFactory
{
     Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
}