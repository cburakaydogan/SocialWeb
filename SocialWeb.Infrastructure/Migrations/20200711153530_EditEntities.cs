using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialWeb.Infrastructure.Migrations
{
    public partial class EditEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tweets",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 141, DateTimeKind.Local).AddTicks(6849));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Shares",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 188, DateTimeKind.Local).AddTicks(1962));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Mentions",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 157, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Likes",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 186, DateTimeKind.Local).AddTicks(8345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Follows",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 185, DateTimeKind.Local).AddTicks(3299));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 181, DateTimeKind.Local).AddTicks(5810));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tweets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 141, DateTimeKind.Local).AddTicks(6849),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Shares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 188, DateTimeKind.Local).AddTicks(1962),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Mentions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 157, DateTimeKind.Local).AddTicks(453),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 186, DateTimeKind.Local).AddTicks(8345),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Follows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 185, DateTimeKind.Local).AddTicks(3299),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 11, 15, 42, 21, 181, DateTimeKind.Local).AddTicks(5810),
                oldClrType: typeof(DateTime));
        }
    }
}
