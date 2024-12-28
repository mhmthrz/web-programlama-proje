using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webodev.Migrations
{
    /// <inheritdoc />
    public partial class Tableupdatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_Salonlar_SalonId",
                table: "Islemler");

            migrationBuilder.DropIndex(
                name: "IX_Islemler_SalonId",
                table: "Islemler");

            migrationBuilder.DropColumn(
                name: "SalonId",
                table: "Islemler");

            migrationBuilder.DropColumn(
                name: "Sure",
                table: "Islemler");

            migrationBuilder.AlterColumn<int>(
                name: "Ucret",
                table: "Islemler",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Ucret",
                table: "Islemler",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SalonId",
                table: "Islemler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Sure",
                table: "Islemler",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_Islemler_SalonId",
                table: "Islemler",
                column: "SalonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_Salonlar_SalonId",
                table: "Islemler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id");
        }
    }
}
