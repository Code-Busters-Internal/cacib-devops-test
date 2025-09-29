using System.Net.Http.Json;
using System.Text.Json;
using Grpc.Net.Client;
using client;
using Grpc.Core;

using var httpClient = new HttpClient();
var formData = new Dictionary<string, string>
{
    { "grant_type", "password" },
    { "scope", "openid" },
    { "client_id", "test-client" },
    { "client_secret", "fZHSS0IFVadddd6InJvEl75adngW3Lzz" },
    { "username", "coderz"},
    { "password", "coderzpwd" },
};
var content = new FormUrlEncodedContent(formData);
var authReply = 
    await httpClient.PostAsync("http://localhost:8190/realms/devops-test/protocol/openid-connect/token",  content);
var jOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
};
var info = await authReply.Content.ReadFromJsonAsync<OauthReply>(jOptions);

if (info == null) throw new Exception("Authentication failed");

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:3000");
// using var channel = GrpcChannel.ForAddress("http://localhost:5000");

var m = new Metadata { { "Authorization", $"Bearer {info.AccessToken}" } };
var client = new Greeter.GreeterClient(channel);
string? me;
do
{
    Console.WriteLine("Who are you ?");
    me = Console.ReadLine();
} while (string.IsNullOrWhiteSpace(me));

var reply = await client.SayHelloAsync(
    new HelloRequest { Name = me }, m);
Console.WriteLine("From server: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();



