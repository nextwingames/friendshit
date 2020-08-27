using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class ReceivingLoginPacket:Packet
        {
            public int Result;
            public string Id;
            public string Nickname;
            public int TotalGame;
            public int Gold;
            public int Silver;
            public int Bronze;
        }
    }
}