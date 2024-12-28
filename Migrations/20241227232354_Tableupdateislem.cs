using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webodev.Migrations
{
    /// <inheritdoc />
    public partial class Tableupdateislem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Islemler_IslemId1",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_IslemId1",
                table: "Calisanlar");

            migrationBuilder.DropColumn(
                name: "IslemId1",
                table: "Calisanlar");

            migrationBuilder.AlterColumn<int>(
                name: "IslemId",
                table: "Calisanlar",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Calisanlar_IslemId",
                table: "Calisanlar",
                column: "IslemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calisanlar_Islemler_IslemId",
                table: "Calisanlar",
                column: "IslemId",
                principalTable: "Islemler",
                principalColumn: "IslemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calisanlar_Islemler_IslemId",
                table: "Calisanlar");

            migrationBuilder.DropIndex(
                name: "IX_Calisanlar_IslemId",
                table: "Calisanlar");

            migrationBuilder.AlterColumn<string>(
                name: "IslemId",
                table: "Calisanlar",
                type: "nvarchar(max)",
                nullable: false,
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
        }
    }
}
