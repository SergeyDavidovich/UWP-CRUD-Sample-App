/// <summary>
/// Этот код является примером использования MVVM Light Toolkit Messaging  и может быть удален из проекта вместе с файлом
/// </summary>
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace CollectionsWorkUWPTemplate10.Messages
{
    public class TestMessage : MessageBase
    {
        public string MessageText { get; set; }
    }
    public class Sender
    {
        public void Send()
        {
            var message = new TestMessage() { MessageText = "Test message from Sender!" };
            Messenger.Default.Send <TestMessage> (message);
        }
    }
    public class Receiver
    {
        public Receiver()
        {
            Messenger.Default.Register<TestMessage>(this,(message)=> HandleMessage(message.MessageText));
        }
        private  void HandleMessage(string par)
        {
            // Do anything there
        }
    }
}
