using Friendshit.MainServer;
using Friendshit.Protocols;
using Nextwin.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace Services
    {
        public class LobbyService:Service
        {
            private ReceivingLobbyPacket _receivingLobbyPacket;

            public LobbyService(Packet packet) : base(packet)
            {
                _receivingLobbyPacket = (ReceivingLobbyPacket)packet;
            }

            public override void Execute()
            {
                GameObject.Find("Lobby Panel").GetComponent<LobbyPanelManager>().UpdateLobbyUi(_receivingLobbyPacket);                
            }
        }
    }
}