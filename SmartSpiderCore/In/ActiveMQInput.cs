using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Newtonsoft.Json;
using SmartSpiderCore.In.ActiveMQMessage;

namespace SmartSpiderCore.In
{
    public class ActiveMQInput : Input
    {
        private IConnection _connection;
        private ISession _session;
        private IDestination _destinationConsumer;
        private IDestination _destinationProducer;
        private IMessageConsumer _messageConsumer;
        private IMessageProducer _messageProducer;
        private const int TimeOut = 2000;

        public string QueueBak = "";
        public string Queue = "";
        public string ConnectionString = "";
        public string Filter = "";

        public override void Init()
        {
            try
            {
                IConnectionFactory factory = new ConnectionFactory(ConnectionString);
                _connection = factory.CreateConnection();
                _connection.Start();
                _session = _connection.CreateSession();

                _destinationConsumer = _session.GetQueue(Queue);
                _messageConsumer = _session.CreateConsumer(_destinationConsumer);

                _destinationProducer = _session.GetQueue(QueueBak);
                _messageProducer = _session.CreateProducer(_destinationProducer);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public override IEnumerator<string> GetEnumerator()
        {
            return new ActiveMQEnumerator();
        }

        public override Content GetContent(string uri)
        {
            var content = new Content();

            try
            {
                while (true)
                {
                    var message = _messageConsumer.Receive(TimeSpan.FromMilliseconds(TimeOut)) as ActiveMQTextMessage;

                    if (message == null)
                    {
                        continue;
                    }

                    //发送到备份队列
                    _messageProducer.Send(message);

                    var htmlMessageContext = JsonConvert.DeserializeObject<HtmlMessageContext>(message.Text);

                    if (Regex.Match(htmlMessageContext.Response.Request.Url, Filter).Success)
                    {
                        content.Session.Add(new NameValue("Request.Url", htmlMessageContext.Response.Request.Url));

                        content.ContentText = htmlMessageContext.Text;

                        break;
                    }
                }

            }
            catch (NMSException nmsException)
            {
                _messageConsumer.Close();
                _messageConsumer.Dispose();
                _messageConsumer = null;

                Thread.Sleep(2000);

                _messageConsumer = _session.CreateConsumer(_destinationConsumer);

                Console.WriteLine(nmsException);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            return content;
        }
    }
}
