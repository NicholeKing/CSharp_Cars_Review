using Microsoft.EntityFrameworkCore.Migrations;

namespace Cars.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Autos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Autos_UserId",
                table: "Autos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Autos_Users_UserId",
                table: "Autos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Autos_Users_UserId",
                table: "Autos");

            migrationBuilder.DropIndex(
                name: "IX_Autos_UserId",
                table: "Autos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Autos");
        }
    }
}
