using FSA_3S.Models.Requests;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController(IContractService contractService) : ControllerBase
    {
        private readonly IContractService _contractService = contractService;

        [HttpPost("Created_Contract")]
        public async Task<IActionResult> CreateContract([FromBody] ContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    Message = "Invalid request data",
                    Errors = errors
                });
            }

            try
            {
                var result = await _contractService.CreateContractAsync(request);
                return Ok(new
                {
                    Message = "Contract created successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = "Failed to create contract",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("Change_Contract_Status_expried")]
        public async Task<IActionResult> UpdateContractStatus()
        {
            await _contractService.UpdateContractStatusAsync();
            return Ok("Contract status updated successfully.");
        }

        [HttpGet("Get_Contract")]
        public async Task<IActionResult> GetAllContract()
        {
            var result = await _contractService.GetAllContractsAsync();
            return Ok(result);
        }

        [HttpDelete("{contractId}")]
        public async Task<IActionResult> DeleteContract(int contractId)
        {
            var isDeleted = await _contractService.DeleteContractAsync(contractId);
            if (!isDeleted)
            {
                return NotFound(new { message = "Contract not found" });
            }

            return Ok(new { message = "Contract deleted successfully" });
        }
    }
}