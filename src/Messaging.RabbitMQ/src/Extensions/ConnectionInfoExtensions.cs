using Messaging.RabbitMQ;

namespace Messaging.RabbitMQ
{
    public static class ConnectionInfoExtensions
    {
        public static IMQFactory CreateMQFactory(this ConnectionInfo info)
        {
            if (info is null)
                throw new System.ArgumentNullException(nameof(info));

            return new RabbitMQFactory(info);
        }

        public static ConnectionInfo WithUserName(
            this ConnectionInfo info, string userName) =>
            new ConnectionInfo(
                hostName: info.HostName,
                userName: userName,
                password: info.Password,
                port: info.Port
            );

        public static ConnectionInfo WithPassword(
            this ConnectionInfo info, string password) =>
            new ConnectionInfo(
                hostName: info.HostName,
                userName: info.UserName,
                password: password,
                port: info.Port
            );

        public static ConnectionInfo WithHostName(
            this ConnectionInfo info, string hostName) =>
            new ConnectionInfo(
                hostName: hostName,
                userName: info.UserName,
                password: info.Password,
                port: info.Port
            );

        public static ConnectionInfo WithPort(
            this ConnectionInfo info, int port) =>
            new ConnectionInfo(
                hostName: info.HostName,
                userName: info.UserName,
                password: info.Password,
                port: port
            );
    }
}
