using Friendshit.MainServer;
using Friendshit.Protocols;
using Nextwin.Protocol;
using UnityEngine;

namespace Friendshit
{
    namespace Services
    {
        public class CreateRoomService:Service
        {
            private ReceivingCreateRoomPacket _receivingCreateRoomPacket;

            public CreateRoomService(Packet packet) : base(packet)
            {
                _receivingCreateRoomPacket = (ReceivingCreateRoomPacket)packet;
            }

            public override void Execute()
            {
                // 방 입장 요청
                int roomId = _receivingCreateRoomPacket.Id;
                string nickName = MainServerManager.PlayerInformation.Nickname;
                _networkManager.Send(Protocol.EnterRoom, new SendingEnterRoomPacket(roomId, nickName));
            }
        }
    }
}
