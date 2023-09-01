namespace KampusSggwBackend.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    // Const
    // public const string CONNECTION_STRING = @"YOUR_CONNECTION_STRING_HERE";
    public const string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Initial Catalog=KampusSggwDatabase;Persist Security Info=True;User ID=Fabiano2;Password=1qaz@WSXC";

    // Service
    private DataContext _dbContext;

    // Properties
    public DataContext DataContext { get { return _dbContext; } }

    // Methods
    public DataContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(CONNECTION_STRING, b => b.MigrationsAssembly("KampusSggwBackend.Data"));
        optionsBuilder.EnableSensitiveDataLogging();
        _dbContext = new DataContext(optionsBuilder.Options);

        return _dbContext;
    }
}
