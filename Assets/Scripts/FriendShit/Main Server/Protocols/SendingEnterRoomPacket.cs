using Friendshit.MainServer;
using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class SendingEnterRoomPacket:Packet
        {
            public int Id;
            public string Nickname;

            public SendingEnterRoomPacket(int id, string nickname)
            {
                Id = id;
                Nickname = nickname;
            }
        }
    }
}