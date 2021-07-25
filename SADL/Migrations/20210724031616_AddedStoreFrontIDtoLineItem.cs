using Microsoft.EntityFrameworkCore.Migrations;

namespace SADL.Migrations
{
    public partial class AddedStoreFrontIDtoLineItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Stores_StoreFrontID",
                table: "LineItems");

            migrationBuilder.AlterColumn<int>(
                name: "StoreFrontID",
                table: "LineItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Stores_StoreFrontID",
                table: "LineItems",
                column: "StoreFrontID",
                principalTable: "Stores",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LineItems_Stores_StoreFrontID",
                table: "LineItems");

            migrationBuilder.AlterColumn<int>(
                name: "StoreFrontID",
                table: "LineItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LineItems_Stores_StoreFrontID",
                table: "LineItems",
                column: "StoreFrontID",
                principalTable: "Stores",
                principalColumn: "StoreFrontID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
