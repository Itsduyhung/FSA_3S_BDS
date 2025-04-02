using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models; 
using FSA_3S.Models.Entities;
namespace FSA_3S.Repositories.Repository
{
    public class MappingUserAppointmentRepository(AppDbContext context) : IMappingUserAppointmentRepository
    {
        private readonly AppDbContext _context = context;


        public async Task<IEnumerable<MappingUserAppointmentEntity>> GetAllAsync()
        {
            return await _context.MappingUserAppointments
            .Include(m => m.User) // Load thông tin User
            .Include(m => m.Appointment) // Load thông tin Appointment
            .ThenInclude(a => a.Customer) // Load luôn Customer nếu có
            .ToListAsync();
        }

        public async Task<MappingUserAppointmentEntity> GetByIdAsync(int id)
        {
            return await _context.MappingUserAppointments.Include(r => r.CreatedBy)
                    .Include(r => r.UpdatedBy)
                    .FirstOrDefaultAsync(r => r.MappingUserAppointmentId == id);

        }
        public async Task<MappingUserAppointmentEntity> GetByIdForPutAsync(int id)
        {
            var entity = await _context.MappingUserAppointments.FindAsync(id);
           
            if (entity != null)
            {
                // Explicit loading cho các navigation properties
                await _context.Entry(entity).Reference(r => r.Creator).LoadAsync();
                await _context.Entry(entity).Reference(r => r.Updater).LoadAsync();
            }
            return entity;
        }
        //public async Task<MappingUserAppointmentEntity> GetAppointmentTitleByIdAsync(int id)
        //{
        //    var entity = await _context.MappingUserAppointments.FindAsync(id);

        //    if (entity == null) return null; // Kiểm tra nếu không tìm thấy entity

        //    var appointmentTitle = await _context.Appointments
        //        .Where(a => a.AppointmentId == entity.AppointmentId)
        //        .Select(a => a.Title)
        //        .FirstOrDefaultAsync();

        //    return appointmentTitle;
        //}

        public async Task<MappingUserAppointmentEntity> AddAsync(MappingUserAppointmentEntity mapping)
        {
            _context.MappingUserAppointments.Add(mapping);
            await _context.SaveChangesAsync();
            return mapping;
        }

        public async Task<MappingUserAppointmentEntity> UpdateAsync(MappingUserAppointmentEntity mapping)
        {
            _context.Entry(mapping).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return mapping;
        }
        public async Task<MappingUserAppointmentEntity> UpdateApprovalStatusAsync(MappingUserAppointmentEntity mapping)
        {
            _context.Entry(mapping).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return mapping;
        }

        public async Task DeleteAsync(int id)
        {
            var mapping = await GetByIdAsync(id);
            if (mapping != null)
            {
                _context.MappingUserAppointments.Remove(mapping);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MappingUserAppointmentEntity>> GetAllApprovalStatusAsync()
        {
            return await _context.MappingUserAppointments
            .Include(m => m.User)
            .Include(m => m.Appointment)
            .ThenInclude(a => a.Customer)
            .ToListAsync();
        }
    }
}