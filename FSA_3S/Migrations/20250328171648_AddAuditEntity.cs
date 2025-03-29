using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audit_contract_ContractId",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_realestate_RealEstateId",
                table: "Audit");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Audit",
                newName: "updatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Audit",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "RealEstateId",
                table: "Audit",
                newName: "realEstateId");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Audit",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "Audit",
                newName: "entityType");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Audit",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Audit",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Audit",
                newName: "contractId");

            migrationBuilder.RenameColumn(
                name: "AuditId",
                table: "Audit",
                newName: "auditId");

            migrationBuilder.RenameIndex(
                name: "IX_Audit_RealEstateId",
                table: "Audit",
                newName: "IX_Audit_realEstateId");

            migrationBuilder.RenameIndex(
                name: "IX_Audit_ContractId",
                table: "Audit",
                newName: "IX_Audit_contractId");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_createdBy",
                table: "Audit",
                column: "createdBy");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_updatedBy",
                table: "Audit",
                column: "updatedBy");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_user_createdBy",
                table: "Audit",
                column: "createdBy",
                principalTable: "user",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_user_updatedBy",
                table: "Audit",
                column: "updatedBy",
                principalTable: "user",
                principalColumn: "userId");
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

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_user_createdBy",
                table: "Audit");

            migrationBuilder.DropForeignKey(
                name: "FK_Audit_user_updatedBy",
                table: "Audit");

            migrationBuilder.DropIndex(
                name: "IX_Audit_createdBy",
                table: "Audit");

            migrationBuilder.DropIndex(
                name: "IX_Audit_updatedBy",
                table: "Audit");

            migrationBuilder.RenameColumn(
                name: "updatedBy",
                table: "Audit",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Audit",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "realEstateId",
                table: "Audit",
                newName: "RealEstateId");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Audit",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "entityType",
                table: "Audit",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Audit",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Audit",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "contractId",
                table: "Audit",
                newName: "ContractId");

            migrationBuilder.RenameColumn(
                name: "auditId",
                table: "Audit",
                newName: "AuditId");

            migrationBuilder.RenameIndex(
                name: "IX_Audit_realEstateId",
                table: "Audit",
                newName: "IX_Audit_RealEstateId");

            migrationBuilder.RenameIndex(
                name: "IX_Audit_contractId",
                table: "Audit",
                newName: "IX_Audit_ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_contract_ContractId",
                table: "Audit",
                column: "ContractId",
                principalTable: "contract",
                principalColumn: "contractId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Audit_realestate_RealEstateId",
                table: "Audit",
                column: "RealEstateId",
                principalTable: "realestate",
                principalColumn: "realEstateId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
