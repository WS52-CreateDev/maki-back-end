using Maki.Domain.Customer.Models.Commands;
using Maki.Domain.Customer.Models.Response;

namespace Maki.Domain.Customer.Services;

public interface ICustomerCommandService
{
    Task<int> Handle(RegisterCustomerCommand command);
    Task<CustomerResponse> Handle(LoginCustomerCommand command);
    Task<bool> Handle(UpdateCustomerCommand command);
    Task<bool> Handle(DeleteCustomerCommand command);}