using Microsoft.EntityFrameworkCore.Migrations;

namespace DateApp.API.Migrations
{
    public partial class firebaseid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FireBaseID",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FireBaseID",
                table: "Photos");
        }
    }
}
