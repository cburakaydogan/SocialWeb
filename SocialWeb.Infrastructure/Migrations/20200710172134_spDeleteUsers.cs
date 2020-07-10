using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialWeb.Infrastructure.Migrations
{
    public partial class spDeleteUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC spDeleteUsers 
                                    @Id int
                                    AS 
                                    BEGIN
                                        DELETE FROM Follows
                                            WHERE  FollowerId = @Id OR  FollowingId = @Id
									    DELETE FROM Likes 
									        WHERE  AppUserId = @Id
									    DELETE FROM Mentions 
									        WHERE  AppUserId = @Id
									    DELETE FROM Shares 
									        WHERE  AppUserId = @Id
				                        DELETE FROM AspNetUsers  
                                            WHERE  Id = @Id
                                    END");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tweets",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 928, DateTimeKind.Local).AddTicks(6184),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 260, DateTimeKind.Local).AddTicks(5458));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Shares",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 959, DateTimeKind.Local).AddTicks(9828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 289, DateTimeKind.Local).AddTicks(7976));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Mentions",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 935, DateTimeKind.Local).AddTicks(8098),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 267, DateTimeKind.Local).AddTicks(7801));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Likes",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 958, DateTimeKind.Local).AddTicks(5259),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 288, DateTimeKind.Local).AddTicks(5686));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Follows",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 957, DateTimeKind.Local).AddTicks(1387),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 287, DateTimeKind.Local).AddTicks(1866));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 954, DateTimeKind.Local).AddTicks(2612),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 284, DateTimeKind.Local).AddTicks(4926));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Tweets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 260, DateTimeKind.Local).AddTicks(5458),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 928, DateTimeKind.Local).AddTicks(6184));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Shares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 289, DateTimeKind.Local).AddTicks(7976),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 959, DateTimeKind.Local).AddTicks(9828));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Mentions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 267, DateTimeKind.Local).AddTicks(7801),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 935, DateTimeKind.Local).AddTicks(8098));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Likes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 288, DateTimeKind.Local).AddTicks(5686),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 958, DateTimeKind.Local).AddTicks(5259));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Follows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 287, DateTimeKind.Local).AddTicks(1866),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 957, DateTimeKind.Local).AddTicks(1387));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 10, 20, 20, 45, 284, DateTimeKind.Local).AddTicks(4926),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 7, 10, 20, 21, 33, 954, DateTimeKind.Local).AddTicks(2612));
        }
    }
}
