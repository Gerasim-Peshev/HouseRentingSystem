using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Migrations
{
    public partial class AddingUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f747de29-c2c2-4de5-a7f7-5b79eaddc5e3", "AQAAAAEAACcQAAAAEAC9/Ogal79mhE6B9WbJKLbEMVvzGBdUqVcxV6tAZ2cYSv/vE6daDV433TxhfLEf3Q==", "7e484441-e1ae-443b-a704-c91eac24d5e8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "359ff3cb-fb71-49be-adfa-6e76e706b7c8", "AQAAAAEAACcQAAAAEHPQ0jJcrYcgcDby/jLTnuTFIUOrp1994euN1Gs5WSqTd3ZHxOy9YA88yoQtj3lh3Q==", "4b51e299-2aa8-41db-9650-0706e465ad4f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0ac37af-dd23-418e-8716-1df03c28986e", "AQAAAAEAACcQAAAAEGEsR7npnFQ60kbuT4BlR/ql6YhqG4ni4cQVY7vnmcPOJOiCYEd30EGeopGt48m85Q==", "a9add337-aa13-4abf-856d-8ec55af49cc2" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d968fd1-d9b7-485a-bb75-e3e1579d7fe8", "AQAAAAEAACcQAAAAEA0ZfXFQrgaSPVHYN3lqROlaYCLXMuru2ArKS87jD16P2QZVK+kZeO5uEKkj2s+VGA==", "900f4441-b6bd-41c7-9d40-7304caeb4da1" });
        }
    }
}
