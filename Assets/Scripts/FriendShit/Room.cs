using Friendshit.Protocols;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Friendshit
{
    public class Room : MonoBehaviour
    {
        public const int SupplyWearHouse = 0;

        public const bool Waiting = false;
        public const bool Playing = true;

        public int Id { get; }
        public string Name { get; set; }
        public int Headcount { get; set; }
        public int MaxPeople { get; set; }
        public int Map { get; set; }
        public bool Status { get; set; }
        public string[] Players { get; set; }

        public Room(ReceivingEnterRoomPacket receivingEnterRoomPacket)
        {
            Id = receivingEnterRoomPacket.Id;
            Name = receivingEnterRoomPacket.Name;
            Headcount = receivingEnterRoomPacket.Headcount;
            MaxPeople = receivingEnterRoomPacket.MaxPeople;
            Map = receivingEnterRoomPacket.Map;
            Status = Waiting;
            Players = receivingEnterRoomPacket.Players;
        }

        //public Room(ReceivingLobbyPacket receivingLobbyPacket)
        //{

        //}
    }
}
