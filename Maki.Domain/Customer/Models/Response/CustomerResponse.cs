﻿namespace Maki.Domain.Customer.Models.Response;

public class CustomerResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Photo { get; set; }
    public int Age { get; set; }
    public string Province { get; set; }
    public string Info { get; set; }
}