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

            private Room _room;

            public void SetRoom(ReceivingEnterRoomPacket receivingEnterRoomPacket)
            {
                _room = new Room(receivingEnterRoomPacket);
                UpdateUi();
            }

            private void UpdateUi()
            {
                _roomId.text = _room.Id.ToString();
                _roomName.text = _room.Name.ToString();

                _roomMaxPeopleDropdown.value = _room.MaxPeople - 4;
                SetMapImage();

                for(int i = 0; i < _room.Headcount; i++)
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
                _playerNicknames[index].text = _room.Players[index];
            }
        }
    }
}
