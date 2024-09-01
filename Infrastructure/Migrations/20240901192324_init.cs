using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelContents",
                columns: table => new
                {
                    ExcelFileName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelContents", x => x.ExcelFileName);
                });

            migrationBuilder.CreateTable(
                name: "ExcelContentRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ExcelFileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ColumnsValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelContentRows", x => new { x.ExcelFileName, x.Id });
                    table.ForeignKey(
                        name: "FK_ExcelContentRows_ExcelContents_ExcelFileName",
                        column: x => x.ExcelFileName,
                        principalTable: "ExcelContents",
                        principalColumn: "ExcelFileName",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelContentRows");

            migrationBuilder.DropTable(
                name: "ExcelContents");
        }
    }
}
