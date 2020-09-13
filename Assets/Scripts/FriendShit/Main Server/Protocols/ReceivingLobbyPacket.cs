using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class ReceivingLobbyPacket:Packet
        {
            public int Page;
            public int Length;
            public int[] Ids;
            public string[] Names;
            public int[] Headcounts;
            public int[] MaxPeoples;
            public bool[] Statuses;
        }
    }
}