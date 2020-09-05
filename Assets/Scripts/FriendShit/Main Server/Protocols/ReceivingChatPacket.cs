using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class ReceivingChatPacket:Packet
        {
            public string Nickname;
            public string Message;
        }
    }
}