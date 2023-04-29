using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ronishbro2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damage_AspNetUsers_VerifiedBy",
                table: "Damage");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_AspNetUsers_CreatedBy",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_UserId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Damage_DamageId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_ApprovedBy",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Car_CarID",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "CarID",
                table: "Request",
                newName: "CarId");

            migrationBuilder.RenameIndex(
                name: "IX_Request_CarID",
                table: "Request",
                newName: "IX_Request_CarId");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Request",
                type: "text",
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Request",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CarID",
                table: "Request",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentInfo",
                table: "Payment",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Offer",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "OfferDescription",
                table: "Offer",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CarStatus",
                table: "Car",
                type: "text",
                nullable: false,
                defaultValue: "Available",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Request_CarID",
                table: "Request",
                column: "CarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Damage_AspNetUsers_VerifiedBy",
                table: "Damage",
                column: "VerifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_AspNetUsers_CreatedBy",
                table: "Offer",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Damage_DamageId",
                table: "Payment",
                column: "DamageId",
                principalTable: "Damage",
                principalColumn: "DamageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_ApprovedBy",
                table: "Request",
                column: "ApprovedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Car_CarID",
                table: "Request",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Car_CarId",
                table: "Request",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "CarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damage_AspNetUsers_VerifiedBy",
                table: "Damage");

            migrationBuilder.DropForeignKey(
                name: "FK_Offer_AspNetUsers_CreatedBy",
                table: "Offer");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_AspNetUsers_UserId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Damage_DamageId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_ApprovedBy",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Car_CarID",
                table: "Request");

            migrationBuilder.DropForeignKey(
                name: "FK_Request_Car_CarId",
                table: "Request");

            migrationBuilder.DropIndex(
                name: "IX_Request_CarID",
                table: "Request");

            migrationBuilder.DropColumn(
                name: "CarID",
                table: "Request");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "Request",
                newName: "CarID");

            migrationBuilder.RenameIndex(
                name: "IX_Request_CarId",
                table: "Request",
                newName: "IX_Request_CarID");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Request",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<int>(
                name: "CarID",
                table: "Request",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentInfo",
                table: "Payment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Offer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OfferDescription",
                table: "Offer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "CarStatus",
                table: "Car",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Available");

            migrationBuilder.AddForeignKey(
                name: "FK_Damage_AspNetUsers_VerifiedBy",
                table: "Damage",
                column: "VerifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_AspNetUsers_CreatedBy",
                table: "Offer",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_AspNetUsers_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Damage_DamageId",
                table: "Payment",
                column: "DamageId",
                principalTable: "Damage",
                principalColumn: "DamageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_ApprovedBy",
                table: "Request",
                column: "ApprovedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_AspNetUsers_UserId",
                table: "Request",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Request_Car_CarID",
                table: "Request",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
