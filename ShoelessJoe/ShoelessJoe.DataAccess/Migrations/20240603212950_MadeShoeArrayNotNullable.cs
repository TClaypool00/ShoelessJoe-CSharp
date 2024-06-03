using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoelessJoe.DataAccess.Migrations
{
    public partial class MadeShoeArrayNotNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ShoeArray",
                table: "ShoeImages",
                type: "longblob",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ShoeArray",
                table: "ShoeImages",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob");
        }
    }
}
