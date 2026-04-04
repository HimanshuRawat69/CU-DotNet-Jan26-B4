using Microsoft.EntityFrameworkCore;
using System;
using VegabondTravelDestinationAPI.Data;
using VegabondTravelDestinationAPI.Models;

namespace VegabondTravelDestinationAPI.Repostries
{
    public class DestinationRepository:IDestinationRepository
    {
        private readonly VegabondTravelDestinationAPIContext _context;

        public DestinationRepository(VegabondTravelDestinationAPIContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
            => await _context.Destinations.ToListAsync();

        public async Task<Destination> GetByIdAsync(int id)
        {
            var data = await _context.Destinations.FindAsync(id);
            if (data == null)
                throw new Exceptions.DestinationNotFoundException("Destination not found");

            return data;
        }

        public async Task AddAsync(Destination destination)
        {
            await _context.Destinations.AddAsync(destination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _context.Destinations.FindAsync(id);
            if (data == null)
                throw new Exceptions.DestinationNotFoundException("Destination not found");

            _context.Destinations.Remove(data);
            await _context.SaveChangesAsync();
        }
    }
}
