using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsCar",
                columns: table => new
                {
                    Car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    license_plate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    number_of_car_seats = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    price_per_day = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCar", x => x.Car_id);
                });

            migrationBuilder.CreateTable(
                name: "MsCustomer",
                columns: table => new
                {
                    Customer_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    driver_license_number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCustomer", x => x.Customer_id);
                });

            migrationBuilder.CreateTable(
                name: "MsEmployee",
                columns: table => new
                {
                    Employee_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsEmployee", x => x.Employee_id);
                });

            migrationBuilder.CreateTable(
                name: "MsCarImages",
                columns: table => new
                {
                    Image_car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    image_link = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCarImages", x => x.Image_car_id);
                    table.ForeignKey(
                        name: "FK_MsCarImages_MsCar_Car_id",
                        column: x => x.Car_id,
                        principalTable: "MsCar",
                        principalColumn: "Car_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrRental",
                columns: table => new
                {
                    Rental_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    rental_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    return_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_status = table.Column<bool>(type: "bit", nullable: false),
                    customer_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrRental", x => x.Rental_id);
                    table.ForeignKey(
                        name: "FK_TrRental_MsCar_car_id",
                        column: x => x.car_id,
                        principalTable: "MsCar",
                        principalColumn: "Car_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrRental_MsCustomer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "MsCustomer",
                        principalColumn: "Customer_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrMaintenance",
                columns: table => new
                {
                    Maintenance_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    maintenance_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    employee_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrMaintenance", x => x.Maintenance_id);
                    table.ForeignKey(
                        name: "FK_TrMaintenance_MsCar_car_id",
                        column: x => x.car_id,
                        principalTable: "MsCar",
                        principalColumn: "Car_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrMaintenance_MsEmployee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "MsEmployee",
                        principalColumn: "Employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LtPayment",
                columns: table => new
                {
                    Payment_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    rental_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LtPayment", x => x.Payment_id);
                    table.ForeignKey(
                        name: "FK_LtPayment_TrRental_rental_id",
                        column: x => x.rental_id,
                        principalTable: "TrRental",
                        principalColumn: "Rental_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LtPayment_rental_id",
                table: "LtPayment",
                column: "rental_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MsCarImages_Car_id",
                table: "MsCarImages",
                column: "Car_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenance_car_id",
                table: "TrMaintenance",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenance_employee_id",
                table: "TrMaintenance",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrRental_car_id",
                table: "TrRental",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrRental_customer_id",
                table: "TrRental",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LtPayment");

            migrationBuilder.DropTable(
                name: "MsCarImages");

            migrationBuilder.DropTable(
                name: "TrMaintenance");

            migrationBuilder.DropTable(
                name: "TrRental");

            migrationBuilder.DropTable(
                name: "MsEmployee");

            migrationBuilder.DropTable(
                name: "MsCar");

            migrationBuilder.DropTable(
                name: "MsCustomer");
        }
    }
}
