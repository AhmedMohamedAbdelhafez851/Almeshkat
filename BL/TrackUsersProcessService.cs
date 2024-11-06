using BL.Data;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TrackUsersProcessService
    {
        private readonly ApplicationDbContext _context;

        public TrackUsersProcessService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrackUsersProcess>> GetAllProcessesAsync()
        {
            return await _context.TrackUsersProcesses.ToListAsync();
        }

        public async Task LogUserProcessAsync(string userId, string processName, string tableName, int rowNumber, string deviceIp)
        {
            var processLog = new TrackUsersProcess
            {
                UserId = userId,
                ProcessName = processName,
                TableName = tableName,
                RowNumber = rowNumber,
                DeviceIp = deviceIp,
                ProcessDate = DateTime.UtcNow.Date,
                ProcessTime = DateTime.UtcNow.TimeOfDay
            };

            _context.TrackUsersProcesses.Add(processLog);
            await _context.SaveChangesAsync();
        }

        public async Task<TrackUsersProcess> GetProcessByIdAsync(int id)
        {
            var process = await _context.TrackUsersProcesses.FindAsync(id);
            if (process == null)
            {
                throw new KeyNotFoundException($"Process with ID {id} not found.");
            }
            return process;
        }


        // New method to delete a process by its ID
        public async Task<bool> DeleteProcessByIdAsync(int id)
        {
            var process = await _context.TrackUsersProcesses.FindAsync(id);
            if (process == null)
            {
                return false; // Process not found
            }

            _context.TrackUsersProcesses.Remove(process);
            await _context.SaveChangesAsync();
            return true; // Successfully deleted
        }
    }
}
