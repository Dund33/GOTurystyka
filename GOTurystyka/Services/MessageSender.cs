using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOTurystyka.Services
{
    public class MessageSender : IMessageService
    {

        Dictionary<int, List<string>> messages;
        public void SendMessage(int id, string message)
        {
            if (!messages.ContainsKey(id))
            {
                messages.Add(id, new List<string> { message }); 
            }
            else
            {
                var success = messages.TryGetValue(id, out var messageList);
                
                if (!success)
                {
                    throw new Exception("Internal consistency error!");
                }

                messageList.Add(message);
            }
        }
    }
}
