using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace school_hub.Migrations
{
    /// <inheritdoc />
    public partial class addTextFaildToAnswere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Answers");
        }
    }
}
