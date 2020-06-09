using Microsoft.EntityFrameworkCore;

namespace Backend
{
        public class KanBanContext : DbContext 
        {

            public KanBanContext(DbContextOptions<KanBanContext> options): base(options){}
                public DbSet<Board> board {get; set;}
                public DbSet<Card> card {get; set;}
                public DbSet<Column> column {get; set;}

                protected override void OnConfiguring(DbContextOptionsBuilder options)
                {

                        options.UseSqlite("Data source =Kanban.db;");
                }

        }
}