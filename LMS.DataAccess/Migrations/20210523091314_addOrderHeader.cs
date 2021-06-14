using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.DataAccess.Migrations
{
    public partial class addOrderHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_" +
                      "Booking" +
                      "_Book_BookId",
                table: "Booking");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Booking",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookName",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderHeaderId",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Booking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    BookCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeader", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_OrderHeaderId",
                table: "Booking",
                column: "OrderHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Book_BookId",
                table: "Booking",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_OrderHeader_OrderHeaderId",
                table: "Booking",
                column: "OrderHeaderId",
                principalTable: "OrderHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Book_BookId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_OrderHeader_OrderHeaderId",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_Booking_OrderHeaderId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookName",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "OrderHeaderId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Booking");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "Booking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Book_BookId",
                table: "Booking",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
