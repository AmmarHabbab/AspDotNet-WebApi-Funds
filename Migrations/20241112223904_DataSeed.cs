using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi1.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.", "New York City" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.", "Antwerp" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf.", "Paris" });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[] { 1, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf", "Central Park", 1 });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[] { 2, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf", "Times Square", 1 });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[] { 3, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf", "sadf", 2 });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[] { 4, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf", "Times fghgfdhf", 2 });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[] { 5, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf", "Censadsa sadtral Park", 3 });

            migrationBuilder.InsertData(
                table: "PointOfInterest",
                columns: new[] { "Id", "Description", "Name", "cityId" },
                values: new object[] { 6, "dsaf dsaf dsaf sdafsdaf sad fsadga agdf", "Timxzczxxces Square", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PointOfInterest",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
