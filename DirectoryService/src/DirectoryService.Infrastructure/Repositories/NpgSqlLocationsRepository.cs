using CSharpFunctionalExtensions;
using Dapper;
using DirectoryService.Application.Locations;
using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectoryService.Infrastructure.Repositories;

public class NpgSqlLocationsRepository : ILocationsRepository
{
    private readonly DirectoryServiceDbContext _dbContext;
    private readonly ILogger<NpgSqlLocationsRepository> _logger;

    public NpgSqlLocationsRepository(DirectoryServiceDbContext dbContext, ILogger<NpgSqlLocationsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid, string>> AddAsync(Location location, CancellationToken cancellationToken = default)
    {
        var connection = _dbContext.Database.GetDbConnection();
        using var transaction = await connection.BeginTransactionAsync(cancellationToken);
        try
        {
            const string locationInsertSql = @"
            INSERT INTO locations (id, name, timezone, active, created_at, updated_at, addresses)
            VALUES (@Id, @Name, @Timezone, @Active, @CreatedAt, @UpdatedAt, @Addresses);
            ";

            var locationInsertParams = new
            {
                Id = location.Id.Value,
                Name = location.Name.Value,
                Timezone = location.Timezone.Value,
                Active = location.IsActive,
                CreatedAt = location.CreatedAt,
                UpdatedAt = location.UpdatedAt,
                Addresses = location.Address,
            };
            await connection.ExecuteAsync(locationInsertSql, locationInsertParams, transaction);

            if (location.Departments?.Any() == true)
            {
                const string insertDeptSql = @"
                    INSERT INTO department_locations (id, department_id, location_id, some_other_column)
                    VALUES (@Id, @DepartmentId, @LocationId, @SomeOther);
                ";

                var deptParams = location.Departments.Select(d => new
                {
                    Id = d.Id,
                    DepartmentId = d.DepartmentId.Value,
                    LocationId = location.Id.Value,
                }).ToList();

                if (deptParams.Count > 0)
                    await connection.ExecuteAsync(insertDeptSql, deptParams, transaction);
            }

            transaction.Commit();
            return location.Id.Value;
        }
        catch(Exception ex)
        {
             transaction.Rollback();

             _logger.LogError(ex, "Failed to insert location");
 
             return Result.Failure<Guid, string>(ex.Message);
        }
    }
}