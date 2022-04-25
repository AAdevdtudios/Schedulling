using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedulling.Migrations
{
    public partial class doa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DOA",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOA",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Members");
        }
    }
}
