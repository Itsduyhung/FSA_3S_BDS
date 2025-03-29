using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_user_createdBy",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_contract_user_createdBy",
                table: "contract");

            migrationBuilder.DropForeignKey(
                name: "FK_realestate_user_createdBy",
                table: "realestate");

            migrationBuilder.AlterColumn<int>(
                name: "createdBy",
                table: "realestate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "createdBy",
                table: "contract",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "createdBy",
                table: "Audit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_user_createdBy",
                table: "Audit",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_contract_user_createdBy",
                table: "contract",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_realestate_user_createdBy",
                table: "realestate",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_user_createdBy",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_contract_user_createdBy",
                table: "contract");

            migrationBuilder.DropForeignKey(
                name: "FK_realestate_user_createdBy",
                table: "realestate");

            migrationBuilder.AlterColumn<int>(
                name: "createdBy",
                table: "realestate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "createdBy",
                table: "contract",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "createdBy",
                table: "Audit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_user_createdBy",
                table: "Audit",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_contract_user_createdBy",
                table: "contract",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_realestate_user_createdBy",
                table: "realestate",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
