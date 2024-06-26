namespace Maki.Application.DesignRequest.QueryServices;

using AutoMapper;
using Maki.Domain.DesignRequest.Models.Commands;
using Maki.Domain.DesignRequest.Models.Entities;
using Maki.Domain.DesignRequest.Repositories;
using Maki.Domain.DesignRequest.Services;
using Maki.Domain.DesignRequest.Models.Queries;
using Maki.Domain.DesignRequest.Models.Response;

using System.Collections.Generic;
using System.Threading.Tasks;

public class DesignRequestQueryService: IDesignRequestQueryService
{
    private readonly IDesignRequestRepository _designRequestRepository;
    private readonly IMapper _mapper;

    public DesignRequestQueryService(IDesignRequestRepository designRequestRepository, IMapper mapper)
    {
        _designRequestRepository = designRequestRepository;
        _mapper = mapper;
    }

    public async Task<List<DesignRequestResponse>?> Handle(GetAllDesignRequestsQuery query)
    {
        var designRequests = await _designRequestRepository.GetAllDesignRequestsAsync();

        if (designRequests == null) return null;

        var response = new List<DesignRequestResponse>();

        foreach (var designRequest in designRequests)
        {
            response.Add(_mapper.Map<DesignRequest, DesignRequestResponse>(designRequest));
        }

        return response;
    }

    public async Task<DesignRequestResponse?> Handle(GetDesignRequestByIdQuery query)
    {
        var designRequest = await _designRequestRepository.GetDesignRequestByIdAsync(query.Id);

        if (designRequest == null) return null;

        return _mapper.Map<DesignRequest, DesignRequestResponse>(designRequest);
    }
    public async Task<List<DesignRequestResponse>?> Handle(GetDesignRequestsByUserIdQuery query)
    {
        var data = await _designRequestRepository.GetDesignRequestsByUserIdAsync(query.UserId);
        var result = _mapper.Map<List<DesignRequest>, List<DesignRequestResponse>>(data);
        return result;
    }
}