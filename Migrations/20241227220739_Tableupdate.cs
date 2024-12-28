using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webodev.Migrations
{
    /// <inheritdoc />
    public partial class Tableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_Salonlar_SalonId",
                table: "Islemler");

            migrationBuilder.RenameColumn(
                name: "Uzmanlik",
                table: "Calisanlar",
                newName: "IslemId");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Islemler",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IslemId1",
                table: "Calisanlar",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_IslemId1",
                table: "Calisanlar",
                column: "IslemId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Islemler_IslemId1",
                table: "Calisanlar",
                column: "IslemId1",
                principalTable: "Islemler",
                principalColumn: "IslemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_Salonlar_SalonId",
                table: "Islemler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Islemler_IslemId1",
                table: "Calisanlar");

            migrationBuilder.DropForeignKey(
                name: "FK_Islemler_Salonlar_SalonId",
                table: "Islemler");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_IslemId1",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "IslemId1",
                table: "Calisanlar");

            migrationBuilder.RenameColumn(
                name: "IslemId",
                table: "Calisanlar",
                newName: "Uzmanlik");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Islemler",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Islemler_Salonlar_SalonId",
                table: "Islemler",
                column: "SalonId",
                principalTable: "Salonlar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
