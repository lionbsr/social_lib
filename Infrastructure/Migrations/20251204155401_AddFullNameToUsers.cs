using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_FollowedId",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_FollowerId",
                table: "follows");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "lists",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "contents",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_FollowedId",
                table: "follows",
                column: "FollowedId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_FollowerId",
                table: "follows",
                column: "FollowerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_FollowedId",
                table: "follows");

            migrationBuilder.DropForeignKey(
                name: "FK_follows_users_FollowerId",
                table: "follows");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "lists");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "contents");

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_FollowedId",
                table: "follows",
                column: "FollowedId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_follows_users_FollowerId",
                table: "follows",
                column: "FollowerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
