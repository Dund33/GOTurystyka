using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOTurystyka.Services
{
    public interface IMessageService
    {
        public void SendMessage(int id, string message);
    }
}
