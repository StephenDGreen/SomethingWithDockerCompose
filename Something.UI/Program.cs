using Microsoft.Extensions.DependencyInjection;
using Something.UI.Handlers.ArgumentHandlers;
using Something.UI.Services;
using System;
using System.Net.Http.Headers;

namespace Something.UI
{
    class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            // debug - a visual studio bug seems to force doing this (Dec 2020)
            var arguments = new string[args.Length];
            if (args.Length == 0) {
                Array.Resize(ref arguments, arguments.Length + 1);
                arguments[0] = "/d";
            } 
            else
            {
                args.CopyTo(arguments, 0);
            }
            // debug end
            var baseUrl = @"http://something.api";
            var baseUri = new Uri(baseUrl);
            services.AddHttpClient<ISomethingService, SomethingService>(client =>
            {
                client.BaseAddress = baseUri;
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            services.AddHttpClient<ISecurityService, SecurityService>(client =>
            {
                client.BaseAddress = baseUri;
            });
            services.AddSingleton<IHandler, ArgumentAHandler>();
            services.AddSingleton<IHandler, ArgumentDHandler>();
            services.AddSingleton<IHandler, UnexpectedArgumentHandler>();
            var provider = services.BuildServiceProvider();
            var somethingService = provider.GetService<ISomethingService>();
            var securityService = provider.GetService<ISecurityService>();
            try
            {
                securityService.GetHeader();
                somethingService.Run(arguments, securityService.SecurityHeader);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
            }
        }
    }
}
