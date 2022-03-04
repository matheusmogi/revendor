using Microsoft.EntityFrameworkCore.Migrations;

namespace Revendor.Infrastructure.Migrations
{
    public partial class SingleMail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAddress");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "Customer",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailLabel",
                table: "Customer",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "EmailLabel",
                table: "Customer");

            migrationBuilder.CreateTable(
                name: "EmailAddress",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddress", x => new { x.CustomerId, x.Id });
                    table.ForeignKey(
                        name: "FK_EmailAddress_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
