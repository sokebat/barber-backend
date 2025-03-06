using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberApp.API.Migrations
{
    /// <inheritdoc />
    public partial class changedb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Appointment",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Appointment");
        }
    }
}
