using AutoMapper;
using Maki.Domain.Customer.Models.Queries;
using Maki.Domain.Customer.Models.Response;
using Maki.Domain.Customer.Repositories;
using Maki.Domain.Customer.Services;
using Maki.Infrastructure.Customer.Persistence;

namespace Maki.Application.Customer.QueryServices;

public class CustomerQueryService:ICustomerQueryService
{ 
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    
    public CustomerQueryService(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<List<CustomerResponse>> Handle(GetAllCustomersQuery query)
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<List<CustomerResponse>>(customers);
    }
    
    public async Task<CustomerResponse> Handle(GetCustomerByEmailAndPasswordQuery query)
    {
        var customer = await _customerRepository.GetByEmailAndPasswordAsync(query.Email, query.Password);
        if (customer == null) throw new KeyNotFoundException("Customer not found");
        return _mapper.Map<CustomerResponse>(customer);
    }

    public async Task<CustomerResponse> Handle(GetCustomerByIdQuery query)
    {
        var customer = await _customerRepository.GetByIdAsync(query.Id);
        if (customer == null) throw new KeyNotFoundException("Customer not found");
        return _mapper.Map<CustomerResponse>(customer);
    }
}