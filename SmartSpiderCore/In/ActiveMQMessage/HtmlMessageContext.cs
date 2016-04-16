using System;

namespace SmartSpiderCore.In.ActiveMQMessage
{
    public class HtmlMessageContext : MessageContext
    {
        /// <summary>
        /// Html文本内容
        /// </summary>
        public string Text { get; set; }

        public HtmlMessageContext()
        {
            Text = String.Empty;
        }
    }
}
