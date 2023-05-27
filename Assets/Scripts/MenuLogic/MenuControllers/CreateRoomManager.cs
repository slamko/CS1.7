using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Dropdown mapSelector, maxPlayersCountSelector;
    [SerializeField] private InputField roomNameField;

    private List<string> mapOptions = new List<string>();
    private List<int> maxPlayersOptions = new List<int>();

    public override void OnConnectedToMaster()
    {
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        mapSelector.ClearOptions();
        maxPlayersCountSelector.ClearOptions();

        mapOptions.AddRange(Enum.GetNames(typeof(MapEnum)));

        mapSelector.AddOptions(mapOptions);
    }

    public void CreateRoom()
    {
        RoomOptions newRoomOptions = new RoomOptions();
        newRoomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable();
        newRoomOptions.CustomRoomProperties.Add("m", mapSelector.value);
        newRoomOptions.MaxPlayers = (byte)(Mathf.Min(SettingsManager.MaxPlayers, MapsDB.MapDb[(MapEnum)mapSelector.value].maxPlayers));

        string roomName = roomNameField.text ?? string.Format("Room_", PhotonNetwork.CountOfRooms + 1);
        PhotonNetwork.CreateRoom(roomName, newRoomOptions);
    }
}
