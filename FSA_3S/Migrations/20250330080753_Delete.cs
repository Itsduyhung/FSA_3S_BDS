using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class Delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntity_user_userId1",
                table: "WorkEntity");

            migrationBuilder.DropIndex(
                name: "IX_WorkEntity_userId1",
                table: "WorkEntity");

            migrationBuilder.DropColumn(
                name: "userId1",
                table: "WorkEntity");

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntity_userId",
                table: "WorkEntity",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntity_user_userId",
                table: "WorkEntity",
                column: "userId",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntity_user_userId",
                table: "WorkEntity");

            migrationBuilder.DropIndex(
                name: "IX_WorkEntity_userId",
                table: "WorkEntity");

            migrationBuilder.AddColumn<int>(
                name: "userId1",
                table: "WorkEntity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntity_userId1",
                table: "WorkEntity",
                column: "userId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntity_user_userId1",
                table: "WorkEntity",
                column: "userId1",
                principalTable: "user",
                principalColumn: "userId");
        }
    }
}
