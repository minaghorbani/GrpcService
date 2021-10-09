using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using System;
using System.Threading;
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

            var headers = new Grpc.Core.Metadata();
            headers.Add("agent", "user");

            var option = new Grpc.Core.CallOptions(headers  ,DateTime.UtcNow.AddSeconds(5));

            var source = new CancellationTokenSource();
            var token = source.Token;
            if (1==2)
            {
                source.CancelAfter(1000);
            }
            try
            {
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "Mina" }, option);
                //var reply = await client.SayHelloAsync(new HelloRequest { Name = "Mina" }, headers, DateTime.UtcNow.AddSeconds(5),token);

                Console.WriteLine(reply.Message);

                var reply2 = await client.SayHelloListAsync(new HelloRequest { Name = "Grpc List", Count = 5 });
                foreach (var item in reply2.List)
                {
                    Console.WriteLine(item.Message);
                }
                
            }
            catch (RpcException ex )
            {
                Console.WriteLine(ex.StatusCode);
            }

            
        }
    }
}
