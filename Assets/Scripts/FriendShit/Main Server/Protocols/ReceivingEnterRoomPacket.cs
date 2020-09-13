using Friendshit.MainServer;
using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class ReceivingEnterRoomPacket:Packet
        {
            public int Result;
            public int Id;
            public string Name;
            public int Headcount;
            public int MaxPeople;
            public int Map;
            public string[] Players;
        }
    }
}