using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_contract_contractId",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_realestate_realEstateId",
                table: "Audit");

            migrationBuilder.CreateTable(
                name: "WorkEntity",
                columns: table => new
                {
                    work_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    time_of_work = table.Column<DateTime>(type: "datetime2", nullable: false),
                    des_work = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    userId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkEntity", x => x.work_id);
                    table.ForeignKey(
                        name: "FK_WorkEntity_user_userId1",
                        column: x => x.userId1,
                        principalTable: "user",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkEntity_userId1",
                table: "WorkEntity",
                column: "userId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_contract_contractId",
                table: "Audit",
                column: "contractId",
                principalTable: "contract",
                principalColumn: "contractId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_realestate_realEstateId",
                table: "Audit",
                column: "realEstateId",
                principalTable: "realestate",
                principalColumn: "realEstateId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_contract_contractId",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_realestate_realEstateId",
                table: "Audit");

            migrationBuilder.DropTable(
                name: "WorkEntity");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_contract_contractId",
                table: "Audit",
                column: "contractId",
                principalTable: "contract",
                principalColumn: "contractId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_realestate_realEstateId",
                table: "Audit",
                column: "realEstateId",
                principalTable: "realestate",
                principalColumn: "realEstateId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
