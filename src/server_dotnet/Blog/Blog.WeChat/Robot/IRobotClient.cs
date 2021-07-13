using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.Robot
{
    public interface IRobotClient
    {
        void SendTextMessage(string text);
    }
}
