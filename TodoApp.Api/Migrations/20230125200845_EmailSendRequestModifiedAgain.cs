using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class EmailSendRequestModifiedAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SendEmailRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "SendEmailRequests");
        }
    }
}
