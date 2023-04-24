using EVoucherMgmtAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoucherMgmtAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = "server=localhost;database=evoucher_mgmt;uid=root;password=root;";
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            optionsBuilder.UseMySql(connectionString, serverVersion);
        }

        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<EVoucher> EVoucher { get; set; }
        public DbSet<PromoCode> PromoCode { get; set; }
    }
}
