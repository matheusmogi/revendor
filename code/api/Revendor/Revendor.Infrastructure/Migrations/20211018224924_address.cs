using Microsoft.EntityFrameworkCore.Migrations;

namespace Revendor.Infrastructure.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "Customer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "Customer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Neighbourhood",
                table: "Customer",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Customer",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetNumber",
                table: "Customer",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Customer",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Customer",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Neighbourhood",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customer",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }
    }
}
