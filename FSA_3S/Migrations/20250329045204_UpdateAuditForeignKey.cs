using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditForeignKey : Migration
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_contract_contractId",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_realestate_realEstateId",
                table: "Audit");

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
    }
}
