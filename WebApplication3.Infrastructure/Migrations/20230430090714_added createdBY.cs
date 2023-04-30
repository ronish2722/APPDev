using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedcreatedBY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Car",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CreatedBy",
                table: "Car",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_AspNetUsers_CreatedBy",
                table: "Car",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_AspNetUsers_CreatedBy",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_CreatedBy",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Car");
        }
    }
}
