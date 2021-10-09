using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService1
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public async override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            //await Task.Delay(4000);
            return new HelloReply
            {
                Message = "Hello " + request.Name
            };
        }

        public async override Task<HelloReplyList> SayHelloList(HelloRequest request, ServerCallContext context)
        {
            var list = new HelloReplyList();
            await Task.Delay(100);
            for (int i = 1; i <= request.Count; i++)
            {
                list.List.Add(new HelloReply { Message = $"gRPC test {i} {request.Name}" });
            }

            return list;
        }
    }
}
