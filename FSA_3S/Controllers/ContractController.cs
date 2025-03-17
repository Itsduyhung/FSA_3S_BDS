//using FSA_3S.Models.Entities;
//using FSA_3S.Models.Requests;
//using FSA_3S.Services.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace FSA_3S.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ContractController(IContractService contractService) : ControllerBase
//    {
//        private readonly IContractService _contractService = contractService;

//        [HttpPost]
//        public async Task<IActionResult> CreateContract([FromBody] ContractRequest request)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var result = await _contractService.CreateContractAsync(request);
//            return Ok(result);
//        }
//    }
//}