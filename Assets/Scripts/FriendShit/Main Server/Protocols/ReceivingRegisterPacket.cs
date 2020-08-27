using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class ReceivingRegisterPacket:Packet
        {
            public int Result;
            public string Id;
        }
    }
}

