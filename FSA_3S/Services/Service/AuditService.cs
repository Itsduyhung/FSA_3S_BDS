using FSA_3S.DTOs;
using FSA_3S.Services.Interface;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models.Entities;
using FSA_3S.Models;

namespace FSA_3S.Services.Service
{
    public class AuditService(AppDbContext context) : IAuditService
    {
        private readonly AppDbContext _context = context;

        public async Task<List<AuditDTO>> GetAuditInfoAsync()
        {
            return await _context.Audits
                .Select(a => new AuditDTO
                {
                    RealEstateId = a.RealEstateId,
                    ContractId = a.ContractId,
                    CreatedBy = a.CreatedBy,
                    CreatedAt = a.CreatedAt,
                    UpdatedBy = a.UpdatedBy,
                    UpdatedAt = a.UpdatedAt,
                    EntityType = a.EntityType,
                    ActionType = a.IsDeleted == true ? "Deleted" : (a.UpdatedAt != null ? "Updated" : "Created")
                })
                .ToListAsync();
        }
    }
}