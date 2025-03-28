using FSA_3S.DTOs;
using FSA_3S.Models.Entities;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Contracts;

namespace FSA_3S.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController(IAuditService auditService) : ControllerBase
    {
        private readonly IAuditService _auditService = auditService;

        [HttpGet]
        public async Task<IActionResult> GetAuditInfo()
        {
            var auditInfo = await _auditService.GetAuditInfoAsync();
            return Ok(auditInfo);
        }
    }
}