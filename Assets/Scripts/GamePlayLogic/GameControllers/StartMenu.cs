using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class StartMenu : MonoBehaviour
{
    public PlayerData MyPlayer { get; private set; }

    public event Action<PlayerData> PlayerInitialized;

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            MyPlayer = GetComponent<TerroristData>();
        else
            MyPlayer = GetComponent<CounterTerrotistData>();

        PlayerInitialized?.Invoke(MyPlayer);
    }
}
