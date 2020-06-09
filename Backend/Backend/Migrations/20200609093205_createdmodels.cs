using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class createdmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "board",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_board", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "column",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoardId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_column", x => x.Id);
                    table.ForeignKey(
                        name: "FK_column_board_BoardId",
                        column: x => x.BoardId,
                        principalTable: "board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ColumnId = table.Column<int>(nullable: false),
                    BoardId = table.Column<int>(nullable: false),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_card_board_BoardId",
                        column: x => x.BoardId,
                        principalTable: "board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_card_column_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_card_BoardId",
                table: "card",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_card_ColumnId",
                table: "card",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_column_BoardId",
                table: "column",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "column");

            migrationBuilder.DropTable(
                name: "board");
        }
    }
}
