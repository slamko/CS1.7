using UnityEngine;
using System.Collections.Generic;

class GameControlsHandler : MonoBehaviour
{
    [SerializeField] private List<ILevelUpdate> gameControlsList = new List<ILevelUpdate>();

    public IReadOnlyList<ILevelUpdate> gameControls;

    private void Awake()
    {
        gameControls = gameControlsList;
    }
}

