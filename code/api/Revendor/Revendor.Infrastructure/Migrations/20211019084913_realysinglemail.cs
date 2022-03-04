using Microsoft.EntityFrameworkCore.Migrations;

namespace Revendor.Infrastructure.Migrations
{
    public partial class realysinglemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailLabel",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Customer",
                newName: "Email");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Customer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Customer",
                newName: "EmailAddress");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailLabel",
                table: "Customer",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
