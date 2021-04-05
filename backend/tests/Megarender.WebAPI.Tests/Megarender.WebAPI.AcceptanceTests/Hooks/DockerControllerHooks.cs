using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using BoDi;
using Ductus.FluentDocker.Builders;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace Megarender.WebAPI.AcceptanceTests.Hooks
{
    [Binding]
    public class DockerControllerHooks
    {
        private static ICompositeService CompositeService;
        private IObjectContainer ObjectContainer;

        public DockerControllerHooks(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;
        }

       // [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var config = LoadConfiguration();

            var dockerComposeFileName = config["DockerComposeFileName"];
            var dockerComposeFilePath = GetDockerComposeLocation(dockerComposeFileName);

            var baseAddress = config["BaseAddress"];
            CompositeService = new Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposeFilePath)
                .RemoveOrphans()
                .WaitForHttp("webapi", $"{baseAddress}",
                    continuation: (response, _) => response.Code != HttpStatusCode.OK ? 2000 : 0)
                .Build()
                .Start();
        }
        
     //   [AfterTestRun]
        public static void DockerComposeDown()
        {
            CompositeService.Stop();
            CompositeService.Dispose();
        }

        [BeforeScenario]
        public void AddHttpClient()
        {
            var config = LoadConfiguration();
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(config["BaseAddress"])
            };
            ObjectContainer.RegisterInstanceAs(httpClient);
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private static string GetDockerComposeLocation(string dockerComposeFileName)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory,"*.yml").Any(s=>s.EndsWith(dockerComposeFileName)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }

            return Path.Combine(directory, dockerComposeFileName);
        }
    }
}