using Microsoft.EntityFrameworkCore.Migrations;

namespace Schedulling.Migrations
{
    public partial class phonechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Messages",
                table: "Phones",
                newName: "Phone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Phones",
                newName: "Messages");
        }
    }
}
