using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class attachmenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageForm_AspNetUsers_VerifiedBy",
                table: "DamageForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageForm_Damage_DamageId",
                table: "DamageForm");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Offer",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "DamageForm",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Damage",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfRents",
                table: "Attachment",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "DrivingLicense",
                table: "Attachment",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Citizenship",
                table: "Attachment",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ActivityStatus",
                table: "Attachment",
                type: "text",
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageForm_AspNetUsers_VerifiedBy",
                table: "DamageForm",
                column: "VerifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageForm_Damage_DamageId",
                table: "DamageForm",
                column: "DamageId",
                principalTable: "Damage",
                principalColumn: "DamageId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DamageForm_AspNetUsers_VerifiedBy",
                table: "DamageForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DamageForm_Damage_DamageId",
                table: "DamageForm");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Offer");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "DamageForm",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Damage",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfRents",
                table: "Attachment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "DrivingLicense",
                table: "Attachment",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Citizenship",
                table: "Attachment",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityStatus",
                table: "Attachment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Active");

            migrationBuilder.AddForeignKey(
                name: "FK_DamageForm_AspNetUsers_VerifiedBy",
                table: "DamageForm",
                column: "VerifiedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DamageForm_Damage_DamageId",
                table: "DamageForm",
                column: "DamageId",
                principalTable: "Damage",
                principalColumn: "DamageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
