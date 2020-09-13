using Friendshit.MainServer;
using Friendshit.Protocols;
using Nextwin.Protocol;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace Services
    {
        public class EnterRoomService:Service
        {
            public const int EnterFailFull = 0;
            public const int EnterFailNull = 1;
            public const int EnterSuccess = 2;

            private ReceivingEnterRoomPacket _receivingEnterRoomPacket;

            // 경고 창
            private Animator _alertAnimator;
            private Text _alertMessage;

            public EnterRoomService(Packet packet) : base(packet)
            {
                _receivingEnterRoomPacket = (ReceivingEnterRoomPacket)packet;
            }

            public override void Execute()
            {
                switch(_receivingEnterRoomPacket.Result)
                {
                    case EnterFailFull:
                        Alert("The room is already full.");
                        break;

                    case EnterFailNull:
                        Alert("The room is not exist.");
                        break;

                    case EnterSuccess:
                        EnterRoom();
                        break;
                }
            }

            private void FindAlertPanel()
            {
                _alertAnimator = GameObject.Find("Alert Panel").GetComponent<Animator>();
                _alertMessage = GameObject.Find("Alert Message").GetComponent<Text>();
            }

            private void Alert(string message)
            {
                FindAlertPanel();

                _alertMessage.text = message;
                _alertAnimator.Play("Open");
            }

            private void EnterRoom()
            {
                GameObject roomPanel = GameObject.Find("Room Panel");
                RoomPanelManager roomPanelManager = roomPanel.GetComponent<RoomPanelManager>();

                roomPanelManager.SetRoom(_receivingEnterRoomPacket);
                roomPanel.GetComponent<Animator>().Play("Open");
            }
        }
    }
}
