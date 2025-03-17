using FSA_3S.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

    // API GET: Lấy tất cả dữ liệu từ bảng report
    [HttpGet]
    public async Task<ActionResult<List<ReportEntity>>> GetAllReports()
    {
        var reports = await _reportService.GetAllReportsAsync();
        if (reports == null || reports.Count == 0)
        {
            return NotFound("No reports found.");
        }
        return Ok(reports);
    }
}

    // API GET: Lấy dữ liệu theo ID
//    [HttpGet("{id}")]
//    public async Task<ActionResult<ReportEntity>> GetReportById(int id)
//    {
//        var report = await _reportService.GetReportByIdAsync(id);
//        if (report == null)
//        {
//            return NotFound($"No report found with ID = {id}");
//        }
//        return Ok(report);
//    }
//}