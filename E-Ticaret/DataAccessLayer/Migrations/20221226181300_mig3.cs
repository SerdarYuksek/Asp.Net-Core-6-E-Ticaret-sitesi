using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_UsersId",
                table: "Sales",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_UsersId",
                table: "Sales",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_UsersId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_UsersId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Sales");
        }
    }
}
