using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ShellTemplate
{
    readonly struct Package
    {
        public readonly string Name;
        public readonly string Ver;

        public Package(string name, string ver)
        {
            Name = name;
            Ver = ver;
        }
    }

    class Data
    {
        public readonly Package[] Packages = new Package[]
        {
            new Package("Microsoft.AspNetCore.SignalR.Client", "5.0.5"),
            new Package("Microsoft.AspNetCore.Http.Connections.Client", "5.0.5"),
            new Package("Microsoft.AspNetCore.Http.Connections.Common", "5.0.5"),
            new Package("Microsoft.Extensions.Logging.Abstractions", "5.0.0"),
            new Package("Microsoft.Extensions.Options", "5.0.0"),
            new Package("Microsoft.Extensions.DependencyInjection.Abstractions", "5.0.0"),
            new Package("Microsoft.Extensions.Primitives", "5.0.1"),
            new Package("System.Buffers", "4.5.1"),
            new Package("System.Memory", "4.5.4"),
            new Package("System.Runtime.CompilerServices.Unsafe", "5.0.0"),
            new Package("System.Numerics.Vectors", "4.5.0"),
            new Package("System.Text.Json", "5.0.2"),
            new Package("System.Text.Encodings.Web", "5.0.1"),
            new Package("System.Threading.Tasks.Extensions", "4.5.4"),
            new Package("Microsoft.AspNetCore.SignalR.Client.Core", "5.0.5"),
            new Package("Microsoft.AspNetCore.SignalR.Common", "5.0.5"),
            new Package("Microsoft.AspNetCore.SignalR.Protocols.Json", "5.0.5"),
            new Package("Microsoft.Extensions.DependencyInjection", "5.0.1"),
            new Package("Microsoft.Extensions.Logging", "5.0.0"),
            new Package("System.Threading.Channels", "5.0.0"),
            new Package("Microsoft.AspNetCore.Connections.Abstractions", "5.0.5"),
            new Package("Microsoft.Bcl.AsyncInterfaces", "5.0.0"),
            new Package("Microsoft.AspNetCore.Http.Features", "5.0.5"),
            new Package("System.IO.Pipelines", "5.0.1"),
            new Package("System.Diagnostics.DiagnosticSource", "5.0.1"),
        };
    }

    class Program
    {


        static async Task Main(string[] args)
        {
            var path = "download.sh";
            var file = File.Create(path);
            
            var data = new Data();

            await using var streamWriter = new StreamWriter(file, Encoding.UTF8);
            await streamWriter.WriteLineAsync("#!/bin/bash");
            foreach (var package in data.Packages)
            {
                await streamWriter.WriteLineAsync($"wget https://www.nuget.org/api/v2/package/{package.Name}/{package.Ver} -O {package.Name}.nupkg");
                await streamWriter.WriteLineAsync($"unzip {package.Name}.nupkg -d {package.Name}");
                await streamWriter.WriteLineAsync($"cp {package.Name}/lib/netstandard2.0/{package.Name}.dll dlls/{package.Name}.dll");
            }

            await streamWriter.WriteLineAsync($"cp System.Diagnostics.DiagnosticSource/lib/netstandard1.3/System.Diagnostics.DiagnosticSource.dll dlls/System.Diagnostics.DiagnosticSource.dll");
        }
    }


}
