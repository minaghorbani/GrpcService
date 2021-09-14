using Grpc.Net.Client;
using GrpcService1;
using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello gRPC !\n");
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //using var channel = GrpcChannel.ForAddress("https://localhost:5001");

            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "Mina" });

            Console.WriteLine(reply.Message);
        }
    }
}
