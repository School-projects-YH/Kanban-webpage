using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
        public class KanBanContext : DbContext 
        {

            public KanBanContext(DbContextOptions<KanBanContext> options): base(options){}
                public DbSet<Board> Board {get; set;}
                public DbSet<Card> Card {get; set;}
                public DbSet<Column> Column {get; set;}

                protected override void OnConfiguring(DbContextOptionsBuilder options)
                {

                        options.UseSqlite("Data source =Kanban.db;");
                }

        }
}