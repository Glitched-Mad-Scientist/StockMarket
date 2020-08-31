using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockMarket.AccountAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyCode = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 25, nullable: false),
                    CEO = table.Column<string>(maxLength: 30, nullable: true),
                    BoardofDirectors = table.Column<string>(maxLength: 100, nullable: true),
                    Turnover = table.Column<long>(nullable: false),
                    StockExchanges = table.Column<string>(maxLength: 30, nullable: true),
                    StockCodes = table.Column<string>(maxLength: 30, nullable: true),
                    Sector = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyCode);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 30, nullable: false),
                    Role = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 1000, nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: true),
                    Mobile = table.Column<string>(maxLength: 30, nullable: true),
                    Confirmed = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "IPO",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 30, nullable: false),
                    CompanyCode = table.Column<int>(nullable: false),
                    StockExchange = table.Column<string>(maxLength: 30, nullable: false),
                    OpenDate = table.Column<DateTime>(type: "Date", nullable: false),
                    PricePerShare = table.Column<int>(nullable: false),
                    TotalNumberOfShares = table.Column<int>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPO", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IPO_Company_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "Company",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPrice",
                columns: table => new
                {
                    RowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyCode = table.Column<int>(nullable: false),
                    StockExchange = table.Column<string>(maxLength: 30, nullable: false),
                    CurrentPrice = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPrice", x => x.RowId);
                    table.ForeignKey(
                        name: "FK_StockPrice_Company_CompanyCode",
                        column: x => x.CompanyCode,
                        principalTable: "Company",
                        principalColumn: "CompanyCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IPO_CompanyCode",
                table: "IPO",
                column: "CompanyCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockPrice_CompanyCode",
                table: "StockPrice",
                column: "CompanyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPO");

            migrationBuilder.DropTable(
                name: "StockPrice");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
