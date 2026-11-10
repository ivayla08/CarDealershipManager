using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarDealershipData.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnersAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarDealershipOwners",
                columns: table => new
                {
                    CarDealershipId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarDealershipOwners", x => new { x.CarDealershipId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_CarDealershipOwners_CarDealerships_CarDealershipId",
                        column: x => x.CarDealershipId,
                        principalTable: "CarDealerships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarDealershipOwners_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CarDealerships",
                columns: new[] { "Id", "Address", "Email", "Name", "PhoneNumber", "isDeleted" },
                values: new object[,]
                {
                    { 1, "Sofia", "sofia@autocenter.bg", "Auto Center Sofia", "02 123 4567", false },
                    { 2, "Plovdiv", "plovdiv@motors.bg", "Plovdiv Motors", "032 555 1234", false },
                    { 3, "Varna", "varna@auto.bg", "Varna Auto", "052 777 8899", false }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "georgiivanov@abv.bg", "Georgi", "Ivanov", "0888 111 222" },
                    { 2, "mariapetrova@abv.bg", "Maria", "Petrova", "0888 111 223" },
                    { 3, "nikolaydimitrov@abv.bg", "Nikolay", "Dimitrov", "0888 111 224" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CarDealershipId", "EngineType", "Model", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", 1, "Hybrid", "Corolla", 2020 },
                    { 2, "BMW", 2, "Diesel", "320d", 2019 },
                    { 3, "Volkswagen", 3, "Petrol", "Golf", 2021 },
                    { 4, "Tesla", 1, "Electric", "Model 3", 2023 }
                });

            migrationBuilder.InsertData(
                table: "CarDealershipOwners",
                columns: new[] { "CarDealershipId", "OwnerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarDealershipOwners_OwnerId",
                table: "CarDealershipOwners",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarDealershipOwners");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CarDealerships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CarDealerships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarDealerships",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
