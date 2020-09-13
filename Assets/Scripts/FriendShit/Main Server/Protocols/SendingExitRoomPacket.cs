using Friendshit.MainServer;
using Nextwin.Protocol;

namespace Friendshit
{
    namespace Protocols
    {
        public class SendingExitRoomPacket:Packet
        {
            public int RoomId;
            public string Nickname;

            public SendingExitRoomPacket(int roomId, string nickname)
            {
                RoomId = roomId;
                Nickname = nickname;
            }
        }
    }
}