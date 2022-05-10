using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace MonopolyRoomServer
{
    public class Configurations
    {
        public EndPoint GetLobbyServerEndPoint()
        {
            int port = GetPortFor("lobbyPort");
            IPAddress ip = GetIPAddress();
            var lobbyEndPoint = new IPEndPoint(ip, port);
            return lobbyEndPoint;
        }

        public EndPoint GetGameServerLocalEndPoint()
        {
            int port = GetPortFor("gamePort");
            IPAddress ip = GetIPAddress();
            var gameEndPoint = new IPEndPoint(ip, port);
            return gameEndPoint;
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
                throw new ArgumentException(nameof(key), new ConfigurationErrorsException($"Configuration has value '{key}'='{value}'"));
            }
            return value;
        }

        private IPAddress GetIPAddress()
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
