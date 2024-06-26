namespace Maki.Domain.DesignRequest.Services;

using Maki.Domain.DesignRequest.Models.Commands;
public interface IDesignRequestCommandService
{
    Task<int> Handle(CreateDesignRequestCommand command);
    Task<bool> Handle(UpdateDesignRequestCommand command);
    Task<bool> Handle(DeleteDesignRequestCommand command);
}