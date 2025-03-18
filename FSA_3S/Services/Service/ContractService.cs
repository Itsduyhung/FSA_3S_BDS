using FSA_3S.Services.Interface;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Repositories.Interface;
using System.Security.Claims;
using FSA_3S.Models.Respone;
using FSA_3S.Enum;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models;
using FSA_3S.DTOs;

namespace FSA_3S.Services.Service
{
    public class ContractService(IContractRepository contractRepository, IHttpContextAccessor httpContextAccessor,AppDbContext context) : IContractService
    {
        private readonly AppDbContext _context = context;
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        /// <summary>
        /// API Post Contract
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task ValidateContractRequest(ContractRequest request)
        {
            try
            {
                // Kiểm tra dữ liệu trong khối try
                _ = request ?? throw new ArgumentNullException(nameof(request), "Request cannot be null.");

                _ = request.RealEstateId == null
                    ? request.RealEstateId
                    : throw new ArgumentException("RealEstateId is required.");

                _ = request.CustomerId == 0
                    ? request.CustomerId
                    : throw new ArgumentException("CustomerId is required.");

                _ = request.StartDate != null
                    ? request.StartDate
                    : throw new ArgumentException("StartDate cannot be null.");

                _ = request.EndDate != null
                    ? request.EndDate
                    : throw new ArgumentException("EndDate cannot be null.");

                _ = request.StartDate < request.EndDate
                    ? request.StartDate
                    : throw new ArgumentException("StartDate must be earlier than EndDate.");
            }
            catch (Exception ex)
            {
                // ✅ Ném lại lỗi để tầng controller hoặc service xử lý
                throw new Exception($"[ValidateContractRequest] Validation failed: {ex.Message}", ex);
            }

            await Task.CompletedTask;
        }

        public async Task<ContractResponse> CreateContractAsync(ContractRequest request)
        {
            int createdBy;
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out createdBy))
                {
                    throw new UnauthorizedAccessException("Invalid or missing user ID.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[CreateContractAsync] Failed to get user ID: {ex.Message}", ex);
            }

            var contract = new ContractEntity
            {
                RealEstateId = request.RealEstateId,
                CustomerId = request.CustomerId,
                ContractType = request.ContractType,
                ContractStatus = request.ContractStatus,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedBy = createdBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            try
            {
                var result = await _contractRepository.CreateContractAsync(contract);

                var response = new ContractResponse
                {
                    ContractId = result.ContractId,
                    RealEstateId = result.RealEstateId,
                    CustomerId = result.CustomerId,
                    ContractType = result.ContractType,
                    ContractStatus = result.ContractStatus,
                    StartDate = result.StartDate,
                    EndDate = result.EndDate,
                    CreatedBy = result.CreatedBy,
                    UpdatedBy = result.UpdatedBy,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt
                };

                return response;
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết hơn từ InnerException
                throw new Exception($"[CreateContractAsync] Failed to create contract: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }

        /// <summary>
        /// Change Status when Contract expried
        /// </summary>
        /// <returns></returns>
        public async Task UpdateContractStatusAsync()
        {
            var expiredContracts = await _contractRepository.GetExpiredContractsAsync();

            if (expiredContracts.Count != 0)
            {
                foreach (var contract in expiredContracts)
                {
                    contract.ContractStatus = ContractStatusEnum.Inactive;
                }

                await _contractRepository.UpdateContractsAsync(expiredContracts);
            }
        }

        /// <summary>
        /// API Get Contract (All)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ContractDTO>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.GetAllContractAsync();

            var contractDTOs = contracts.Select(c => new ContractDTO
            {
                ContractId = c.ContractId,
                RealEstateId = c.RealEstateId,
                CustomerId = c.CustomerId,
                ContractType = c.ContractType,
                ContractStatus = c.ContractStatus,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                CreatedBy = c.CreatedBy,
                UpdatedBy = c.UpdatedBy,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return contractDTOs;
        }
        /// <summary>
        /// API Delete Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteContractAsync(int contractId)
        {
            var contract = await _contractRepository.GetContractByIdAsync(contractId);
            if (contract == null)
                return false;

            await _contractRepository.DeleteContractAsync(contract);
            return true;
        }
    }
}