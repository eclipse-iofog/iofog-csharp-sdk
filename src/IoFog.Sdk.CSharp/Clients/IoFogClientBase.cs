using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace IoFog.Sdk.CSharp.Clients
{
    public abstract class IoFogClientBase
    {
        protected readonly string Scheme;
        protected readonly string Host;
        protected readonly int Port;

        protected static readonly string ContainerId;
        protected static readonly bool Ssl;

        static IoFogClientBase()
        {
            Ssl = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ssl"));
            ContainerId = Environment.GetEnvironmentVariable("selfname");
        }

        protected IoFogClientBase(string scheme = null, string host = null, int? port = null)
        {
            Scheme = scheme + (Ssl ? "s" : string.Empty);

            if (!string.IsNullOrEmpty(host))
            {
                Host = host;
            }
            else
            {
                Host = "iofog";

                var isHostReachableTask = IsHostReachable(Host);
                isHostReachableTask.Wait();

                if (!isHostReachableTask.Result)
                {
                    Host = "127.0.0.1";
                }
            }

            Port = port ?? 54321;
        }

        protected Uri BuildUri(string relativePath = null)
        {
            var builder = new UriBuilder(Scheme, Host, Port, relativePath);
            return builder.Uri;
        }

        protected async Task<bool> IsHostReachable(string host)
        {
            using (Ping pinger = new Ping())
            {
                bool pingable = false;
                try
                {
                    PingReply reply = await pinger.SendPingAsync(host, 1000).ConfigureAwait(false);
                    pingable = reply.Status == IPStatus.Success;
                }
                catch (PingException)
                {
                    pingable = false;
                }

                return pingable;
            }
        }
    }
}
