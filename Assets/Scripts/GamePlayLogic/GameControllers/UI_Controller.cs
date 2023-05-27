using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] private Text counterTerroristScoreText, terroristScoreText;

    private int counterTerroristScore, terroristScore;

    public void OnEnable()
    {
        counterTerroristScoreText.text = 0.ToString();
        terroristScoreText.text = 0.ToString();
    }

    public void UpdateLevel(PlayerData playerWon)
    {
        if(playerWon is TerroristData)
            terroristScoreText.text = terroristScore++.ToString();
        else if(playerWon is CounterTerrotistData)
            counterTerroristScoreText.text = counterTerroristScore++.ToString();
    }
}
