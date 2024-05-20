using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoelessJoe.DataAccess.Migrations
{
    public partial class ChangedRelationshipAndShoeAndImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoeImages_Shoes_ShoeId",
                table: "ShoeImages");

            migrationBuilder.DropIndex(
                name: "IX_ShoeImages_ShoeId",
                table: "ShoeImages");

            migrationBuilder.DropColumn(
                name: "ShoeId",
                table: "ShoeImages");

            migrationBuilder.AddColumn<int>(
                name: "ShoeImageId",
                table: "Shoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_ShoeImageId",
                table: "Shoes",
                column: "ShoeImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shoes_ShoeImages_ShoeImageId",
                table: "Shoes",
                column: "ShoeImageId",
                principalTable: "ShoeImages",
                principalColumn: "ShoeImageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shoes_ShoeImages_ShoeImageId",
                table: "Shoes");

            migrationBuilder.DropIndex(
                name: "IX_Shoes_ShoeImageId",
                table: "Shoes");

            migrationBuilder.DropColumn(
                name: "ShoeImageId",
                table: "Shoes");

            migrationBuilder.AddColumn<int>(
                name: "ShoeId",
                table: "ShoeImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoeImages_ShoeId",
                table: "ShoeImages",
                column: "ShoeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeImages_Shoes_ShoeId",
                table: "ShoeImages",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "ShoeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
