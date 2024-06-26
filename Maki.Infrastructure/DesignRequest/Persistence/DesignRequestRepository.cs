namespace Maki.Infrastructure.DesignRequest.Persistence;

using Maki.Domain.DesignRequest.Models.Entities;
using Maki.Domain.DesignRequest.Repositories;
using Maki.Infrastructure.Shared.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maki.Domain.DesignRequest.Repositories;
using Maki.Domain.DesignRequest.Services;
using Maki.Domain.DesignRequest.Models.Queries;
using Maki.Domain.DesignRequest.Models.Response;

public class DesignRequestRepository: IDesignRequestRepository
{
   private readonly MakiContext _makiContext;

       public DesignRequestRepository(MakiContext makiContext)
        {
            _makiContext = makiContext;
        }

          public async Task<int> AddDesignRequestAsync(DesignRequest designRequest)
        {
            var strategy = _makiContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _makiContext.Database.BeginTransactionAsync();
                await _makiContext.DesignRequests.AddAsync(designRequest);
                await _makiContext.SaveChangesAsync();
                await transaction.CommitAsync();
            });
            return designRequest.Id;
        }

        public async Task<DesignRequest?> GetDesignRequestByIdAsync(int id)
        {
            return await _makiContext.DesignRequests
                .Include(dr => dr.Artisan)
                .Where(dr => dr.Id == id && dr.IsActive)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DesignRequest>> GetAllDesignRequestsAsync()
        {
            return await _makiContext.DesignRequests
                .Include(dr => dr.Artisan)
                .Where(dr => dr.IsActive)
                .ToListAsync();
        }

        public async Task<List<DesignRequest>> GetDesignRequestsByArtisanIdAsync(int artisanId)
        {
            return await _makiContext.DesignRequests
                .Include(dr => dr.Artisan)
                .Where(dr => dr.ArtisanId == artisanId && dr.IsActive)
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(DesignRequest designRequest, int id)
        {
            var existingDesignRequest = await _makiContext.DesignRequests
                .Where(dr => dr.Id == id)
                .FirstOrDefaultAsync();

            if (existingDesignRequest == null) return false;

            existingDesignRequest.Name = designRequest.Name;
            existingDesignRequest.Characteristics = designRequest.Characteristics;
            existingDesignRequest.Photo = designRequest.Photo;
            existingDesignRequest.Email = designRequest.Email; 
            existingDesignRequest.ArtisanId = designRequest.ArtisanId;
            existingDesignRequest.UpdatedDate = designRequest.UpdatedDate;

            _makiContext.DesignRequests.Update(existingDesignRequest);
            await _makiContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingDesignRequest = await _makiContext.DesignRequests
                .Where(dr => dr.Id == id)
                .FirstOrDefaultAsync();

            if (existingDesignRequest == null) return false;

            existingDesignRequest.IsActive = false;
            _makiContext.DesignRequests.Update(existingDesignRequest);
            await _makiContext.SaveChangesAsync();
            return true;
        }
    
}