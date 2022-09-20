using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            var output = new CustomerModel() 
            { 
                FirstName = "Michael", 
                LastName = "Crippa", 
                EmailAdress = "michaelecrippa@gmail.com", 
                Age = 21, 
                IsAlive = true 
            };

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>()
            {
                new CustomerModel()
                {
                    FirstName = "Nike",
                    LastName = "Arrie",
                    EmailAdress = "nikeArrie@gmail.com",
                    Age = 26,
                    IsAlive = false
                },
                new CustomerModel()
                {
                    FirstName = "Michael",
                    LastName = "Crippa",
                    EmailAdress = "michaelecrippa@gmail.com",
                    Age = 21,
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
            {
                await Task.Delay(690);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
