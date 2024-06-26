namespace Maki.Application.DesignRequest.CommandServices;

using AutoMapper;
using Maki.Domain.DesignRequest.Models.Commands;
using Maki.Domain.DesignRequest.Models.Entities;
using Maki.Domain.DesignRequest.Repositories;
using Maki.Domain.DesignRequest.Services;

public class DesignRequestCommandService: IDesignRequestCommandService
{
    private readonly IDesignRequestRepository _designRequestRepository;
    private readonly IMapper _mapper;

    public DesignRequestCommandService(IDesignRequestRepository designRequestRepository, IMapper mapper)
    {
        _designRequestRepository = designRequestRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateDesignRequestCommand command)
    {
        var designRequest = _mapper.Map<CreateDesignRequestCommand, DesignRequest>(command);
        return await _designRequestRepository.AddDesignRequestAsync(designRequest);
    }

    public async Task<bool> Handle(UpdateDesignRequestCommand command)
    {
        var existingDesignRequest = await _designRequestRepository.GetDesignRequestByIdAsync(command.Id);
        if (existingDesignRequest == null) return false;

        var designRequest = _mapper.Map<UpdateDesignRequestCommand, DesignRequest>(command);
        return await _designRequestRepository.UpdateAsync(designRequest, command.Id);
    }

    public async Task<bool> Handle(DeleteDesignRequestCommand command)
    {
        return await _designRequestRepository.DeleteAsync(command.Id);
    }
}