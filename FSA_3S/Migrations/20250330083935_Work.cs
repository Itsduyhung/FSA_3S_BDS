using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class Work : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkEntity_user_userId",
                table: "WorkEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkEntity",
                table: "WorkEntity");

            migrationBuilder.RenameTable(
                name: "WorkEntity",
                newName: "work");

            migrationBuilder.RenameIndex(
                name: "IX_WorkEntity_userId",
                table: "work",
                newName: "IX_work_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_work",
                table: "work",
                column: "work_id");

            migrationBuilder.AddForeignKey(
                name: "FK_work_user_userId",
                table: "work",
                column: "userId",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_work_user_userId",
                table: "work");

            migrationBuilder.DropPrimaryKey(
                name: "PK_work",
                table: "work");

            migrationBuilder.RenameTable(
                name: "work",
                newName: "WorkEntity");

            migrationBuilder.RenameIndex(
                name: "IX_work_userId",
                table: "WorkEntity",
                newName: "IX_WorkEntity_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkEntity",
                table: "WorkEntity",
                column: "work_id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkEntity_user_userId",
                table: "WorkEntity",
                column: "userId",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
