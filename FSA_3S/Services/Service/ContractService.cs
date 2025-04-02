using FSA_3S.Enum;
using FSA_3S.Helpers;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.Repositories.Interface;
using FSA_3S.Repositories.Repository;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Services.Service
{
    public class ContractService(
        IContractRepository contractRepository,
        AppDbContext context,
        ICustomerRepository customerRepository,
        IRealEstateRepository realEstateRepository,
        IHttpContextAccessor httpContextAccessor) : IContractService
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly AppDbContext _context = context;
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IRealEstateRepository _realEstateRepository = realEstateRepository;

        /// <summary>
        /// APi Post Contract
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<ContractResponse?> CreateContractAsync(ContractRequest request)
        {
            int createdBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            int buyerId = await GetOrCreateCustomerAsync(request.Buyer);
            int sellerId = await GetOrCreateCustomerAsync(request.Seller);

            var contract = new ContractEntity
            {
                RealEstateId = request.RealEstateId,
                ContractType = request.ContractType,
                ContractStatus = request.ContractStatus,
                StatusPayment = request.StatusPayment,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedBy = createdBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };

            // Đặt vào Transaction để kiểm tra lỗi và đảm bảo lưu được vào DB
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (!await _context.RealEstates.AnyAsync(x => x.RealEstateId == request.RealEstateId))
                    throw new Exception("Invalid RealEstateId");

                // Thêm hợp đồng
                _context.Entry(contract).State = EntityState.Added;

                // Lưu hợp đồng
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"Rows affected (contract): {result}");

                // Tạo bản ghi AuditEntity cho hợp đồng
                var audit = new AuditEntity
                {
                    ContractId = contract.ContractId,
                    EntityType = nameof(ContractEntity),
                    CreatedBy = createdBy,
                    CreatedAt = contract.CreatedAt
                };

                _context.Entry(audit).State = EntityState.Added;

                result = await _context.SaveChangesAsync();
                Console.WriteLine($"Rows affected (audit): {result}");

                // Tạo quan hệ giữa Contract và Customer (Buyer/Seller)
                var mappings = new List<MappingContractCustomerEntity>
            {
                new()
                {
                    ContractId = contract.ContractId,
                    BuyerId = buyerId,
                    SellerId = sellerId,
                }
            };

                await _contractRepository.AddMappingsAsync(mappings);

                // Thêm các điều khoản cho hợp đồng
                var clauseMappings = request.ClauseIds.Select(clauseId => new MappingContractClauseEntity
                {
                    ContractId = contract.ContractId,
                    ClauseId = clauseId
                }).ToList();

                await _contractRepository.AddClauseMappingsAsync(clauseMappings);

                // ✅ Commit Transaction nếu mọi thứ thành công
                await transaction.CommitAsync();

                // Trả về kết quả sau khi tạo thành công
                return new ContractResponse
                {
                    ContractId = contract.ContractId,
                    RealEstateId = contract.RealEstateId,
                    BuyerId = buyerId,
                    SellerId = sellerId,
                    ContractType = contract.ContractType,
                    ContractStatus = contract.ContractStatus,
                    StatusPayment = contract.StatusPayment,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    ClauseIds = request.ClauseIds,
                    CreatedBy = contract.CreatedBy,
                    CreatedAt = contract.CreatedAt
                };
            }
            catch (Exception ex)
            {
                // ❌ Rollback nếu có lỗi
                await transaction.RollbackAsync();
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        private async Task<int> GetOrCreateCustomerAsync(PersonInfo personInfo)
        {
            // Kiểm tra xem khách hàng đã tồn tại chưa
            var existingCustomer = await _customerRepository.GetByIdentityNumberAsync(personInfo.CCCD);
            if (existingCustomer != null)
            {
                return existingCustomer.CustomerId;
            }

            // Nếu chưa tồn tại, tạo mới khách hàng
            var newCustomer = new CustomerEntity
            {
                FullName = personInfo.FullName,
                CCCD = personInfo.CCCD,
                PhoneNumber = personInfo.PhoneNumber,
                Address = personInfo.Address,
                CreatedAt = DateTime.UtcNow
            };

            return await _customerRepository.AddCustomerAsync(newCustomer);
        }

        /// <summary>
        /// API Get All Contract
        /// </summary>
        /// <returns></returns>
        public async Task<List<ContractResponse>> GetAllContractsAsync()
        {
            var contracts = await _contractRepository.GetAllContractsAsync();
            return contracts.Select(c => new ContractResponse
            {
                ContractId = c.ContractId,
                RealEstateId = c.RealEstateId,
                BuyerId = c.MappingContractCustomer.FirstOrDefault(mc => mc.Buyer != null)?.Buyer.CustomerId ?? 0,
                SellerId = c.MappingContractCustomer.FirstOrDefault(mc => mc.Seller != null)?.Seller.CustomerId ?? 0,
                ContractType = c.ContractType,
                ContractStatus = c.ContractStatus,
                StatusPayment = c.StatusPayment,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                ClauseIds = c.ContractClauses.Select(cl => cl.ClauseId).ToList(),
                CreatedBy = c.CreatedBy,
                UpdatedBy = c.UpdatedBy,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();
        }

        /// <summary>
        /// API Put Contract
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<ContractResponse?> UpdateContractAsync(int contractId, ContractRequest request)
        {
            var contract = await _contractRepository.GetContractByIdAsync(contractId);
            if (contract == null)
                return null;

            // Lấy thông tin của người tạo từ context
            int updatedBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            // Cập nhật thông tin Buyer và Seller
            int buyerId = await GetOrCreateCustomerAsync(request.Buyer);
            int sellerId = await GetOrCreateCustomerAsync(request.Seller);

            // Kiểm tra RealEstateId có tồn tại hay không
            var realEstateExists = await _context.RealEstates
                .AnyAsync(re => re.RealEstateId == request.RealEstateId);
            if (!realEstateExists)
                throw new KeyNotFoundException($"RealEstate with ID {request.RealEstateId} not found.");

            // Cập nhật thông tin hợp đồng
            contract.RealEstateId = request.RealEstateId;
            contract.ContractType = request.ContractType;
            contract.ContractStatus = request.ContractStatus;
            contract.StatusPayment = request.StatusPayment;
            contract.StartDate = request.StartDate;
            contract.EndDate = request.EndDate;
            contract.UpdatedBy = updatedBy;
            contract.UpdatedAt = DateTime.UtcNow;

            await _contractRepository.UpdateContractAsync(contract);

            // Xóa dữ liệu cũ trước khi thêm mới
            await _contractRepository.DeleteMappingsByContractIdAsync(contractId);
            await _contractRepository.DeleteClauseMappingsByContractIdAsync(contractId);

            // Cập nhật bản ghi AuditEntity
            var audit = new AuditEntity
            {
                ContractId = contract.ContractId,
                EntityType = nameof(ContractEntity),
                UpdatedBy = contract.UpdatedBy,
                UpdatedAt = contract.UpdatedAt
            };

            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();

            // Tạo mới quan hệ giữa Contract và Customer (Buyer/Seller)
            var mappings = new List<MappingContractCustomerEntity>
    {
        new()
        {
            ContractId = contract.ContractId,
            BuyerId = buyerId,
            SellerId = sellerId,
        }
    };

            await _contractRepository.AddMappingsAsync(mappings);

            var clauseMappings = request.ClauseIds.Select(clauseId => new MappingContractClauseEntity
            {
                ContractId = contract.ContractId,
                ClauseId = clauseId
            }).ToList();

            await _contractRepository.AddClauseMappingsAsync(clauseMappings);

            return new ContractResponse
            {
                ContractId = contract.ContractId,
                RealEstateId = contract.RealEstateId,
                BuyerId = buyerId,
                SellerId = sellerId,
                ContractType = contract.ContractType,
                ContractStatus = contract.ContractStatus,
                StatusPayment = contract.StatusPayment,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                ClauseIds = request.ClauseIds,
                CreatedBy = contract.CreatedBy,
                CreatedAt = contract.CreatedAt,
                UpdatedBy = contract.UpdatedBy,
                UpdatedAt = contract.UpdatedAt
            };
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

            int updatedBy = UserIdHelper.GetUserId(_httpContextAccessor)
                ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            var audit = new AuditEntity
            {
                EntityType = nameof(ContractEntity),
                ContractId = contract.ContractId,
                UpdatedBy = updatedBy,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = true
            };

            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();

            await _contractRepository.DeleteMappingsByContractIdAsync(contractId);

            await _contractRepository.DeleteClauseMappingsByContractIdAsync(contractId);

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            Console.WriteLine("[DEBUG] Contract deleted");

            return true;
        }
        public async Task<List<int>> GetDeletedContractsAsync()
        {
            return await _contractRepository.GetDeletedContractIdsAsync();
        }
    }
}