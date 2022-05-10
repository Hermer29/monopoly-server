using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace MonopolyGameServer
{
    public class Configurations
    {
        public EndPoint GetGameServerGlobalEndpoint()
        {
            int port = GetPortFor("gameExternalPort");
            IPAddress ip = GetIPAddressFor("gameServerGlobalIp");
            return new IPEndPoint(ip, port);
        }

        public EndPoint GetGameServerLocalEndPoint()
        {
            int port = GetPortFor("gameLocalPort");
            IPAddress ip = GetIPAddressFor("gameServerLocalIp");
            return new IPEndPoint(ip, port);
        }

        public string GetConnectionsLoggingFilePath()
        {
            return GetConfiguration("connectionsLogsFilePath");
        }

        public string GetPlayFabTitleId()
        {
            return GetConfiguration("playFabTitleId");
        }

        public string GetPlayFabDeveloperSecretKey()
        {
            return GetConfiguration("playFabDeveloperSecretKey");
        }

        public string GetVersion()
        {
            return GetConfiguration("version");
        }

        private string GetConfiguration(string key)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException(nameof(key));
            }
            return value;
        }

        private IPAddress GetIPAddressFor(string key)
        {
            var hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName, AddressFamily.InterNetwork);
            return ipEntry.AddressList[0];
        }

        private int GetPortFor(string configurationKey)
        {
            var port = GetConfiguration(configurationKey);
            return int.Parse(port);
        }
    }
}
