using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using ServiceMessage = System.ServiceModel.Channels.Message;

namespace VedaConnect
{
    public class MessageInspector : IClientMessageInspector, IEndpointBehavior
    {
        public static event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public object BeforeSendRequest(ref ServiceMessage request, IClientChannel channel)
        {
            return null;
        }

        public void AfterReceiveReply(ref ServiceMessage reply, object correlationState)
        {
            var buffer = reply.CreateBufferedCopy(int.MaxValue);
            reply = buffer.CreateMessage();
            OnMessageReceived(new MessageReceivedEventArgs
            {
                Message = new Message
                {
                    Headers = new MessageHeaders(reply.Headers),
                    Content = reply.ToString()
                }
            });
        }

        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        public void Validate(ServiceEndpoint endpoint) { }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new MessageInspector());
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}