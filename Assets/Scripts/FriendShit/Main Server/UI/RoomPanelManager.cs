using Friendshit.Protocols;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    namespace MainServer
    {
        public class RoomPanelManager : MonoBehaviour
        {
            [SerializeField]
            private Text _roomId;
            [SerializeField]
            private Text _roomName;

            [SerializeField]
            private Dropdown _roomMaxPeopleDropdown;
            [SerializeField]
            private Image _mapImage;

            [SerializeField]
            private Image[] _playerImages;
            [SerializeField]
            private Text[] _playerNicknames;

            public Room Room { get; private set; }

            public void SetRoom(ReceivingEnterRoomPacket receivingEnterRoomPacket)
            {
                Room = new Room(receivingEnterRoomPacket);
                UpdateUi();
            }

            private void UpdateUi()
            {
                _roomId.text = Room.Id.ToString();
                _roomName.text = Room.Name.ToString();

                _roomMaxPeopleDropdown.value = Room.MaxPeople - 4;
                SetMapImage();

                for(int i = 0; i < Room.Headcount; i++)
                {
                    UpdatePlayerUi(i);
                }
            }

            private void SetMapImage()
            {
                
            }

            private void UpdatePlayerUi(int index)
            {
                //_playerImages
                _playerNicknames[index].text = Room.Players[index];
            }
        }
    }
}
