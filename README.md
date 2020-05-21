# Messaging.RabbitMQ

## Description

Provides convenient usage of RabbitMQ Message Broker for .NET

## Usage

```csharp
using Messaging.RabbitMQ;

class Message { public string id; public string content;}

(...)

ConnectionInfo.Default
    .WithHostName("<yourHost>")
    .WithUserName("<yourUser>")
    .WithPassword("<yourPassword>")
    .CreateChannelFactory()
    .CreateChannelWriter<Message>()
    .Write(new Message()
    {
        id = "123"
        content = "Hello World!"
    });

```
