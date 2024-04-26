using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cryptos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descentralized = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cash = table.Column<double>(type: "float", nullable: false),
                    Wallet = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CryptoId = table.Column<int>(type: "int", nullable: true),
                    Concept = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Charge = table.Column<double>(type: "float", nullable: false),
                    Payment_Method = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Cryptos_CryptoId",
                        column: x => x.CryptoId,
                        principalTable: "Cryptos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cryptos",
                columns: new[] { "Id", "Descentralized", "Description", "Developer", "Name", "RegisterDate", "Symbol", "Value" },
                values: new object[,]
                {
                    { 1, true, "Bitcoin", "Bitcoin", "Bitcoin", new DateTime(2024, 1, 3, 13, 54, 18, 0, DateTimeKind.Unspecified), "BTC", 40000.0 },
                    { 2, true, "Etherium", "Etherium", "Etherium", new DateTime(2024, 1, 3, 13, 54, 48, 0, DateTimeKind.Unspecified), "ETH", 2000.0 },
                    { 3, true, "Solana", "Solana", "Solana", new DateTime(2024, 1, 3, 13, 55, 6, 0, DateTimeKind.Unspecified), "SOL", 90.0 },
                    { 4, false, "Ripple", "Ripple", "Ripple", new DateTime(2024, 1, 3, 13, 55, 25, 0, DateTimeKind.Unspecified), "XRP", 0.5 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthdate", "Cash", "DNI", "Email", "Name", "Nationality", "Password", "Phone", "Wallet" },
                values: new object[,]
                {
                    { 1, new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 146.0, "123D", "mario@gmail.com", "Mario", "España", "123", "123", 350.0 },
                    { 2, new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "456D", "carlos@gmail.com", "Carlos", "España", "456", "456", 0.0 },
                    { 3, new DateTime(2003, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "789D", "fernando@gmail.com", "Fernando", "España", "789", "789", 0.0 },
                    { 4, new DateTime(2004, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "123456789D", "eduardo@gmail.com", "Eduardo", "España", "123456789", "123456789", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "Charge", "Concept", "CryptoId", "Date", "Payment_Method", "UserId" },
                values: new object[,]
                {
                    { 1, 500.0, 1.0, "Ingreso", null, new DateTime(2024, 1, 3, 17, 52, 7, 0, DateTimeKind.Unspecified), "Google Pay", 1 },
                    { 2, 100.0, 1.0, "Comprar Bitcoin", 1, new DateTime(2024, 1, 3, 17, 54, 21, 0, DateTimeKind.Unspecified), null, 1 },
                    { 3, 100.0, 1.0, "Comprar Etherium", 2, new DateTime(2024, 1, 3, 17, 54, 28, 0, DateTimeKind.Unspecified), null, 1 },
                    { 4, 50.0, 1.0, "Comprar Solana", 3, new DateTime(2024, 1, 3, 17, 54, 33, 0, DateTimeKind.Unspecified), null, 1 },
                    { 5, 100.0, 1.0, "Comprar Bitcoin", 1, new DateTime(2024, 1, 3, 17, 54, 37, 0, DateTimeKind.Unspecified), null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CryptoId",
                table: "Transactions",
                column: "CryptoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cryptos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
