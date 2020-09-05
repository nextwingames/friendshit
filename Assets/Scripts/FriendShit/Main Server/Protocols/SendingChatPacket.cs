using Friendshit.MainServer;
using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class SendingChatPacket:Packet
        {
            public string Nickname;
            public string Message;

            public SendingChatPacket(string nickname, string message)
            {
                Nickname = nickname;
                Message = message;
            }
        }
    }
}