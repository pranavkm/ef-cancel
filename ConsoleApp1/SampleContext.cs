using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class SampleContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        public async Task LongTask(CancellationToken cancellationToken = default)
        {
            await Database.ExecuteSqlRawAsync("WAITFOR DELAY '0:0:2'", cancellationToken);
        }
    }
}
