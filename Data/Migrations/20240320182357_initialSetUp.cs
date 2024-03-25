using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgeWebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class initialSetUp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "knowledge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    generalQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    generalAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_knowledge", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "knowledge");
        }
    }
}
