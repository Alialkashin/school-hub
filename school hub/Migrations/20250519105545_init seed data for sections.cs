using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace school_hub.Migrations
{
    /// <inheritdoc />
    public partial class initseeddataforsections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "SectionId", "Description", "ImagePath", "Name", "SectionType" },
                values: new object[,]
                {
                    { (short)1, "Mathematics study section covering algebra and geometry.", "/images/sections/math.png", "Mathematics", 0 },
                    { (short)2, "Physics study section focusing on classical mechanics.", "/images/sections/physics.png", "Physics", 0 },
                    { (short)3, "Chemistry study section including organic and inorganic chemistry.", "/images/sections/chemistry.png", "Chemistry", 0 },
                    { (short)4, "Biology study section covering human anatomy and genetics.", "/images/sections/biology.png", "Biology", 0 },
                    { (short)5, "Computer Science study section focusing on programming and algorithms.", "/images/sections/cs.png", "Computer Science", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "SectionId",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "SectionId",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "SectionId",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "SectionId",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "Sections",
                keyColumn: "SectionId",
                keyValue: (short)5);
        }
    }
}
