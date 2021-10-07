using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class SeedSLATable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServicesLA",
                columns: new[] { "Id", "Description" },
                values: new object[] { 1, "Customer" });

            migrationBuilder.InsertData(
                table: "ServicesLA",
                columns: new[] { "Id", "Description" },
                values: new object[] { 2, "Internal" });

            migrationBuilder.InsertData(
                table: "ServicesLA",
                columns: new[] { "Id", "Description" },
                values: new object[] { 3, "Multilevel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServicesLA",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServicesLA",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServicesLA",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
