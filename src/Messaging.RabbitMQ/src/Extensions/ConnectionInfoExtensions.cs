using Messaging.RabbitMQ;

namespace Messaging.RabbitMQ
{
    public static class ConnectionInfoExtensions
    {
        public static IChannelFactory CreateChannelFactory(this ConnectionInfo info)
        {
            if (info is null)
                throw new System.ArgumentNullException(nameof(info));

            return new RabbitMQFactory(info);
        }

        public static ConnectionInfo WithUserName(
            this ConnectionInfo info, string userName
        )
        {
            if (info == null)
                throw new System.ArgumentNullException(nameof(info));

            return new ConnectionInfo(
                hostName: info.HostName,
                userName: userName,
                password: info.Password,
                port: info.Port
            );
        }

        public static ConnectionInfo WithPassword(
            this ConnectionInfo info, string password
        )
        {
            if (info == null)
                throw new System.ArgumentNullException(nameof(info));

            return new ConnectionInfo(
                hostName: info.HostName,
                userName: info.UserName,
                password: password,
                port: info.Port
            );
        }

        public static ConnectionInfo WithHostName(
            this ConnectionInfo info, string hostName
        )
        {
            if (info == null)
                throw new System.ArgumentNullException(nameof(info));

            return new ConnectionInfo(
                hostName: hostName,
                userName: info.UserName,
                password: info.Password,
                port: info.Port
            );
        }

        public static ConnectionInfo WithPort(
            this ConnectionInfo info, int port
        )
        {
            if (info == null)
                throw new System.ArgumentNullException(nameof(info));

            return new ConnectionInfo(
                hostName: info.HostName,
                userName: info.UserName,
                password: info.Password,
                port: port
            );
        }
    }
}
