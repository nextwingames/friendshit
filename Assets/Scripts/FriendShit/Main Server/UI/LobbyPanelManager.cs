using Friendshit;
using Friendshit.Protocols;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanelManager : MonoBehaviour
{
    public const int RoomsPerPage = 5;

    [SerializeField]
    private GameObject[] _roomObjects;
    private Text[] _roomIds = new Text[RoomsPerPage];
    private Text[] _roomNames = new Text[RoomsPerPage];
    private Text[] _roomHeadcounts = new Text[RoomsPerPage];
    private Text[] _roomStatuses = new Text[RoomsPerPage];

    private int _page;

    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            _roomIds[i] = _roomObjects[i].transform.GetChild(0).GetComponent<Text>();
            _roomNames[i] = _roomObjects[i].transform.GetChild(1).GetComponent<Text>();
            _roomHeadcounts[i] = _roomObjects[i].transform.GetChild(2).GetComponent<Text>();
            _roomStatuses[i] = _roomObjects[i].transform.GetChild(3).GetComponent<Text>();
        }
    }

    public void UpdateLobbyUi(ReceivingLobbyPacket receivingLobbyPacket)
    {
        _page = receivingLobbyPacket.Page;
        int length = receivingLobbyPacket.Length;

        for(int i = 0; i < length; i++)
        {
            _roomIds[i].text = receivingLobbyPacket.Ids[i].ToString();
            _roomNames[i].text = receivingLobbyPacket.Names[i];
            _roomHeadcounts[i].text = receivingLobbyPacket.Headcounts[i] + "/" + receivingLobbyPacket.MaxPeoples[i];
            _roomStatuses[i].text = receivingLobbyPacket.Statuses[i] ? "Playing" : "Waiting";
        }
        for(int i = length; i < 5; i++)
        {
            _roomIds[i].text = "";
            _roomNames[i].text = "";
            _roomHeadcounts[i].text = "";
            _roomStatuses[i].text = "";
        }
    }
}
