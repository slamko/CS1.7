using UnityEngine;

public abstract class PlayerData : MonoBehaviour
{
    [SerializeField] public Transform[] spawnPoints = new Transform[4];

    [SerializeField] public GameObject playerPrefab;
}
