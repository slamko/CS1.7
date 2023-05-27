using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ServersManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject joinRoomButtonPref;

    [SerializeField] private RectTransform startButtonSpawnPos;
    private RectTransform lastButtonSpawnPos;

    private List<GameObject> spawnedButtons = new List<GameObject>();
    private List<JoinRoomButton> spawnedButtonsScripts = new List<JoinRoomButton>();

    private const float ButtonSpawnOffset = 50f;

    private int _roomsCount;
    
    public override void OnConnectedToMaster()
    {   
        lastButtonSpawnPos = startButtonSpawnPos;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (GameObject but in spawnedButtons) Destroy(but);

        lastButtonSpawnPos = startButtonSpawnPos;

        for(int i = 0; i < roomList.Count; i++)
        {
            GameObject newButton = Instantiate(joinRoomButtonPref, lastButtonSpawnPos);
            lastButtonSpawnPos.anchoredPosition = new Vector2(lastButtonSpawnPos.anchoredPosition.x, 
                lastButtonSpawnPos.anchoredPosition.y - ButtonSpawnOffset);
            
            JoinRoomButton butScript = newButton.GetComponent<JoinRoomButton>();

            butScript.Initialize(roomList[i]);
            butScript.GetComponent<Button>().onClick.AddListener(() => PhotonNetwork.JoinRoom(butScript.RoomName));
        }
    }
}
