namespace Maki.Domain.DesignRequest.Repositories;

using Maki.Domain.DesignRequest.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IDesignRequestRepository
{
    Task<int> AddDesignRequestAsync(DesignRequest designRequest);
    Task<DesignRequest?> GetDesignRequestByIdAsync(int id);
    Task<List<DesignRequest>> GetAllDesignRequestsAsync();
    Task<List<DesignRequest>> GetDesignRequestsByArtisanIdAsync(int artisanId); // Actualizar método
    Task<bool> UpdateAsync(DesignRequest designRequest, int id);
    Task<bool> DeleteAsync(int id);
}