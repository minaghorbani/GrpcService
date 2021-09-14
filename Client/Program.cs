﻿using Grpc.Core;
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

            var headers = new Grpc.Core.Metadata();
            headers.Add("agent", "user");

            var option = new Grpc.Core.CallOptions(headers  ,DateTime.UtcNow.AddSeconds(5));
            try
            {
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "Mina" }, option);

                Console.WriteLine(reply.Message);
            }
            catch (RpcException ex )
            {
                Console.WriteLine(ex.StatusCode);
            }

            
        }
    }
}
