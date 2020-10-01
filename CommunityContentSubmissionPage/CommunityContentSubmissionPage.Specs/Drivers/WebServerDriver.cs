﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace CommunityContentSubmissionPage.Specs.Drivers
{
    public class WebServerDriver
    {
        private IHost _host;
        public string Hostname { get; private set; }

        public void Start()
        {
            Hostname = $"http://localhost:{GeneratePort()}";

            string location = typeof(KestrelHostBuilder).Assembly.Location;
            string applicationAssemblyPath = Path.GetDirectoryName(location);
            string webRoot = Path.Combine(applicationAssemblyPath, "..", "..", "..", "..", "CommunityContentSubmissionPage", "wwwroot");

            var hostBuilder = new KestrelHostBuilder();
            var builder = hostBuilder.CreateHostBuilder(new string[] { }, Hostname, webRoot);
            _host = builder.Build();
            _host.StartAsync().ConfigureAwait(false);
        }


        public async Task Stop()
        {
            if (_host != null) await _host.StopAsync();
        }

        private int GeneratePort()
        {
            return new Random().Next(5000, 32000);
        }
    }
}
