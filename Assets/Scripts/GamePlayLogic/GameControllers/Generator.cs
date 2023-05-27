using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Generator : MonoBehaviour
{
    private GameObject[] players = new GameObject[2];
    [SerializeField] private GameObject playerControllerPrefab;

    [HideInInspector] public GameObject instantiatedPlayer;

    private void Start()
    {
        Invoke("Spawn", 1f);
    }

    private void Spawn()
    {
        GenerateLevel(GetComponent<StartMenu>().MyPlayer);
    }

    public void GenerateLevel(PlayerData _playerData)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length > 0) {
            foreach (GameObject player in players)
                Destroy(player);
        }

        Transform spawnTransform = _playerData.spawnPoints[Random.Range(0, _playerData.spawnPoints.Length)];
        instantiatedPlayer = 
            PhotonNetwork.Instantiate(_playerData.playerPrefab.name, spawnTransform.position, spawnTransform.rotation);
        print(instantiatedPlayer);
    }
}
