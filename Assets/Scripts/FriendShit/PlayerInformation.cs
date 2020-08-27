using Friendshit.Protocols;
using UnityEngine;
using UnityEngine.UI;

namespace Friendshit
{
    public class PlayerInformation
    {
        public string Id { get; }
        public string Nickname { get; }
        public int TotalGame { get; }
        public int Gold { get; }
        public int Silver { get; }
        public int Bronze { get; }

        private GameObject _playerInformationPannel;
        private Text _nickname;
        private Text _gold;
        private Text _silver;
        private Text _bronze;

        public PlayerInformation(ReceivingLoginPacket receivingLoginPacket)
        {
            Id = receivingLoginPacket.Id;
            Nickname = receivingLoginPacket.Nickname;
            TotalGame = receivingLoginPacket.TotalGame;
            Gold = receivingLoginPacket.Gold;
            Silver = receivingLoginPacket.Silver;
            Bronze = receivingLoginPacket.Bronze;

            SetLobbyUi();
        }

        private void SetLobbyUi()
        {
            _playerInformationPannel = GameObject.Find("Player Information Panel");
            _nickname = _playerInformationPannel.transform.GetChild(0).GetComponent<Text>();
            _gold = _playerInformationPannel.transform.GetChild(1).GetComponent<Text>();
            _silver = _playerInformationPannel.transform.GetChild(2).GetComponent<Text>();
            _bronze = _playerInformationPannel.transform.GetChild(3).GetComponent<Text>();

            _nickname.text = Nickname;
            _gold.text = Gold.ToString();
            _silver.text = Silver.ToString();
            _bronze.text = Bronze.ToString();
        }
    }
}

