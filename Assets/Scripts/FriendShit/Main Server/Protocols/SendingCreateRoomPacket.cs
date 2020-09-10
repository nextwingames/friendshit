using Friendshit.MainServer;
using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class SendingCreateRoomPacket:Packet
        {
            public string Name;
            public int MaxPeople;
            public int Map;

            public SendingCreateRoomPacket(string name, int maxPeople, int map)
            {
                Name = name;
                MaxPeople = maxPeople;
                Map = map;
            }
        }
    }
}