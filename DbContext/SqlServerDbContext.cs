using Microsoft.EntityFrameworkCore;

namespace BuildingMaterials.DbContext
{
    public class SqlServerDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {

        }
    }
}
