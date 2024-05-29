using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.EventsTriggers
{
    public interface IMessage
    {
        string Trigger { get; }
    }


    public interface IMessageHandler
    {
        string Data { get; set; }
        abstract Task ProcessAsync(IServiceProvider provider);
        abstract void Process(IServiceProvider provider);
    }


    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    public class MessageHanderAttribute : Attribute
    {
    }

    public class MessageEventArgs
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public MessageEventArgs(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public MessageEventArgs() { }
    }
}
