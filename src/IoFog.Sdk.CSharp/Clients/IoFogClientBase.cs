using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace IoFog.Sdk.CSharp.Clients
{
    public abstract class IoFogClientBase
    {
        protected readonly string Scheme;
        protected readonly string Host;
        protected readonly int Port;

        protected readonly string ContainerId;
        protected readonly bool Ssl;

        private IoFogClientBase()
        {
            Ssl = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ssl"));
            Console.WriteLine($"{DateTime.Now}: " + $"SSL={Ssl}");
            ContainerId = Environment.GetEnvironmentVariable("SELFNAME");
            Console.WriteLine($"{DateTime.Now}: " + $"SELFNAME={ContainerId}");
        }

        protected IoFogClientBase(string scheme = null, string host = null, int? port = null) : this()
        {
            Scheme = scheme + (Ssl ? "s" : string.Empty);

            Host = !string.IsNullOrEmpty(host) ? host : "iofog";

            Port = port ?? 54321;
        }

        protected Uri BuildUri(string relativePath = null)
        {
            var builder = new UriBuilder(Scheme, Host, Port, relativePath);
            return builder.Uri;
        }
    }
}
