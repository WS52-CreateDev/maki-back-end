using Maki.Domain.Customer.Models.Queries;
using Maki.Domain.Customer.Models.Response;
using Maki.Domain.Product.Models.Queries;

namespace Maki.Domain.Customer.Services;

public interface ICustomerQueryService
{
    Task<List<CustomerResponse>?> Handle(GetAllCustomersQuery query);
    Task<CustomerResponse?> Handle(GetCustomerByEmailAndPasswordQuery query);
    Task<CustomerResponse?> Handle(GetCustomerByIdQuery query);
    

}