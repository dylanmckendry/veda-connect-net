using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace VedaConnect
{
    internal static class ClientFactory
    {
        internal static TClient Create<TClient, TPort>(string url, string username, string password)
            where TClient : ClientBase<TPort>
            where TPort : class
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var encoding = new TextMessageEncodingBindingElement { MessageVersion = MessageVersion.Soap11WSAddressing10 };
            var transport = new HttpsTransportBindingElement
            {
                AuthenticationScheme = AuthenticationSchemes.Basic,
                KeepAliveEnabled = true,
                MaxReceivedMessageSize = Int32.MaxValue
            };
            var security = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
            security.IncludeTimestamp = false;

            var binding = new CustomBinding();
            binding.Elements.Add(encoding);
            binding.Elements.Add(security);
            binding.Elements.Add(transport);

            var endpointAddress = new EndpointAddress(url);

            var constructor = typeof(TClient).GetConstructor(new[] { typeof(Binding), typeof(EndpointAddress) });
            var client = (TClient)constructor.Invoke(new object[] { binding, endpointAddress});

            client.Endpoint.Behaviors.Add(new MessageInspector());

            if (client.ClientCredentials == null)
            {
                throw new Exception("Client credentials not available because the web service endpoint hasn't been configured properly.");
            }

            client.ClientCredentials.UserName.UserName = username;
            client.ClientCredentials.UserName.Password = password;

            return client;
        }
    }
}
