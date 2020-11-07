using Microsoft.EntityFrameworkCore.Migrations;

namespace Shopping.Server.Data.Migrations.ApplicationDb
{
    public partial class StandardUserGroupId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StandardUserGroupId",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StandardUserGroupId",
                table: "AspNetUsers");
        }
    }
}
