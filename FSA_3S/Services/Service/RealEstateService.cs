using CloudinaryDotNet.Actions;
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
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace FSA_3S.Services.Service
{
    public class RealEstateService(IRealEstateRepository _realestaterepository,AppDbContext _context, IHttpContextAccessor _httpContextAccessor, CloudinaryService _cloudinaryService) : IRealEstateService
    {
        /// <summary>
        /// API Post RealEstate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<RealEstateRespone?> CreateAsync(RealEstateRequest request)
        {
            int createdBy = UserIdHelper.GetUserId(_httpContextAccessor)
                             ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            string? imageUrl = null;
            if (request.ImagePath != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(request.ImagePath);
            }

            var realEstate = new RealEstateEntity
            {
                RealEstateName = request.Name,
                RealEstateType = request.RealEstateType,
                RealEstateStatus = RealEstateStatusEnum.Waiting_for_approval,
                Price = request.Price,
                Seller = request.Seller,
                SaleDate = request.SaleDate,
                Coordinate = request.Coordinate,
                ImagePath = imageUrl,
                Address = request.Address,
                Description = request.Description,
                CreatedBy = createdBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };

            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();

            var audit = new AuditEntity
            {
                RealEstateId = realEstate.RealEstateId,
                EntityType = nameof(RealEstateEntity),
                CreatedBy = realEstate.CreatedBy,
                CreatedAt = realEstate.CreatedAt
            };

            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();

            return new RealEstateRespone
            {
                RealEstateId = realEstate.RealEstateId,
                RealEstateName = realEstate.RealEstateName,
                RealEstateType = realEstate.RealEstateType,
                RealEstateStatus = RealEstateStatusEnum.Waiting_for_approval,
                Price = realEstate.Price,
                Seller = realEstate.Seller,
                Coordinate = realEstate.Coordinate,
                SaleDate = realEstate.SaleDate,
                ImagePath = realEstate.ImagePath,
                Address = realEstate.Address,
                Description = realEstate.Description,
                CreatedBy = realEstate.CreatedBy,
                UpdatedBy = realEstate.UpdatedBy,
                CreatedAt = realEstate.CreatedAt,
                UpdatedAt = realEstate.UpdatedAt
            };
        }
        public async Task<RealEstateEntity?> GetByIdAsync(int id)
        {
            return await _realestaterepository.GetByIdAsync(id);
        }

        /// <summary>
        /// API Get for RealEstate (All)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RealEstateRespone>> GetAllRealEstateAsync()
        {
            var result = await _realestaterepository.GetAllAsync();

            return result.Select(r => new RealEstateRespone
            {
                RealEstateId = r.RealEstateId,
                RealEstateName = r.RealEstateName,
                RealEstateType = r.RealEstateType,
                RealEstateStatus = r.RealEstateStatus,
                Price = r.Price,
                Seller = r.Seller,
                Coordinate = r.Coordinate,
                SaleDate = r.SaleDate,
                ImagePath = r.ImagePath,
                Address = r.Address,
                Description = r.Description,
                CreatedBy = r.CreatedBy,
                UpdatedBy = r.UpdatedBy,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
        }
        /// <summary>
        /// API Put RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RealEstateRespone> UpdateRealEstateAsync(int id, RealEstateRequest request)
        {
            var realEstate = await _realestaterepository.GetByIdForPutAsync(id);
            if (realEstate == null) return null;

            var UpdatedBy = UserIdHelper.GetUserId(_httpContextAccessor);
            if (UpdatedBy == null)
            {
                throw new UnauthorizedAccessException("Invalid or missing user ID.");
            }

            realEstate.RealEstateName = request.Name;
            realEstate.RealEstateType = request.RealEstateType;
            realEstate.RealEstateStatus = request.RealEstateStatus;
            realEstate.Price = request.Price;
            realEstate.Seller = request.Seller;
            realEstate.SaleDate = request.SaleDate;
            realEstate.Coordinate = request.Coordinate;
            realEstate.Address = request.Address;
            realEstate.Description = request.Description;
            realEstate.UpdatedBy = UpdatedBy;
            realEstate.UpdatedAt = DateTime.UtcNow;

            if (request.ImagePath != null)
            {
                realEstate.ImagePath = await _cloudinaryService.UploadImageAsync(request.ImagePath);
            }

            _context.Entry(realEstate).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            if (realEstate.RealEstateId > 0)
            {
                var audit = new AuditEntity
                {
                    RealEstateId = realEstate.RealEstateId,
                    EntityType = nameof(RealEstateEntity),
                    UpdatedBy = realEstate.UpdatedBy,
                    UpdatedAt = realEstate.UpdatedAt
                };
                _context.Audits.Add(audit);
                await _context.SaveChangesAsync();
            }

            return new RealEstateRespone
            {
                RealEstateId = realEstate.RealEstateId,
                RealEstateName = realEstate.RealEstateName,
                RealEstateType = realEstate.RealEstateType,
                RealEstateStatus = realEstate.RealEstateStatus,
                Price = realEstate.Price,
                Seller = realEstate.Seller,
                SaleDate = realEstate.SaleDate,
                Coordinate = realEstate.Coordinate,
                Address = realEstate.Address,
                Description = realEstate.Description,
                ImagePath = realEstate.ImagePath,
                CreatedBy = realEstate.CreatedBy,
                UpdatedBy = realEstate.UpdatedBy,
                CreatedAt = realEstate.CreatedAt,
                UpdatedAt = realEstate.UpdatedAt
            };
        }

        /// <summary>
        /// API Delete RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRealEstateAsync(int id)
        {
            var realEstate = await _realestaterepository.GetByIdAsync(id);
            if (realEstate == null) return false;

            var deletedBy = UserIdHelper.GetUserId(_httpContextAccessor)
                            ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            var audit = new AuditEntity
            {
                EntityType = nameof(RealEstateEntity),
                RealEstateId = realEstate.RealEstateId,
                UpdatedBy = deletedBy,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = true
            };

            _context.Audits.Add(audit);
            await _context.SaveChangesAsync();

            var isDeleted = await _realestaterepository.DeleteAsync(id);
            return isDeleted;
        }
    }
}