using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webodev.Migrations
{
    /// <inheritdoc />
    public partial class updateuygun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "uygun",
                table: "Randevular",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "uygun",
                table: "Randevular");
        }
    }
}
