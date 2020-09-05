using Friendshit.MainServer;
using Friendshit.Protocols;
using Nextwin.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace Services
    {
        public class LobbyChatService:Service
        {
            private ReceivingChatPacket _receivingChatPacket;

            private ScrollRect _lobbyChatScrollView;
            private Text _lobbyChatMessages;

            public LobbyChatService(Packet packet) : base(packet)
            {
                _receivingChatPacket = (ReceivingChatPacket)packet;

                _lobbyChatScrollView = GameObject.Find("Lobby Chat Scroll View").GetComponent<ScrollRect>();
                _lobbyChatMessages = GameObject.Find("Lobby Chat Messages").GetComponent<Text>();
            }

            public override void Execute()
            {
                string nickname = _receivingChatPacket.Nickname;
                string message = "[" + nickname + "] " +  _receivingChatPacket.Message + "\n";

                _lobbyChatMessages.text += message;
                _lobbyChatScrollView.verticalNormalizedPosition = 0;
            }
        }
    }
}