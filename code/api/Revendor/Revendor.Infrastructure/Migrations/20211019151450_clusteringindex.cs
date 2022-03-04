using Microsoft.EntityFrameworkCore.Migrations;

namespace Revendor.Infrastructure.Migrations
{
    public partial class clusteringindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" alter table [user] drop CONSTRAINT FK_User_Tenant_TenantId;");
            migrationBuilder.Sql(@" alter table [customer] drop CONSTRAINT FK_Customer_Tenant_TenantId;");
            migrationBuilder.Sql(@" alter table [Phone] drop CONSTRAINT FK_Phone_Customer_CustomerId;");
          
 
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");
            
            migrationBuilder.Sql(@" truncate table [customer];");
            migrationBuilder.Sql(@" truncate table [user];");
            migrationBuilder.Sql(@" truncate table [Tenant];");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "User",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "Tenant",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "Customer",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.InsertData(
                table: "Tenant",
                columns: new[] { "Id", "Name" },
                values: new object[] { "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d", "Test Tenant" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Active", "Password", "Role", "TenantId", "Username" },
                values: new object[] { "655ecf50-191a-4322-8a24-decec9f92116", true, "$2a$11$ckpeXAuckb7iNaMImlT.B.LMFiMJMIeoPh43rAnugHPObXV9g5b0O", 1, null, "revendor.admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Active", "Password", "Role", "TenantId", "Username" },
                values: new object[] { "4ff9fe1a-4d14-41c4-a03b-cf351ed9f512", true, "$2a$11$ckpeXAuckb7iNaMImlT.B.LMFiMJMIeoPh43rAnugHPObXV9g5b0O", 2, "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_User_ClusterId",
                table: "User",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_ClusterId",
                table: "Tenant",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_ClusterId",
                table: "Customer",
                column: "ClusterId")
                .Annotation("SqlServer:Clustered", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_ClusterId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant");

            migrationBuilder.DropIndex(
                name: "IX_Tenant_ClusterId",
                table: "Tenant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_ClusterId",
                table: "Customer");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "4ff9fe1a-4d14-41c4-a03b-cf351ed9f512");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: "655ecf50-191a-4322-8a24-decec9f92116");

            migrationBuilder.DeleteData(
                table: "Tenant",
                keyColumn: "Id",
                keyValue: "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "User",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tenant",
                table: "Tenant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");
        }
    }
}
