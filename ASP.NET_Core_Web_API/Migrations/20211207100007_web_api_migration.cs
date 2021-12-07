using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core_Web_API.Migrations
{
    public partial class web_api_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_School_SchoolID",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_School_SchoolID",
                table: "Person",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "SchoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_School_SchoolID",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_School_SchoolID",
                table: "Person",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "SchoolID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
