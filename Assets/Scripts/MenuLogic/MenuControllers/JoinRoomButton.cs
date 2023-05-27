using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class JoinRoomButton : MonoBehaviour
{
    [SerializeField] private Text _roomName, _roomMap, _roomPlayerCount;

    public RoomInfo MyRoom { get; private set; }

    public string RoomName { get; private set; }

    public void Initialize(RoomInfo room)
    {
        MyRoom = room;
        RoomName = MyRoom.Name;
        _roomName.text = MyRoom.Name;
        _roomMap.text = Enum.GetName(typeof(MapEnum), MyRoom.CustomProperties["m"]);
        _roomPlayerCount.text = MyRoom.PlayerCount.ToString();
    }
}
