using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FiledPayment.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardNumber = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    CardHolder = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityCode = table.Column<string>(type: "VARCHAR(3)", nullable: true),
                    Amount = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    PaymentStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_PaymentStates_PaymentStateId",
                        column: x => x.PaymentStateId,
                        principalTable: "PaymentStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "PaymentStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Processed" });

            migrationBuilder.InsertData(
                table: "PaymentStates",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Failed" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentStateId",
                table: "Payments",
                column: "PaymentStateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentStates");
        }
    }
}
