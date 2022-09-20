using Grpc.Net.Client;
using GrpcClient.Protos;


var channel = GrpcChannel.ForAddress("http://localhost:5045");

//var client = new Greeter.GreeterClient(channel);
//var request = new HelloRequest() { Name = "Mike" };
 
var customerClient = new Customer.CustomerClient(channel);

var lookupModel = new CustomerLookupModel() {  UserId = 1 };
var customer = await customerClient.GetCustomerInfoAsync(lookupModel);

Console.WriteLine(customer.FirstName);
Console.WriteLine(customer.LastName);

using (var call = customerClient.GetNewCustomers(new NewCustomerRequest()))
{
    Console.WriteLine("New Customers:");
    while (await call.ResponseStream.MoveNext(default))
    {
        var newCustomer = call.ResponseStream.Current;

        Console.WriteLine(newCustomer.FirstName);
    }
}

Console.ReadLine();
