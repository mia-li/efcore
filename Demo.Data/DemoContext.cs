using Demo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Data
{
    public class DemoContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(ConsoleLoggerFactory)
                .UseSqlServer(
               connectionString: "Data Source=localhost;Initial Catalog=Demo;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GamePlayer>().HasKey(x => new { x.PlayerId, x.GameId });
            //设置联合主键
            modelBuilder.Entity<Resume>()
                .HasOne(x => x.Player) //resume 只能绑定一个player
                .WithOne(x => x.Resume) //它绑定的player只有能一个resume 1对1
                .HasForeignKey<Resume>(x => x.PlayerId); //resume的外键是PlayerId
        }
        public DbSet<League> Leagues { get; set; } 
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Game> Games { get; set; }
        //实际在SQL server里对应创建的表名就是 Leagues、Clubs、Players...

        //用来打印具体操作的sql语句的
        public static readonly ILoggerFactory ConsoleLoggerFactory = LoggerFactory.Create(builder =>
          {
              builder.AddFilter((category, level) =>
              category == DbLoggerCategory.Database.Command.Name
              && level == LogLevel.Information)
              .AddConsole();
          });
    }
}
