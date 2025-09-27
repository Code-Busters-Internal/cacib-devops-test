using Grpc.Net.Client;
using client;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:8082");
var client = new Greeter.GreeterClient(channel);
string? me;
do
{
    Console.WriteLine("Who are you ?");
    me = Console.ReadLine();
} while (string.IsNullOrWhiteSpace(me));

var reply = await client.SayHelloAsync(
    new HelloRequest { Name = me });
Console.WriteLine("From server: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();