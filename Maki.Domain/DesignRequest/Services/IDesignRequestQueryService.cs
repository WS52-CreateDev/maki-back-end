namespace Maki.Domain.DesignRequest.Services;

using Maki.Domain.DesignRequest.Models.Queries;
using Maki.Domain.DesignRequest.Models.Response;
public interface IDesignRequestQueryService
{
    Task<List<DesignRequestResponse>?> Handle(GetAllDesignRequestsQuery query);
    Task<DesignRequestResponse?> Handle(GetDesignRequestByIdQuery query);
    Task<List<DesignRequestResponse>?> Handle(GetDesignRequestsByUserIdQuery query); // Nuevo método
}