using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;

namespace AopDemo.Reflection
{
    class HelperClass
    {
    }

    public class AOPTestAttribute : ContextAttribute, IContributeObjectSink
    {
        public AOPTestAttribute()
            : base("AOPTest")
        { }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new MessageSink(nextSink);
        }

    }
    public class MessageSink : IMessageSink
    {
        public MessageSink(IMessageSink messagesink)
        {
            nextsink = messagesink;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg,IMessageSink replySink)
        {
            return null;
        }

        private IMessageSink nextsink = null;
        public IMessageSink NextSink
        {
            get { return nextsink; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            Console.WriteLine("Before:do something");
            IMessage returnMsg = nextsink.SyncProcessMessage(msg);
            Console.WriteLine("After:do something");
            return returnMsg;
        }


    }
}
