using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRealEstateAuditRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractEntityContractId",
                table: "Audit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Audit_ContractEntityContractId",
                table: "Audit",
                column: "ContractEntityContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_contract_ContractEntityContractId",
                table: "Audit",
                column: "ContractEntityContractId",
                principalTable: "contract",
                principalColumn: "contractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_contract_ContractEntityContractId",
                table: "Audit");

            migrationBuilder.DropIndex(
                name: "IX_Audit_ContractEntityContractId",
                table: "Audit");

            migrationBuilder.DropColumn(
                name: "ContractEntityContractId",
                table: "Audit");
        }
    }
}
