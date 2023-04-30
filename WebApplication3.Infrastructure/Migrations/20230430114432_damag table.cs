using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class damagtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Damage_AspNetUsers_VerifiedBy",
                table: "Damage");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Damage_DamageId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Damage_VerifiedBy",
                table: "Damage");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Damage");

            migrationBuilder.RenameColumn(
                name: "VerifiedBy",
                table: "Damage",
                newName: "description");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Damage",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DamageForm",
                columns: table => new
                {
                    DamageFormId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VerifiedBy = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    DamageId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageForm", x => x.DamageFormId);
                    table.ForeignKey(
                        name: "FK_DamageForm_AspNetUsers_VerifiedBy",
                        column: x => x.VerifiedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DamageForm_Damage_DamageId",
                        column: x => x.DamageId,
                        principalTable: "Damage",
                        principalColumn: "DamageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DamageForm_DamageId",
                table: "DamageForm",
                column: "DamageId");

            migrationBuilder.CreateIndex(
                name: "IX_DamageForm_VerifiedBy",
                table: "DamageForm",
                column: "VerifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_DamageForm_DamageId",
                table: "Payment",
                column: "DamageId",
                principalTable: "DamageForm",
                principalColumn: "DamageFormId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_DamageForm_DamageId",
                table: "Payment");

            migrationBuilder.DropTable(
                name: "DamageForm");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Damage");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Damage",
                newName: "VerifiedBy");

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "Damage",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Damage_VerifiedBy",
                table: "Damage",
                column: "VerifiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Damage_AspNetUsers_VerifiedBy",
                table: "Damage",
                column: "VerifiedBy",
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
        }
    }
}
