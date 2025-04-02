using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using FSA_3S.Data;
using FSA_3S.Enum;
using FSA_3S.Models;

namespace FSA_3S.Controllers
{
    [Route("api/report")]
    [ApiController]
    public class ReportController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // Đếm số bất động sản chưa bán
        [HttpGet("realestate/count/available")]
        public async Task<IActionResult> GetAvailableCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.Available);
            return Ok(new { status = "Available", count });
        }

        // Đếm số bất động sản đã bán
        [HttpGet("realestate/count/sold")]
        public async Task<IActionResult> GetSoldCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.Sold);
            return Ok(new { status = "Sold", count });
        }

        // Đếm số bất động sản chưa cho thuê
        [HttpGet("realestate/count/for-rent")]
        public async Task<IActionResult> GetForRentCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.ForRent);
            return Ok(new { status = "For Rent", count });
        }

        // Đếm số bất động sản đã cho thuê
        [HttpGet("realestate/count/rented")]
        public async Task<IActionResult> GetRentedCount()
        {
            var count = await _context.RealEstates.CountAsync(r => r.RealEstateStatus == RealEstateStatusEnum.Rented);
            return Ok(new { status = "Rented", count });
        }

        // Đếm số hợp đồng đang thực hiện
        [HttpGet("contract/count/active")]
        public async Task<IActionResult> GetActiveContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Active);
            return Ok(new { status = "Active", count });
        }

        // Đếm số hợp đồng đã hoàn tất
        [HttpGet("contract/count/completed")]
        public async Task<IActionResult> GetCompletedContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Completed);
            return Ok(new { status = "Completed", count });
        }

        // Đếm số hợp đồng đã hủy
        [HttpGet("contract/count/canceled")]
        public async Task<IActionResult> GetCanceledContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Canceled);
            return Ok(new { status = "Canceled", count });
        }

        // Đếm số hợp đồng đã hết hạn
        [HttpGet("contract/count/expired")]
        public async Task<IActionResult> GetExpiredContractsCount()
        {
            var count = await _context.Contracts.CountAsync(c => c.ContractStatus == ContractStatusEnum.Expired);
            return Ok(new { status = "Expired", count });
        }

        // Đếm số cuộc hẹn đã hoàn thành
        [HttpGet("appointment/count/finished")]
        public async Task<IActionResult> GetFinishedAppointmentsCount()
        {
            var count = await _context.Appointments.CountAsync(a => a.Status == "finish");
            return Ok(new { status = "Finish", count });
        }

        // Đếm số cuộc hẹn chưa hoàn thành
        [HttpGet("appointment/count/not-finish")]
        public async Task<IActionResult> GetNotFinishedAppointmentsCount()
        {
            var count = await _context.Appointments.CountAsync(a => a.Status == "not finish");
            return Ok(new { status = "Not Finish", count });
        }
    }
}