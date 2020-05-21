using System;

namespace Genet.PDM.Workflows.RabbitMQ
{
    public class ConnectionInfo
    {
        public ConnectionInfo(string hostName, string userName, string password, int port)
        {
            HostName = hostName ?? throw new ArgumentNullException(nameof(hostName));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Port = port;
        }

        public string HostName { get; }

        public string UserName { get; }

        public string Password { get; }

        public int Port { get; }
    }
}

