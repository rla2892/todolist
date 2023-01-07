using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TodoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // NOTE : .net core 3.0 이상에서는 http 2.0 이 default
                    // NOTE : http2 사용 시, request size 감소(크롬 개발자 툴에서 확인)
                    // NOTE : http2 사용 시, protocol 이 h2 임 (http/1.1 이 아님)
                    // NOTE : (IIS) kestrel 의 경우 아래 옵션으로 http2.0 을 enable/disable 할 수 있으나, IIS 는 이를 설정할 수 없음. client/server 가 둘 다 http 2.0 사용 가능하면 자동으로 2.0 사용 그외에는 1.1 사용
                    webBuilder.ConfigureKestrel((options) =>
                    {
                        // trying to use Http1AndHttp2 causes http2 connections to fail with invalid protocol error
                        // according to Microsoft dual http version mode not supported in unencrypted scenario: https://learn.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0
                        options.ConfigureEndpointDefaults(lo => lo.Protocols = HttpProtocols.Http2);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
